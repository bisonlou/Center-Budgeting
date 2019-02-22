Imports System.Data.SqlClient
Imports System.Configuration
Public Class CreateDB
    Dim DbName As String
    Dim FilePath As String
    Public conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
    Private Sub CreateDB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProgressBarControl1.Properties.Minimum = 0
        ProgressBarControl1.Properties.Maximum = 100
        ProgressBarControl1.EditValue = 0

        ProgressBarControl1.EditValue = 10
        Dim Db As String = "palIntCentreBudgets"

        ProgressBarControl1.EditValue = 20
        Dim Path As String = "SELECT SUBSTRING(physical_name, 1, CHARINDEX(N'master.mdf', LOWER(physical_name)) - 1)" & _
        "FROM Master.sys.master_files " & _
                  "WHERE database_id = 1 AND file_id = 1"
        Dim PathCmd As New SqlCommand(Path, conn)
        Try
            ProgressBarControl1.EditValue = 30
            conn.Open()
            Dim PathReader As SqlDataReader = PathCmd.ExecuteReader()
            While PathReader.Read
                FilePath = PathReader(0).ToString
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
        ProgressBarControl1.EditValue = 40
        Dim database = "CREATE DATABASE palIntCentreBudgets ON PRIMARY (NAME =  palIntCentreBudgets, " & _
                   " FILENAME = '" + FilePath + "palIntCentreBudgets.mdf')" & _
                   " LOG ON (NAME = palIntCentreBudgets_Log, " & _
                   "FILENAME = '" + FilePath + "palIntCentreBudgets_Log.ldf', " & _
                   " SIZE = 1MB, " & _
                   " MAXSIZE = 5MB, " & _
                   " FILEGROWTH = 10%) "

        ProgressBarControl1.EditValue = 60

        Dim tblBudgets As String = "CREATE TABLE [palIntCentreBudgets].[dbo].[tblBudgets](" & _
    "[uidCenterID] [uniqueidentifier] NOT NULL," & _
    "[strLvl1] [nvarchar](200) NOT NULL," & _
    "[strLvl2] [nvarchar](200) NOT NULL," & _
    "[strLvl3] [nvarchar](200) NULL," & _
    "[strLvl4] [nvarchar](200) NULL," & _
    "[Account] [int] NOT NULL," & _
    "[Desc] [nvarchar](200) NOT NULL," & _
    "[decBudget] [money] NOT NULL," & _
    "[ExchangeRate] [decimal](18, 2) NOT NULL DEFAULT ((1.00))"

        Dim tblUsers As String = "CREATE TABLE [palIntCentreBudgets].[dbo].[tblUsers](" & _
   "[strUserName] [nvarchar](20) NOT NULL," & _
   "[strName] [nvarchar](50) NOT NULL," & _
   "[strPassword] [nvarchar](2000) NOT NULL," & _
   "CONSTRAINT [PK_tblUsers] PRIMARY KEY CLUSTERED " & _
   "( [strUserName] Asc)" & _
   "WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON," & _
   "ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]"

        ProgressBarControl1.EditValue = 70
        Dim myCommand As New SqlCommand(database, conn)
        Dim tblBudgetsCmd As New SqlCommand(tblBudgets, conn)
        Dim tblUsersCmd As New SqlCommand(tblUsers, conn)
        Try
            conn.Open()
            myCommand.ExecuteNonQuery()
            tblBudgetsCmd.ExecuteNonQuery()
            ProgressBarControl1.EditValue = 80
            tblUsersCmd.ExecuteNonQuery()
            ProgressBarControl1.EditValue = 90
            ProgressBarControl1.EditValue = 100
            MessageBox.Show("Database is created successfully", _
                                "Centre Budgeting", MessageBoxButtons.OK, _
                                 MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()

              
        Me.Close()
        Application.Restart()
    End Sub
End Class