
Imports System.IO
Imports System.Text
Imports System.IO.Ports
Imports System.Net
Imports System.Net.Sockets
Imports System.Drawing.Printing

Public Class frmMain

    Private dao As New LabelDao()
    Private vo As New LabelVo()
    Private cPos As Integer

    Private Sub CreateDataGrid()
        Me.dgvList.Columns.Clear()
        Dim DgvRow_Flag As DataGridViewColumn = New System.Windows.Forms.DataGridViewImageColumn
        Me.dgvList.Columns.Add("MNBR", "MNBR")
        Me.dgvList.Columns.Add("MCNO", "MCNO")
    End Sub


    Private Sub tsbtOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtOpen.Click

        Dim op As New OpenFileDialog
        op.Filter = "txt file(*.txt)|"
        op.FileName = ""


        If op.ShowDialog = Windows.Forms.DialogResult.OK Then

            If op.FileName = "" Then
            Else

                Dim fs As New FileStream(op.FileName, FileMode.Open, FileAccess.Read)
                Dim sr As New StreamReader(fs)
                Dim strTemp As String = sr.ReadToEnd

                For Each Str As String In strTemp.Split(vbCrLf)
                    Dim ary() As String = Str.Split("|")
                    If ary.Length = 2 Then
                        vo = dao.GetMNBR(ary(1).Trim)
                        Me.dgvList.Rows.Add(vo.MNBR.Trim, vo.MachineName)
                    Else
                        vo = dao.GetMNBR(Str.Trim)
                        Me.dgvList.Rows.Add(vo.MNBR, vo.MachineName)
                    End If

                Next
                SetDesignDataGridView(Me.dgvList, True, True)

            End If

        End If

    End Sub

    Private Sub tsbtSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtSelect.Click

        If Me.masktxtPrintIP.Text = "" Then
            MessageBox.Show("Please input Printer IP first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim dr As DataGridViewRow

        For Each dr In Me.dgvList.SelectedRows
            Me.ExecutePrint(dr.Cells(0).Value, dr.Cells(1).Value)
        Next

    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Main.Main()
        Me.tscbPrinter.Text = "Intermec PD42"
        CreateDataGrid()

    End Sub

    Private Sub tsbtAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtAll.Click

        If Me.masktxtPrintIP.Text = "" Then
            MessageBox.Show("Please input Printer IP first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim dResult As DialogResult = MessageBox.Show("Will you print all items?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If dResult = Windows.Forms.DialogResult.Yes Then
            Dim dr As DataGridViewRow

            For Each dr In Me.dgvList.Rows

                Me.ExecutePrint(dr.Cells(0).Value, dr.Cells(1).Value)

            Next
        End If

    End Sub
    Public Sub SetDesignDataGridView(ByVal d As DataGridView, ByVal FullRowSelect As Boolean, ByVal ClearSelection As Boolean)

        Dim c As DataGridViewColumn
        With d

            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            .CellBorderStyle = DataGridViewCellBorderStyle.Sunken
            .GridColor = Color.RosyBrown
            .RowHeadersVisible = False
            .ColumnHeadersVisible = True
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .EditMode = DataGridViewEditMode.EditOnEnter
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = False
            If ClearSelection = True Then
                .ClearSelection()
                .Enabled = True
            End If

            If FullRowSelect = True Then
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            Else
                .SelectionMode = DataGridViewSelectionMode.CellSelect
            End If

            For Each c In d.Columns
                c.SortMode = DataGridViewTriState.False
            Next

        End With


    End Sub

    Private Sub ExecutePrint(ByVal mnbr As String, ByVal machineNo As String)

        Select Case Me.tscbPrinter.SelectedIndex
            Case 0
                'JFRAN TEST COMMIT
                'Dim sb As New StringBuilder
                'sb.Append("<STX><ESC>C<ETX>")
                'sb.Append("<STX><ESC>P<ETX>")
                'sb.Append("<STX>E1;F1<ETX>")
                'sb.Append("<STX>H7;o100,210;f0;h1;w1;c20;r0;b0;d3,M" & _MNBR & ";<ETX>")
                'sb.Append("<STX>H8;o100,410;f0;h1;w1;c20;r0;b0;d3,STOP" & _MNBR & ";<ETX>")
                'sb.Append("<STX>H9;o100,610;f0;h1;w1;c20;r0;b0;d3,BURN OFF 1;<ETX>")
                'sb.Append("<STX>H10;o100,810;f0;h1;w1;c20;r0;b0;d3,CLEAR;<ETX>")
                'sb.Append("<STX>B1;o100,200;f1;c18,2;h6;w6;d0,100<ETX>")
                'sb.Append("<STX>B2;o100,400;f1;c18,2;h6;w6;d0,100<ETX>")
                'sb.Append("<STX>B3;o100,600;f1;c18,2;h6;w6;d0,100<ETX>")
                'sb.Append("<STX>B4;o100,800;f1;c18,2;h6;w6;d0,100<ETX>")
                'sb.Append("<STX>R<ETX>")
                'sb.Append("<STX><ESC>E1<CAN><ETX>")
                'sb.Append("<STX><ESC>F1<LF>M" & _MNBR & "<ETX>")
                'sb.Append("<STX><ESC>F2<LF>STOP" & _MNBR & "<ETX>")
                'sb.Append("<STX><ESC>F3<LF>BURN OFF 1<ETX>")
                'sb.Append("<STX><ESC>F4<LF>CLEAR<ETX>")
                'sb.Append("<STX><ETB><ETX>")

                Dim sb As New StringBuilder
                sb.Append("<STX><ESC>C<ETX>")
                sb.Append("<STX><ESC>P<ETX>")
                sb.Append("<STX>E1;F1<ETX>")
                sb.Append("<STX>H11;o30,35;f0;h1;w1;c20;r0;b0;d3," & machineNo & ";<ETX>")
                sb.Append("<STX>H12;o30,390;f0;h1;w1;c20;r0;b0;d3," & machineNo & ";<ETX>")
                sb.Append("<STX>H13;o280,35;f0;h1;w1;c20;r0;b0;d3," & machineNo & ";<ETX>")
                sb.Append("<STX>H14;o280,390;f0;h1;w1;c20;r0;b0;d3," & machineNo & ";<ETX>")
                sb.Append("<STX>H7;o30,210;f0;h1;w1;c20;r0;b0;d3,M" & mnbr & ";<ETX>")
                sb.Append("<STX>H8;o30,570;f0;h1;w1;c20;r0;b0;d3,STOP" & mnbr & ";<ETX>")
                sb.Append("<STX>H9;o280,210;f0;h1;w1;c20;r0;b0;d3,BURN OFF 1;<ETX>")
                sb.Append("<STX>H10;o280,570;f0;h1;w1;c20;r0;b0;d3,CLEAR;<ETX>")
                sb.Append("<STX>B1;o30,200;f1;c18,2;h6;w6;d0,100<ETX>")
                sb.Append("<STX>B2;o30,560;f1;c18,2;h6;w6;d0,100<ETX>")
                sb.Append("<STX>B3;o280,200;f1;c18,2;h6;w6;d0,100<ETX>")
                sb.Append("<STX>B4;o280,560;f1;c18,2;h6;w6;d0,100<ETX>")
                sb.Append("<STX>R<ETX>")
                sb.Append("<STX><ESC>E1<CAN><ETX>")
                sb.Append("<STX><ESC>F1<LF>M" & mnbr & "<ETX>")
                sb.Append("<STX><ESC>F2<LF>STOP" & mnbr & "<ETX>")
                sb.Append("<STX><ESC>F3<LF>BURN OFF 1<ETX>")
                sb.Append("<STX><ESC>F4<LF>CLEAR<ETX>")
                sb.Append("<STX><ETB><ETX>")

                'PrintLabel(Me.masktxtPrintIP.Text.Replace(" ", ""), sb.ToString, "9100")


                Dim s As String = sb.ToString
                ' device-dependent string, need a FormFeed?
                ' Allow the user to select a printer.
                Dim pd As New PrintDialog()
                pd.PrinterSettings = New PrinterSettings()
                If DialogResult.OK = pd.ShowDialog(Me) Then
                    ' Send a printer-specific to the printer.
                    RawPrinterHelper.SendStringToPrinter(pd.PrinterSettings.PrinterName, s)
                End If

                'File.Copy("c:\print.txt", "USB001")
            Case Else


        End Select
    End Sub


    Private Sub tsbManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbManual.Click

        'If Me.masktxtPrintIP.Text = "   .   .   ." Then
        '    MessageBox.Show("Please input Printer IP first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End If

        'If Me.txtMNBR.Text = "" Then
        '    MessageBox.Show("Please input MNBR.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End If

        'If IsNumeric(txtMNBR.Text) = False Then
        '    MessageBox.Show("MNBR shoud be numeric.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End If

        vo = dao.GetMNBR(txtMcNo.Text)
        If Not IsNothing(vo.MNBR) Then
            Me.dgvList.Rows.Add(vo.MNBR.Trim, vo.MachineName.Trim)
        Else
            MessageBox.Show("No enrolled machine found", "System Info", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub

    Private Sub Refrechk(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtRefresh.Click
        Me.txtMNBR.Text = ""
        Me.lblInform.Text = "List was cleared"
    End Sub

    Private Sub dgvList_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList.CellClick


        If e.RowIndex = -1 Then Exit Sub
        cPos = e.RowIndex
        Me.txtMNBR.Text = Me.dgvList(0, cPos).Value

    End Sub

    Private Function PrintLabel(ByVal PrinterIP As String, ByVal Content As String, ByVal PrintProt As String) As Boolean

        Try                        '꼭Try catch로진행해야함. Try 부분이실질적인process예요
            Dim tcpClient As New System.Net.Sockets.TcpClient()
            Dim sendBytes As [Byte]() = Encoding.Default.GetBytes(Content)
            Dim remoteEP As New Net.IPEndPoint(System.Net.IPAddress.Parse(PrinterIP), CInt(PrintProt))
            tcpClient.Connect(remoteEP)

            Dim networkStream As NetworkStream = tcpClient.GetStream()
            networkStream.Write(sendBytes, 0, sendBytes.Length)
            networkStream.Close()

            tcpClient.Close()

            System.Threading.Thread.Sleep(1500)

        Catch ioEx As InvalidOperationException
            Dim sMsg As String

            sMsg = "Can not connect to printer, please check print first." + vbCrLf + ioEx.Message
            MessageBox.Show(sMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False

        Catch socketEx As SocketException
            Dim sMsg As String

            sMsg = "Can not connect to printer, please check print first." + vbCrLf + socketEx.Message
            MessageBox.Show(sMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False

        Catch Ex As Exception
            Dim sMsg As String

            sMsg = Ex.Message
            MessageBox.Show(sMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

        Return True

    End Function

    Private Sub tsbtClear_Click(sender As Object, e As EventArgs) Handles tsbtClear.Click
        Me.dgvList.Rows.Clear()
    End Sub
End Class
