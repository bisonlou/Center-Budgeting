Imports System.Data.SqlClient
Imports System.Configuration
Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo
Imports System.Reflection
Imports System.IO
Public Class Connection
    Public IC As String
    Dim DataSource As String
    Dim User As String
    Dim Password As String
    Dim Database As String
    Dim port As String
    Public conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)

    Public Sub Connection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        XtraTabControl1.SelectedTabPage = XtraTabPage1
        TextEdit1.Text = conn.DataSource

        Try
            conn.Open()
            Dim companies As String = "select substring(name,6,21) as Company from sys.databases where name like 'paldb%'"
            Dim dataadapter As New SqlDataAdapter(companies, conn)
            Dim ds As New DataSet()
            dataadapter.Fill(ds, "Companies")
            DataGridView1.DataSource = ds
            DataGridView1.DataMember = "Companies"
            conn.Close()

            Dim Company As String = "select count(substring(name,6,21)) from sys.databases where name = 'palintCentreBudgets'"
            Dim CompanyCmd As New SqlCommand(Company, conn)
            Dim Count As Integer
            conn.Open()
            Count = CompanyCmd.ExecuteScalar()
            conn.Close()
            If Count = 0 Then
                CreateDB.Show()
            End If
        Catch ex As Exception
            XtraTabControl1.SelectedTabPage = XtraTabPage2
        End Try


    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        DataSource = TextEdit1.Text
        User = TextEdit2.Text
        Password = TextEdit3.Text

        writeConnection(DataSource, User, Password, Database)

    End Sub

    Private Sub CheckEdit1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckEdit1.CheckedChanged
        If CheckEdit1.CheckState = CheckState.Checked Then
            TextEdit3.Properties.PasswordChar = ""
        Else
            TextEdit3.Properties.PasswordChar = "*"
        End If

    End Sub

    Private Sub writeConnection(DataSource As String, User As String, Password As String, Database As String)
        Dim config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        Dim connectionStringsSection = DirectCast(config.GetSection("connectionStrings"), ConnectionStringsSection)
        connectionStringsSection.ConnectionStrings("ConnectionString").ConnectionString = "Data Source='" + DataSource + "';UID='" + User + "';password='" + Password + "';Persist Security Info = 'True'"
        config.Save()
        ConfigurationManager.RefreshSection("connectionStrings")

        Application.Restart()
    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        SelectCompany()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        SelectCompany()
    End Sub
    Private Sub SelectCompany()
        IC = Nothing
        Dim CompanyName As String = DataGridView1.CurrentCell.Value.ToString
        IC = "paldb" & CompanyName
        Master.IC = IC

        Dim countUsers As String = "Select count(*) from palintCentreBudgets.dbo.tblUsers"
        Dim CountCmd As New SqlCommand(countUsers, conn)
        Try
            conn.Open()
            If CountCmd.ExecuteScalar = 0 Then
                Master.Show()
                Me.Hide()
            Else
                LoginForm.Show()
                Me.Hide()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

End Class