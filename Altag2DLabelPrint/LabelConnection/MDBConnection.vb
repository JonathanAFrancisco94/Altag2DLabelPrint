
Imports System.Data.OleDb

Public Class MDBConnection
    Inherits DBConnection

#Region "INSTANCE VARIABLE - FIELD OBJECT"
    Private _cmd As OleDbCommand
#End Region

    Public Overrides Function GetDataTableDataSet(ConnString As String) As DataSet
        Dim conn As OleDbConnection = New OleDbConnection(ConnString)
        _cmd = New OleDbCommand
        Dim mdbSQLAdapter As New OleDbDataAdapter(_cmd)
        Dim ds As New DataSet
        SyncLock _cmd
            Try
                Using _cmd
                    With _cmd
                        .Connection = conn
                        .CommandType = MyBase.CommandType
                        .CommandText = MyBase.Query
                        SetSQLParameters()
                        .CommandTimeout = 120
                        mdbSQLAdapter.Fill(ds)
                    End With
                End Using
            Catch ex As Exception
                DoDispose(mdbSQLAdapter)
                If conn IsNot Nothing Then
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                        DoDispose(conn)
                    End If
                End If
                DoDispose(_cmd)
            End Try
        End SyncLock
        Return ds
    End Function
    Public Overrides Function ExecuteNonQuery(ConnString As String) As Integer
        Dim conn As OleDbConnection = New OleDbConnection(ConnString)
        _cmd = New OleDbCommand
        SyncLock _cmd

            Try
                conn.Open()

                Using _cmd
                    With _cmd

                        .CommandType = MyBase.CommandType
                        .CommandText = MyBase.Query
                        SetSQLParameters()
                        .Connection = conn
                        .CommandTimeout = 120
                        Return .ExecuteNonQuery()
                    End With
                End Using
            Catch ex As Exception
                'end try here

            Finally
                If conn IsNot Nothing Then
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                        DoDispose(conn)
                    End If
                End If
                DoDispose(_cmd)

            End Try
        End SyncLock
        Return Nothing
    End Function
    Protected Overrides Sub SetSQLParameters()
        Dim myEnumerator As IDictionaryEnumerator = CType(referenceParameter.GetEnumerator, IDictionaryEnumerator)
        While (myEnumerator.MoveNext())
            Dim Key = myEnumerator.Key
            Dim strParamName As String = CType(myEnumerator.Value, String)

            If _parameter.Keys.Contains(strParamName) Then
                With _cmd
                    Dim strParamValue = _parameter.Item(strParamName)
                    If strParamName.Trim.Equals("") Then
                        .Parameters.Add(strParamValue)

                    ElseIf IsNothing(strParamValue) Then
                        .Parameters.AddWithValue(strParamName, vbNull)
                    Else
                        .Parameters.AddWithValue(strParamName, strParamValue)
                    End If

                End With
            End If
        End While

        'For Each value As DictionaryEntry In _parameter
        '    Dim strParamName = myEnumerator.Key
        '    Dim strParamValue = myEnumerator.Value
        'Next

        'For Each strParam In _parameter
        '    Dim strParamName = strParam.Key
        '    Dim strParamValue = strParam.Value
        '    With _cmd
        '        If strParamName.trim.Equals("") Then
        '            .Parameters.Add(strParamValue)

        '        ElseIf IsNothing(strParamValue) Then
        '            .Parameters.AddWithValue(strParamName, vbNull)
        '        Else
        '            .Parameters.AddWithValue(strParamName, strParamValue)
        '        End If

        '    End With
        'Next
    End Sub

    Private referenceParameter As New SortedList(Of Integer, String)
    <System.Diagnostics.DebuggerStepThrough()> _
    Public Overrides Sub AddParameter(parameter As String, value As Object)
        Dim parameterCurrentCount As Integer = _parameter.Count
        If parameterCurrentCount = 0 Then
            referenceParameter.Clear()
            referenceParameter = New SortedList(Of Integer, String)
        End If
        _parameter.Add(parameter, value)
        referenceParameter.Add(parameterCurrentCount, parameter)
    End Sub
End Class
