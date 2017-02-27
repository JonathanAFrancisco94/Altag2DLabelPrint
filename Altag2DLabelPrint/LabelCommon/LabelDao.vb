Imports LabelConnection
Imports System.Linq
Imports System.Text
Imports System.IO

Public Class LabelDao
    Inherits DAO
    Private _mdbConnectionStringP1 As String
    Private _mdbConnectionStringP3 As String
    Private _timeFormat As String = "yyyy-MM-dd HH:mm:ss"
    Public strSQLDBName As String
    Public strSQLUsername As String
    Public strSQLPassword As String

#Region "CONTTRUCTOR"
    Sub New()

        _mdbConnectionStringP1 = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=\SmsartConsoleLogs.mdb"
        _mdbConnectionStringP3 = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=\SmsartConsoleLogs.mdb"
    End Sub
    Sub New(ByVal connectionString As String)
        If connectionString.Trim <> "" Then
            '_mdbConnectionString = connectionString
        End If
    End Sub

#End Region


    Private Function createMSSQLConnectionString(ByVal strServer As String, ByVal strDBName As String, ByVal strClientUser As String, ByVal strClientPassWord As String, ByVal strTimeOut As String) As String



        Dim strConnectionString As String
        If strServer = "" Then
            MessageBox.Show("No Server Provided!" & vbCrLf & "The program will now exit." & vbCrLf, "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ExitApplication()
        End If
        If strDBName = "" Then
            MessageBox.Show("No DBName Provided!" & vbCrLf & "The program will now exit." & vbCrLf, "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ExitApplication()
        End If
        If strClientUser = "" Then
            MessageBox.Show("No Client Account Provided!" & vbCrLf & "The program will now exit." & vbCrLf, "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ExitApplication()
        End If

        If strClientPassWord = "" Then
            MessageBox.Show("No Password Provided!" & vbCrLf & "The program will now exit." & vbCrLf, "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ExitApplication()
        End If

        If strClientPassWord = "" Then
            MessageBox.Show("No Password Provided!" & vbCrLf & "The program will now exit." & vbCrLf, "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ExitApplication()
        End If
        If strTimeOut = "" Then
            MessageBox.Show("No Timeout  provided!" & vbCrLf & "The program will now exit." & vbCrLf, "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ExitApplication()
        End If

        strConnectionString = "Data Source=" & strServer & "; Initial Catalog=" & strDBName & _
                       ";User Id=" & strClientUser & ";Password=" & strClientPassWord & ";Connect Timeout=" & strTimeOut & ";"

        Return strConnectionString
    End Function


End Class
