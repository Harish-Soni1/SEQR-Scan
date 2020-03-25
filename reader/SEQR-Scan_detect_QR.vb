Imports AForge
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports MessagingToolkit.QRCode.Codec
Imports MySql.Data.MySqlClient

Public Class scan
    Dim cam As VideoCaptureDevice
    Dim bit As Bitmap
    Dim dr1 As MySqlDataReader
    Dim sno As String
    Dim query As String = ""
    Dim cn2 As New MySqlConnection("server=localhost;userid=root;password=harish#1;database=qr")
    Dim contan As New MySqlConnection("server=127.0.0.1;userid=root;password=harish#1;database=qr")

    Private Sub scan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cn2.Open()
    End Sub
    Private Sub Captured(ByVal sender As Object, ByVal eventArgs As NewFrameEventArgs)
        bit = DirectCast(eventArgs.Frame.Clone(), Bitmap)
        PictureBox1.Image = DirectCast(eventArgs.Frame.Clone(), Bitmap)
    End Sub

    Private Sub StartWebcam()
        Try

            Dim cam1 As VideoCaptureDeviceForm = New VideoCaptureDeviceForm
            If cam1.ShowDialog = Windows.Forms.DialogResult.OK Then
                cam = cam1.VideoDevice
                AddHandler cam.NewFrame, New NewFrameEventHandler(AddressOf Captured)
                cam.Start()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        StartWebcam()
        DecodeTextBox.Clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            DecodeTextBox.Text = ""
            PictureBox1.ImageLocation = OpenFileDialog1.FileName
        End If
    End Sub
    Public Sub select1()
        Try
            Dim contan As New MySqlConnection("server=localhost;userid=root;password=harish#1;database=qr")
            contan.Open()
            Dim c() As String = DecodeTextBox.Text.Split("|")
            Dim a As String = c(0)
            Dim b As String = c(1)
            Dim s As String = "select * from login_table where name='" + b + "' AND log_out IS NULL"
            Dim ci1 As New MySqlCommand(s, contan)
            dr1 = ci1.ExecuteReader()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub select2()
        Try
            Dim contan As New MySqlConnection("server=localhost;userid=root;password=harish#1;database=qr")
            contan.Open()
            Dim c() As String = DecodeTextBox.Text.Split("|")
            Dim a As String = c(0)
            Dim b As String = c(1)
            Dim s As String = "select max(S_NO) from login_table where name='" + b + "'"
            Dim ci1 As New MySqlCommand(s, contan)
            dr1 = ci1.ExecuteReader()
            While dr1.Read
                sno = dr1.GetString("max(S_NO)")
            End While
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Scans the received image
        Dim scan As New MessagingToolkit.Barcode.BarcodeDecoder
        Dim scanr As MessagingToolkit.Barcode.Result
        Try
            scanr = scan.Decode(New Bitmap(PictureBox1.Image))
            DecodeTextBox.Text = scanr.ToString()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        cn2.Close()
        select1()
        contan.Open()
        Dim contan1 As New MySqlConnection("server=localhost;userid=root;password=harish#1;database=qr")
        contan1.Open()
        Try
            Dim c() As String = DecodeTextBox.Text.Split("|")
            Dim a As String = c(0)
            Dim b As String = c(1)
            If dr1.HasRows Then
                select2()
                query = "update login_table set log_out=now() where S_NO='" + sno + "'"
                MsgBox("See you again Mr. " + b)
            Else
                query = "insert into login_table(id_number,name,log_in) values('" + a + "','" + b + "',now())"
                MsgBox("You are logged in Mr. " + b)
            End If
            contan1.Close()
            Dim cmd As New MySqlCommand(query, contan)
            cmd.ExecuteNonQuery()
            contan.Close()
        Catch e1 As Exception
            MsgBox(e1.ToString)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        cam.Stop()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Hide()
        home.Show()
        Refresh()
    End Sub
End Class