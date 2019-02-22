Imports System.Data.SqlClient
Imports System.Configuration
Public Class Master
    Public IC As String
    Dim uidCenterID As Guid
    Dim UpdateDatabase As Boolean = False
    Public conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToolStripStatusLabel2.Text = Mid(IC, 6)
        Connection.Close()
        ComboBox2.Enabled = False
        ComboBox3.Enabled = False
        ComboBox4.Enabled = False
        SimpleButton1.Enabled = False

        Call Fill_Combobox1(sender, e)
    End Sub
    Private Sub Fill_Combobox1(sender As Object, e As EventArgs)

        Dim Level1 As String = "SELECT DISTINCT [strLvl1] FROM " & _
          "" + IC + ".[dbo].[tblCenters] WHERE [strLvl2] IS NULL"
        Dim Level1Cmd As New SqlCommand(Level1, conn)

        conn.Open()
        Dim Level1Reader As SqlDataReader = Level1Cmd.ExecuteReader()
        While Level1Reader.Read()
            ComboBox1.Items.Add(Level1Reader(0).ToString())
        End While
        Level1Reader.Close()
        conn.Close()
    End Sub
    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        SimpleButton1.Enabled = True
        ComboBox2.Enabled = True
        ComboBox2.Items.Clear()
        ComboBox3.Items.Clear()
        ComboBox4.Items.Clear()
        ComboBox2.Text = ""
        ComboBox3.Text = ""
        ComboBox4.Text = ""
        Dim Level2 As String = "SELECT DISTINCT [strLvl2] FROM " & _
                   "" + IC + ".[dbo].[tblCenters] WHERE [strLvl1] = @Level1 and strLvl2 IS NOT NULL"
        Dim Level2Cmd As New SqlCommand(Level2, conn)
        Level2Cmd.Prepare()
        Level2Cmd.Parameters.Add("@Level1", SqlDbType.NVarChar)
        Level2Cmd.Parameters("@Level1").Value = ComboBox1.Text
        conn.Open()
        Dim Level2Reader As SqlDataReader = Level2Cmd.ExecuteReader()
        While Level2Reader.Read()
            ComboBox2.Items.Add(Level2Reader(0).ToString())
        End While
        Level2Reader.Close()
        conn.Close()
    End Sub

    Private Sub ComboBox2_TextChanged(sender As Object, e As EventArgs) Handles ComboBox2.TextChanged
        ComboBox3.Enabled = True
        ComboBox3.Items.Clear()
        ComboBox4.Items.Clear()
        ComboBox3.Text = ""
        ComboBox4.Text = ""
        Dim Level3 As String = "SELECT DISTINCT [strLvl3] FROM " & _
                   "" + IC + ".[dbo].[tblCenters] WHERE [strLvl1] = @Level1 AND [strLvl2] = @Level2 AND strLvl3 IS NOT NULL"
        Dim Level3Cmd As New SqlCommand(Level3, conn)
        Level3Cmd.Prepare()
        Level3Cmd.Parameters.Add("@Level1", SqlDbType.NVarChar)
        Level3Cmd.Parameters.Add("@Level2", SqlDbType.NVarChar)
        Level3Cmd.Parameters("@Level1").Value = ComboBox1.Text
        Level3Cmd.Parameters("@Level2").Value = ComboBox2.Text
        conn.Open()
        Dim Level3Reader As SqlDataReader = Level3Cmd.ExecuteReader()
        While Level3Reader.Read()
            ComboBox3.Items.Add(Level3Reader(0).ToString())
        End While
        Level3Reader.Close()
        conn.Close()
    End Sub
    Private Sub ComboBox3_TextChanged(sender As Object, e As EventArgs) Handles ComboBox3.TextChanged
        ComboBox4.Enabled = True
        ComboBox4.Items.Clear()
        ComboBox4.Text = ""
        Dim Level4 As String = "SELECT DISTINCT [strLvl4] FROM " & _
                  "" + IC + ".[dbo].[tblCenters] WHERE [strLvl1] = @Level1 AND [strLvl2] = @Level2 AND strLvl3 = @Level3 AND strLvl4 IS NOT NULL"
        Dim Level4Cmd As New SqlCommand(Level4, conn)
        Level4Cmd.Prepare()
        Level4Cmd.Parameters.Add("@Level1", SqlDbType.NVarChar)
        Level4Cmd.Parameters.Add("@Level2", SqlDbType.NVarChar)
        Level4Cmd.Parameters.Add("@Level3", SqlDbType.NVarChar)
        Level4Cmd.Parameters("@Level1").Value = ComboBox1.Text
        Level4Cmd.Parameters("@Level2").Value = ComboBox2.Text
        Level4Cmd.Parameters("@Level3").Value = ComboBox3.Text
        conn.Open()
        Dim Level4Reader As SqlDataReader = Level4Cmd.ExecuteReader()
        While Level4Reader.Read()
            ComboBox4.Items.Add(Level4Reader(0).ToString())
        End While
        Level4Reader.Close()
        conn.Close()
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Accounts.Show()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click

        Dim Amount As Decimal = Decimal.Parse(TextEdit3.Text)
        If TextEdit4.Text = "" Then
            TextEdit4.Text = 1
        End If
        Dim Rate As Decimal = Decimal.Parse(TextEdit4.Text)
        Dim local As Decimal = Amount * Rate

        DataGridView1.Rows.Add(TextEdit1.Text, TextEdit2.Text, Rate, local)
        TextEdit1.Text = ""
        TextEdit2.Text = ""
        TextEdit3.Text = ""
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        If DataGridView1.SelectedRows.Count = 1 Then
            Dim Selected As DataGridViewRow = DataGridView1.CurrentRow
            DataGridView1.Rows.Remove(Selected)
        End If
    End Sub
    Private Sub TextEdit1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextEdit1.TextChanged
        If TextEdit1.Text = "" Or TextEdit2.Text = "" Or TextEdit3.Text = "" Then
            SimpleButton2.Enabled = False
        ElseIf TextEdit1.Text <> "" Or TextEdit2.Text <> "" Or TextEdit3.Text <> "" Then
            SimpleButton2.Enabled = True
        End If
    End Sub
    Private Sub TextEdit2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextEdit2.TextChanged
        If TextEdit1.Text = "" Or TextEdit2.Text = "" Or TextEdit3.Text = "" Then
            SimpleButton2.Enabled = False
        ElseIf TextEdit1.Text <> "" Or TextEdit2.Text <> "" Or TextEdit3.Text <> "" Then
            SimpleButton2.Enabled = True
        End If
    End Sub


    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Call UID()
        Dim Level1 As String = ComboBox1.Text
        Dim Level2 As String = ComboBox2.Text
        Dim level3 As String = ComboBox3.Text

        Dim DonorLedgers As String = "Select Account,[Desc],ExchangeRate,decBudget from palIntCentreBudgets.dbo.tblBudgets " & _
            "WHERE palIntCentreBudgets.dbo.tblBudgets.uidCenterID = @UID"
        Dim DonorLedgersCmd As New SqlCommand(DonorLedgers, conn)
        DonorLedgersCmd.Prepare()
        DonorLedgersCmd.Parameters.Add("@UID", SqlDbType.UniqueIdentifier)
        DonorLedgersCmd.Parameters("@UID").Value = uidCenterID

        conn.Open()
        Dim dsLedger As New DataSet
        Dim daLedgers As New SqlDataAdapter(DonorLedgersCmd)

        daLedgers.Fill(dsLedger, "tblBudgetLedger")
        conn.Close()
        DataGridView1.Rows.Clear()

        For i = 0 To dsLedger.Tables("tblBudgetLedger").Rows.Count - 1
            DataGridView1.Rows.Add()
            DataGridView1.Rows(i).Cells(0).Value = dsLedger.Tables("tblBudgetLedger").Rows(i).Item(0).ToString
            DataGridView1.Rows(i).Cells(1).Value = dsLedger.Tables("tblBudgetLedger").Rows(i).Item(1).ToString
            DataGridView1.Rows(i).Cells(2).Value = dsLedger.Tables("tblBudgetLedger").Rows(i).Item(2).ToString
            DataGridView1.Rows(i).Cells(3).Value = dsLedger.Tables("tblBudgetLedger").Rows(i).Item(3).ToString
        Next
        UpdateDatabase = True
    End Sub

    Private Sub UID()
        Dim Level1 As String = ComboBox1.Text
        Dim Level2 As String = ComboBox2.Text

        Dim level3 As String
        If ComboBox3.Text = "" Then
            level3 = "NULL"
        Else
            level3 = ComboBox3.Text
        End If

        Dim level4 As String
        If ComboBox4.Text = "" Then
            level4 = "NULL"
        Else
            level4 = ComboBox4.Text
        End If

        Dim UID As String
        If ComboBox4.Text = "" And ComboBox3.Text = "" And ComboBox2.Text <> "" And ComboBox1.Text <> "" Then
            UID = "select uidCenterID from " + IC + ".dbo.tblCenters WHERE " & _
            "strLvl1 = @Lvl1 AND strLvl2 = @Lvl2"
        ElseIf ComboBox4.Text = "" And ComboBox3.Text <> "" And ComboBox2.Text <> "" And ComboBox1.Text <> "" Then
            UID = "select uidCenterID from " + IC + ".dbo.tblCenters WHERE " & _
                "strLvl1 = @Lvl1 AND strLvl2 = @Lvl2 AND strLvl3 = @Lvl3"
        ElseIf ComboBox4.Text <> "" And ComboBox3.Text <> "" And ComboBox2.Text <> "" And ComboBox1.Text <> "" Then
            UID = "select uidCenterID from " + IC + ".dbo.tblCenters WHERE " & _
                "strLvl1 = @Lvl1 AND strLvl2 = @Lvl2 AND strLvl3 = @Lvl3 AND strLvl4 = @Lvl4"
        Else
            UID = "select uidCenterID from " + IC + ".dbo.tblCenters WHERE " & _
            "strLvl1 = @Lvl1 "
        End If

        Dim UIDCmd As New SqlCommand(UID, conn)
        UIDCmd.Prepare()
        UIDCmd.Parameters.Add("@Lvl1", SqlDbType.NVarChar)
        UIDCmd.Parameters.Add("@Lvl2", SqlDbType.NVarChar)
        UIDCmd.Parameters.Add("@Lvl3", SqlDbType.NVarChar)
        UIDCmd.Parameters.Add("@Lvl4", SqlDbType.NVarChar)

        UIDCmd.Parameters("@Lvl1").Value = Level1
        UIDCmd.Parameters("@Lvl2").Value = Level2
        UIDCmd.Parameters("@Lvl3").Value = level3
        UIDCmd.Parameters("@Lvl4").Value = level4

        Try
            conn.Open()
            uidCenterID = UIDCmd.ExecuteScalar
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub syncronize()
        Dim success As Boolean = False

        Dim dsBudgets As New DataSet
        Dim strSQL As String = "select Account, sum(decBudget) from palIntCentreBudgets.dbo.tblbudgets group by Account"
        Dim da As New SqlDataAdapter(strSQL, conn)
        da.Fill(dsBudgets, "tblBudgets")

        If dsBudgets.Tables(0).Rows.Count > 0 Then
            Dim strDeleteQry As String = "DELETE FROM " & IC & ".dbo.tblAcctBudgets"
            If deletePalladiumBudget(strDeleteQry) Then

                For i = 0 To dsBudgets.Tables(0).Rows.Count - 1
                    Dim intGLNumber As String = dsBudgets.Tables(0).Rows(i).Item(0).ToString
                    Dim decBudget As Decimal = Decimal.Parse(dsBudgets.Tables(0).Rows(i).Item(1))

                    Dim decPeriodBudgets As Decimal
                    decPeriodBudgets = decBudget / 12

                    Dim strInsertQry As String = "INSERT INTO " & IC & ".dbo.tblAcctBudgets " & _
                        "VALUES (@SQintGLNumber,@SQdecPeriod,@SQdecPeriod,@SQdecPeriod,@SQdecPeriod," & _
                        "@SQdecPeriod,@SQdecPeriod,@SQdecPeriod,@SQdecPeriod,@SQdecPeriod,@SQdecPeriod," & _
                        "@SQdecPeriod,@SQdecPeriod)"

                    If insertPalladiumBudget(strInsertQry, intGLNumber, decPeriodBudgets) Then
                        success = True
                    End If

                Next
            End If
            If success = True Then
                MessageBox.Show("Palladium Syncronization Succesful", "Centre Budgeting", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

    End Sub

    Private Function deletePalladiumBudget(strDeleteQry As String) As Boolean
        Dim success As Boolean = False
        Dim deleteCmd As New SqlCommand(strDeleteQry, conn)
        Try
            conn.Open()
            deleteCmd.ExecuteNonQuery()
            success = True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            conn.Close()
        End Try
        Return success
    End Function

    Private Function insertPalladiumBudget(strInsertQry As String, intGLNumber As String, decPeriodBudgets As Decimal) As Boolean

        Dim success As Boolean = False

        Dim insertCmd As New SqlCommand(strInsertQry, conn)
        insertCmd.Prepare()
        insertCmd.Parameters.Add("@SQintGLNumber", SqlDbType.NVarChar)
        insertCmd.Parameters.Add("@SQdecPeriod", SqlDbType.Decimal)

        insertCmd.Parameters("@SQintGLNumber").Value = intGLNumber
        insertCmd.Parameters("@SQdecPeriod").Value = decPeriodBudgets

        Try
            conn.Open()
            insertCmd.ExecuteNonQuery()
            success = True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            conn.Close()
        End Try

        Return success
    End Function


    Private Sub ToolStripLabel1_Click(sender As Object, e As EventArgs) Handles ToolStripLabel1.Click
        ComboBox1.Text = ""
        ComboBox2.Text = ""
        ComboBox3.Text = ""
        ComboBox4.Text = ""

        TextEdit1.Text = ""
        TextEdit2.Text = ""
        TextEdit3.Text = ""


        DataGridView1.Rows.Clear()
    End Sub


    Private Sub ToolStripLabel2_Click(sender As Object, e As EventArgs) Handles ToolStripLabel2.Click
        Dim check As String = "Select count(*) from palIntCentreBudgets.[dbo].[tblBudgets] WHERE uidCenterID = @UID"
        Dim checkCmd As New SqlCommand(check, conn)
        checkCmd.Prepare()
        checkCmd.Parameters.Add("@UID", SqlDbType.UniqueIdentifier)
        checkCmd.Parameters("@UID").Value = uidCenterID
        Dim count As Integer

        conn.Open()
        count = Integer.Parse(checkCmd.ExecuteScalar())
        conn.Close()



      
        If count > 0 Then
            Dim result As Integer = MessageBox.Show("Are you sure you want to change the budget amount?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = MsgBoxResult.No Then
                Exit Sub
            Else
                saveBudget()
            End If
        Else
            saveBudget()
        End If

        ComboBox1.Text = ""
        ComboBox2.Text = ""
        ComboBox3.Text = ""
        ComboBox4.Text = ""

        TextEdit1.Text = ""
        TextEdit2.Text = ""
        TextEdit3.Text = ""


        DataGridView1.Rows.Clear()

        'Syncronization

        If ToggleSwitch1.EditValue = True Then
            syncronize()
        End If

    End Sub

    Private Sub ToolStripLabel3_Click(sender As Object, e As EventArgs) Handles ToolStripLabel3.Click
        syncronize()
    End Sub

    Private Sub Panel2_DoubleClick(sender As Object, e As EventArgs) Handles Panel2.DoubleClick
        ReportOptions.Show()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.DoubleClick
        ReportOptions.Show()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.DoubleClick
        ReportOptions.strreportName = "Budget"
        ReportOptions.Show()
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.DoubleClick
        ReportOptions.strreportName = "Budget"
        ReportOptions.Show()
    End Sub

    Private Sub Label5_MouseHover(sender As Object, e As EventArgs) Handles Label5.MouseHover
        Label5.ForeColor = Color.Teal
        Label6.ForeColor = Color.Teal
    End Sub

    Private Sub Label5_MouseLeave(sender As Object, e As EventArgs) Handles Label5.MouseLeave
        Label5.ForeColor = Color.Empty
        Label6.ForeColor = Color.Empty
    End Sub
    Private Sub Label6_MouseHover(sender As Object, e As EventArgs) Handles Label6.MouseHover
        Label5.ForeColor = Color.Teal
        Label6.ForeColor = Color.Teal
    End Sub

    Private Sub Label6_MouseLeave(sender As Object, e As EventArgs) Handles Label6.MouseLeave
        Label5.ForeColor = Color.Empty
        Label6.ForeColor = Color.Empty
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.DoubleClick
        ReportOptions.strreportName = "BudgetVActual"
        ReportOptions.Show()
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.DoubleClick
        ReportOptions.strreportName = "BudgetVActual"
        ReportOptions.Show()
    End Sub
    Private Sub Label7_MouseHover(sender As Object, e As EventArgs) Handles Label7.MouseHover
        Label7.ForeColor = Color.Teal
        Label8.ForeColor = Color.Teal
    End Sub

    Private Sub Label7_MouseLeave(sender As Object, e As EventArgs) Handles Label7.MouseLeave
        Label7.ForeColor = Color.Empty
        Label8.ForeColor = Color.Empty
    End Sub
    Private Sub Label8_MouseHover(sender As Object, e As EventArgs) Handles Label8.MouseHover
        Label7.ForeColor = Color.Teal
        Label8.ForeColor = Color.Teal
    End Sub

    Private Sub Label8_MouseLeave(sender As Object, e As EventArgs) Handles Label8.MouseLeave
        Label7.ForeColor = Color.Empty
        Label8.ForeColor = Color.Empty
    End Sub
    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.DoubleClick
        ReportOptions.strreportName = "BudgetVActualQuaterly"
        ReportOptions.Show()
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.DoubleClick
        ReportOptions.strreportName = "BudgetVActualQuaterly"
        ReportOptions.Show()
    End Sub
    Private Sub Label9_MouseHover(sender As Object, e As EventArgs) Handles Label9.MouseHover
        Label9.ForeColor = Color.Teal
        Label10.ForeColor = Color.Teal
    End Sub

    Private Sub Label9_MouseLeave(sender As Object, e As EventArgs) Handles Label9.MouseLeave
        Label9.ForeColor = Color.Empty
        Label10.ForeColor = Color.Empty
    End Sub
    Private Sub Label10_MouseHover(sender As Object, e As EventArgs) Handles Label10.MouseHover
        Label9.ForeColor = Color.Teal
        Label10.ForeColor = Color.Teal
    End Sub

    Private Sub Label10_MouseLeave(sender As Object, e As EventArgs) Handles Label10.MouseLeave
        Label9.ForeColor = Color.Empty
        Label10.ForeColor = Color.Empty
    End Sub
    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.DoubleClick
        ReportOptions.printContributionReport()
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.DoubleClick
        ReportOptions.printContributionReport()
    End Sub
    
    Private Sub Label11_MouseHover(sender As Object, e As EventArgs) Handles Label11.MouseHover
        Label11.ForeColor = Color.Teal
        Label12.ForeColor = Color.Teal
    End Sub

    Private Sub Label11_MouseLeave(sender As Object, e As EventArgs) Handles Label11.MouseLeave
        Label11.ForeColor = Color.Empty
        Label12.ForeColor = Color.Empty
    End Sub
    Private Sub Label12_MouseHover(sender As Object, e As EventArgs) Handles Label12.MouseHover
        Label11.ForeColor = Color.Teal
        Label12.ForeColor = Color.Teal
    End Sub

    Private Sub Label12_MouseLeave(sender As Object, e As EventArgs) Handles Label12.MouseLeave
        Label11.ForeColor = Color.Empty
        Label12.ForeColor = Color.Empty
    End Sub

    Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DataGridView1.EditingControlShowing

        If DataGridView1.CurrentCell.ColumnIndex = 3 Then
            AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
        End If

    End Sub

    Private Sub TextBox_keyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        If Not (Char.IsDigit(CChar(CStr(e.KeyChar))) Or e.KeyChar = "." Or e.KeyChar = ControlChars.Back) Then e.Handled = True


    End Sub

    Private Sub saveBudget()
        Call UID()
        Dim Level1 As String = ComboBox1.Text
        Dim Level2 As String = ComboBox2.Text
        Dim level3 As String = ComboBox3.Text
        Dim level4 As String = ComboBox4.Text

        If UpdateDatabase = True Then

            Dim delete As String = "Delete from palIntCentreBudgets.[dbo].[tblBudgets] WHERE uidCenterID = @UID"
            Dim DeleteCmd As New SqlCommand(delete, conn)
            DeleteCmd.Prepare()
            DeleteCmd.Parameters.Add("@UID", SqlDbType.UniqueIdentifier)
            DeleteCmd.Parameters("@UID").Value = uidCenterID

            conn.Open()
            DeleteCmd.ExecuteNonQuery()
            conn.Close()
        End If

        If DataGridView1.Rows.Count > 0 Then

            DataGridView1.Rows(0).Cells(0).Selected = True

            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                Dim Account As String = DataGridView1.Rows(i).Cells(0).Value
                Dim Description As String = DataGridView1.Rows(i).Cells(1).Value
                Dim Rate As String = DataGridView1.Rows(i).Cells(2).Value
                Dim Budget As String = DataGridView1.Rows(i).Cells(3).Value

                Dim Insert As String = "INSERT INTO palIntCentreBudgets.[dbo].[tblBudgets]([uidCenterID],[strLvl1]," & _
                    "[strLvl2],[strLvl3],[strLvl4],[Account],[Desc],[ExchangeRate],[decBudget]) " & _
                    "VALUES (@UID,@Lvl1,@Lvl2,@Lvl3,@Lvl4,@Account,@Desc,@Rate,@Budget)"
                Dim InsertCmd As New SqlCommand(Insert, conn)
                InsertCmd.Prepare()
                InsertCmd.Parameters.Add("@UID", SqlDbType.UniqueIdentifier)
                InsertCmd.Parameters.Add("@Lvl1", SqlDbType.NVarChar)
                InsertCmd.Parameters.Add("@Lvl2", SqlDbType.NVarChar)
                InsertCmd.Parameters.Add("@Lvl3", SqlDbType.NVarChar)
                InsertCmd.Parameters.Add("@Lvl4", SqlDbType.NVarChar)
                InsertCmd.Parameters.Add("@Account", SqlDbType.NVarChar)
                InsertCmd.Parameters.Add("@Desc", SqlDbType.NVarChar)
                InsertCmd.Parameters.Add("@Rate", SqlDbType.Money)
                InsertCmd.Parameters.Add("@Budget", SqlDbType.Money)

                InsertCmd.Parameters("@UID").Value = uidCenterID
                InsertCmd.Parameters("@Lvl1").Value = Level1
                InsertCmd.Parameters("@Lvl2").Value = Level2
                InsertCmd.Parameters("@Lvl3").Value = level3
                InsertCmd.Parameters("@Lvl4").Value = level4
                InsertCmd.Parameters("@Account").Value = Account
                InsertCmd.Parameters("@Desc").Value = Description
                InsertCmd.Parameters("@rate").Value = Rate
                InsertCmd.Parameters("@Budget").Value = Budget

                Try
                    conn.Open()
                    InsertCmd.ExecuteNonQuery()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                conn.Close()
            Next i
        End If
    End Sub

End Class
