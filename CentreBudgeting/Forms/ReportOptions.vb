Imports System.Data.SqlClient
Imports System.Configuration
Public Class ReportOptions
    Public strreportName As String
    Private IC As String = Master.IC
    Private dsCenters As New DataSet
    Public connBudgets As New SqlConnection

    Private conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)

    Private Sub ReportOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If strreportName = "BudgetVActualQuaterly" Then
            DateTimePicker1.Enabled = True
            DateTimePicker2.Enabled = True
        Else
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
        End If
        Call Fill_Grid(sender, e)
    End Sub

    Private Sub Fill_Grid(sender As Object, e As EventArgs)
        Dim Level1 As String = "SELECT DISTINCT [strLvl1] as [Centres] FROM " & _
         "" + IC + ".[dbo].[tblCenters] WHERE [strLvl2] IS NULL"
        Dim Level1Cmd As New SqlCommand(Level1, conn)

        conn.Open()
        Dim daCenters As New SqlDataAdapter(Level1Cmd)
        daCenters.Fill(dsCenters, "tblCenters")
        conn.Close()

        DataGridView1.DataSource = dsCenters
        DataGridView1.DataMember = "tblCenters"
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Me.Close()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Dim datasource As String = conn.DataSource
        connBudgets.ConnectionString = "Data Source=" & datasource & ";Database=palIntCentreBudgets;UID=sa;password=@2mcl123"

        Dim strlvl1 As String

        If DataGridView1.Rows.Count > 0 Then
            Try
                strlvl1 = DataGridView1.CurrentCell.Value.ToString
            Catch ex As Exception
                MessageBox.Show("Please select a Centre", "Centre Budgeting", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try
            If strreportName = "Budget" Then

                Dim dsBudgetList As New dsBudgetList
                Dim rptBugetList As New rptBudgetList
                Dim query As String = "Select * from tblBudgets WHERE strLvl1 = '" & strlvl1 & "'"

                Dim queryCmd As New SqlCommand(query, connBudgets)
                Try
                    connBudgets.Open()
                    Dim daBudgetList As New SqlDataAdapter(queryCmd)
                    daBudgetList.Fill(dsBudgetList, "tblBudgets")

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                Finally
                    connBudgets.Close()
                End Try

                If dsBudgetList.Tables(0).Rows.Count > 0 Then

                    rptBugetList.SetDataSource(dsBudgetList)

                    If ChkIsOveriden.CheckState = CheckState.Checked Then
                        rptBugetList.ParameterFields(0).CurrentValues.AddValue(True)
                        rptBugetList.ParameterFields(1).CurrentValues.AddValue(SpinEdit1.Value)
                    Else
                        rptBugetList.ParameterFields(0).CurrentValues.AddValue(False)
                        rptBugetList.ParameterFields(1).CurrentValues.AddValue(1.0)
                    End If


                    ReportViewer.CrystalReportViewer1.ReportSource = rptBugetList
                    ReportViewer.Show()
                Else
                    MessageBox.Show("No data to display.", "Palladium Centre Budgeting", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            ElseIf strreportName = "BudgetVActual" Then

                Dim rptBudgetVActual As New rptBudgetVActual
                Dim dsBudgetVActual As New BudgetVActual

                Dim query As String = "Select * from tblBudgets WHERE strLvl1 = '" & strlvl1 & "'"
                Dim queryCmd As New SqlCommand(query, connBudgets)
                Try
                    connBudgets.Open()
                    Dim daBudgetVActual As New SqlDataAdapter(queryCmd)
                    daBudgetVActual.Fill(dsBudgetVActual, "tblBudgets")

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                Finally
                    connBudgets.Close()
                End Try

                If dsBudgetVActual.Tables("tblBudgets").Rows.Count > 0 Then

                    Dim connExpenses As New SqlConnection
                    connExpenses.ConnectionString = "Data Source=" & datasource & ";Database=" & IC & ";UID=sa;password=@2mcl123"

                    Dim queryExpenses As String = "Select * from CentreExpense WHERE strLvl1 = '" & strlvl1 & "'"
                    Dim queryExpensesCmd As New SqlCommand(queryExpenses, connExpenses)

                    Try
                        connExpenses.Open()
                        Dim daCentreExpense As New SqlDataAdapter(queryExpensesCmd)
                        daCentreExpense.Fill(dsBudgetVActual, "CentreExpenseQuaterly")

                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    Finally
                        connExpenses.Close()
                    End Try
                End If

                If dsBudgetVActual.Tables("CentreExpenseQuaterly").Rows.Count > 0 Then

                    rptBudgetVActual.SetDataSource(dsBudgetVActual)

                    If ChkIsOveriden.CheckState = CheckState.Checked Then
                        rptBudgetVActual.ParameterFields(1).CurrentValues.AddValue(True)
                        rptBudgetVActual.ParameterFields(0).CurrentValues.AddValue(SpinEdit1.Value)
                    Else
                        rptBudgetVActual.ParameterFields(1).CurrentValues.AddValue(False)
                        rptBudgetVActual.ParameterFields(0).CurrentValues.AddValue(1.0)
                    End If

                    ReportViewer.CrystalReportViewer1.ReportSource = rptBudgetVActual
                    ReportViewer.Show()
                Else
                    MessageBox.Show("No data to display.", "Palladium Centre Budgeting", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If


            ElseIf strreportName = "BudgetVActualQuaterly" Then

                Dim Months As Integer
                Months = DateDiff(DateInterval.Month, DateTimePicker1.Value.Date, DateTimePicker2.Value.Date) + 1

                Dim rptBudgetVActualQuaterly As New rptBudgetVActualQuaterly
                Dim dsQuaterlyExpenses As New dsQuaterlyExpenses

                Dim query As String = "Select * from tblBudgets WHERE strLvl1 = '" & strlvl1 & "'"
                Dim queryCmd As New SqlCommand(query, connBudgets)
                Try
                    connBudgets.Open()
                    Dim daBudgetVActual As New SqlDataAdapter(queryCmd)
                    daBudgetVActual.Fill(dsQuaterlyExpenses, "tblBudgets")

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                Finally
                    connBudgets.Close()
                End Try

                If dsQuaterlyExpenses.Tables("tblBudgets").Rows.Count > 0 Then

                    Dim connExpenses As New SqlConnection
                    connExpenses.ConnectionString = "Data Source=" & datasource & ";Database=" & IC & ";UID=sa;password=@2mcl123"

                    Dim dteStart As String
                    Dim dteEnd As String

                    dteStart = ConvertToDatabaseDateFormat(DateTimePicker1.Value)
                    dteEnd = ConvertToDatabaseDateFormat(DateTimePicker2.Value)


                    Dim queryExpenses As String = "SELECT uidCenterID, strLvl1, strLvl2, strLvl3, strDesc, SUM(TOTAL) AS Expenses, intGLNumber " & _
                    "FROM dbo.CentreExpenses WHERE (dteJournalDate >= '" & dteStart & "') AND (dteJournalDate <= '" & dteEnd & "') " & _
                    "GROUP BY uidCenterID, strLvl1, strLvl2, strLvl3, strDesc, intGLNumber "

                    Dim queryExpensesCmd As New SqlCommand(queryExpenses, connExpenses)

                    Try
                        connExpenses.Open()
                        Dim daCentreExpense As New SqlDataAdapter(queryExpensesCmd)
                        daCentreExpense.Fill(dsQuaterlyExpenses, "CentreExpensesQuaterly")

                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    Finally
                        connExpenses.Close()
                    End Try
                End If

                If dsQuaterlyExpenses.Tables("CentreExpensesQuaterly").Rows.Count > 0 Then

                    rptBudgetVActualQuaterly.SetDataSource(dsQuaterlyExpenses)

                    If ChkIsOveriden.CheckState = CheckState.Checked Then
                        rptBudgetVActualQuaterly.ParameterFields(1).CurrentValues.AddValue(True)
                        rptBudgetVActualQuaterly.ParameterFields(0).CurrentValues.AddValue(SpinEdit1.Value)
                        rptBudgetVActualQuaterly.ParameterFields(2).CurrentValues.AddValue(Months)

                    Else
                        rptBudgetVActualQuaterly.ParameterFields(1).CurrentValues.AddValue(False)
                        rptBudgetVActualQuaterly.ParameterFields(0).CurrentValues.AddValue(1.0)
                        rptBudgetVActualQuaterly.ParameterFields(2).CurrentValues.AddValue(Months)
                    End If

                    ReportViewer.CrystalReportViewer1.ReportSource = rptBudgetVActualQuaterly
                    ReportViewer.Show()
                Else
                    MessageBox.Show("No data to display.", "Palladium Centre Budgeting", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Else
            MessageBox.Show("Please select a Centre", "Centre Budgeting", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles ChkIsOveriden.CheckStateChanged
        If ChkIsOveriden.CheckState = CheckState.Unchecked Then
            SpinEdit1.Enabled = False
        Else
            SpinEdit1.Enabled = True
        End If
    End Sub

    Public Sub printContributionReport()
        Dim dsDonorContribution As New dsDonorContribution
        Dim rptBugetList As New rptConsolidatedBudget
        Dim query As String = "Select * from DonorContribution"
        Dim datasource As String = conn.DataSource
        connBudgets.ConnectionString = "Data Source=" & datasource & ";Database=palIntCentreBudgets;UID=sa;password=@2mcl123"

        Dim queryCmd As New SqlCommand(query, connBudgets)
        Try
            connBudgets.Open()
            Dim daDonorContribution As New SqlDataAdapter(queryCmd)
            daDonorContribution.Fill(dsDonorContribution, "DonorContribution")

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            connBudgets.Close()
        End Try

        If dsDonorContribution.Tables(0).Rows.Count > 0 Then

            rptBugetList.SetDataSource(dsDonorContribution)

            ReportViewer.CrystalReportViewer1.ReportSource = rptBugetList
            ReportViewer.Show()
        Else
            MessageBox.Show("No data to display.", "Palladium Centre Budgeting", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If


    End Sub
    Public Function ConvertToDatabaseDateFormat(dteDate As Date, Optional Time As String = "") As String
        Dim sqlDateDay = DatePart(DateInterval.Day, dteDate)
        Dim sqlDateMonth = DatePart(DateInterval.Month, dteDate)
        Dim sqlDateYear = DatePart(DateInterval.Year, dteDate)
        Dim sqlDate As String
        If Time = "" Then
            sqlDate = sqlDateYear.ToString + "-" + sqlDateMonth.ToString + "-" + sqlDateDay.ToString
        Else
            sqlDate = sqlDateYear.ToString + "-" + sqlDateMonth.ToString + "-" + sqlDateDay.ToString + " " + Time
        End If
        Return sqlDate
    End Function
End Class