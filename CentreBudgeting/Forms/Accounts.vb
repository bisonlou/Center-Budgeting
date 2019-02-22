Imports System.Data.SqlClient
Imports System.Configuration
Public Class Accounts
    Public IC As String = Master.IC
    Public conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
    Private Sub Accounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextEdit1.Focus()
        Dim Accounts As String = "Select intGLNumber as [Number], strDesc AS [Description] " & _
            "FROM " + IC + ".[dbo].[tblAccounts]"
        Dim AccountsCmd As New SqlCommand(Accounts, conn)
        Dim dsAccounts As New DataSet
        Dim daAccounts As New SqlDataAdapter(AccountsCmd)
        conn.Open()
        daAccounts.Fill(dsAccounts, "tblAccounts")
        conn.Close()

        DataGridView1.DataSource = dsAccounts
        DataGridView1.DataMember = "tblAccounts"
    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        Master.TextEdit1.Text = DataGridView1.CurrentRow.Cells(0).Value
        Master.TextEdit2.Text = DataGridView1.CurrentRow.Cells(1).Value
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Master.TextEdit1.Text = DataGridView1.CurrentRow.Cells(0).Value
        Master.TextEdit2.Text = DataGridView1.CurrentRow.Cells(1).Value
        Me.Close()
    End Sub

    Private Sub TextEdit1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextEdit1.KeyPress
        If TextEdit1.Text = "" Then
            Dim Accounts As String = "Select intGLNumber as [Number], strDesc AS [Description] " & _
                       "FROM " + IC + ".[dbo].[tblAccounts]"
            Dim AccountsCmd As New SqlCommand(Accounts, conn)
            Dim dsAccounts As New DataSet
            Dim daAccounts As New SqlDataAdapter(AccountsCmd)
            conn.Open()
            daAccounts.Fill(dsAccounts, "tblAccounts")
            conn.Close()

            DataGridView1.DataSource = dsAccounts
            DataGridView1.DataMember = "tblAccounts"
        Else
            Dim Accounts As String = "Select intGLNumber as [Number], strDesc AS [Description] " & _
            "FROM " + IC + ".[dbo].[tblAccounts] WHERE strDesc LIKE '%" + TextEdit1.Text + "%'"
            Dim AccountsCmd As New SqlCommand(Accounts, conn)
            Dim dsAccounts As New DataSet
            Dim daAccounts As New SqlDataAdapter(AccountsCmd)
            conn.Open()
            daAccounts.Fill(dsAccounts, "tblAccounts")
            conn.Close()

            DataGridView1.DataSource = dsAccounts
            DataGridView1.DataMember = "tblAccounts"

        End If

    End Sub
End Class