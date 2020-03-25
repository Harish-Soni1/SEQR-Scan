Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Net
Imports System.Web
Imports System.Net.Mail
Public Class mmenu
    Dim cn As New MySqlConnection("server=localhost;userid=root;password=harish#1;database=qr")
    Dim qrg As New MessagingToolkit.QRCode.Codec.QRCodeEncoder
    Dim dr1 As MySqlDataReader
    Dim d1 As String
    Dim d2 As String
    Private Sub mmenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cn.Open()
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim cn1 As New MySqlConnection("server=localhost;userid=root;password=harish#1;database=qr")
        cn1.Open()
        Dim q As String = "select * from membr where name='" + TextBox1.Text + "'"
        Dim cmd1 As New MySqlCommand(q, cn1)
        cmd1.Parameters.AddWithValue("name", TextBox1.Text)
        Dim dr1 As MySqlDataReader
        dr1 = cmd1.ExecuteReader()
        While dr1.Read
            Label23.ForeColor = Color.Red
            Label23.Text = "Already Taken"
            Exit Sub
        End While
        Label23.Text = "Available"
        Label23.ForeColor = Color.Green
        cn1.Close()
        If TextBox1.Text = " " Then
            Label23.Visible = False
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim s As String
            If ComboBox1.SelectedItem = "Computer" Then
                s = "Computer"
            ElseIf ComboBox1.SelectedItem = "Science" Then
                s = "Computer"
            ElseIf ComboBox1.SelectedItem = "Managment" Then
                s = "Managment"
            ElseIf ComboBox1.SelectedItem = "Pharmacy" Then
                s = "Pharmacy"
            End If
            Dim ins As String
            ins = "insert into membr values('" & TextBox9.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & s & "','" & TextBox4.Text & "')"
            Dim ci As New MySqlCommand(ins, cn)
            ci.ExecuteNonQuery()
            MessageBox.Show("Member Saved")
        Catch ex As Exception
            MsgBox("Please Insert Necessary Data")
        End Try
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox10.Text = "" Then
            MsgBox("Please Insert Data")
        Else
            Try
                Dim up As String
                up = "update membr set name='" & TextBox8.Text & "', designation='" & TextBox7.Text & "',department='" & TextBox6.Text & "',mobile_number='" & TextBox5.Text & "' where id_number='" & TextBox10.Text & "'"
                Dim ci As New MySqlCommand(up, cn)
                ci.ExecuteNonQuery()
                MessageBox.Show("Updation Succesfull")
                disp_data()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox10.Text = "" Then
            MsgBox("Please Select Id")
        Else
            Try
                Dim del As String
                del = "delete from membr where id_number='" + TextBox10.Text + "'"
                Dim ci As New MySqlCommand(del, cn)
                ci.ExecuteNonQuery()
                MessageBox.Show("Record Deleted")
                disp_data()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub
    Public Sub disp_data()
        Dim cmd As MySqlCommand
        cmd = cn.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from membr order by id_number"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim da As New MySqlDataAdapter(cmd)
        da.Fill(dt)
        DataGridView1.DataSource = dt
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        disp_data()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)

    End Sub

    Private Sub LinkLabel2_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Me.Hide()
        home.Show()
        Refresh()
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Me.Hide()
        home.Show()
        Refresh()
    End Sub

    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        Me.Hide()
        home.Show()
        Refresh()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SaveFileDialog1.ShowDialog()
    End Sub
    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Try
            Dim img As New Bitmap(PictureBox1.Image)
            img.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Png)
        Catch ex As Exception
            MessageBox.Show("error")
        End Try
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        Dim reg As Regex = New Regex("[0-9]{10}")
        Dim isvalid As Boolean = reg.IsMatch(TextBox4.Text.Trim)
        If isvalid Then
            Label24.Text = "Number is valid"
            Label24.ForeColor = Color.Green
        Else
            Label24.Text = "Number is invalid"
            Label24.ForeColor = Color.Red
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If TextBox1.Text = "" And TextBox9.Text = "" Then
            MsgBox("Please Insert Data")
        Else
            Try
                PictureBox1.Image = qrg.Encode(TextBox9.Text + "|" + TextBox1.Text)
            Catch ex As Exception
                MessageBox.Show("error")
            End Try
        End If
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        Dim cn1 As New MySqlConnection("server=localhost;userid=root;password=harish#1;database=qr")
        cn1.Open()
        Dim q As String = "select * from membr where id_number='" + TextBox9.Text + "'"
        Dim cmd1 As New MySqlCommand(q, cn1)
        cmd1.Parameters.AddWithValue("name", TextBox1.Text)
        Dim dr1 As MySqlDataReader
        dr1 = cmd1.ExecuteReader()
        While dr1.Read
            Label25.ForeColor = Color.Red
            Label25.Text = "Already Taken"
            Exit Sub
        End While
        Label25.Text = "Available"
        Label25.ForeColor = Color.Green
        cn1.Close()
    End Sub

    Private Sub LinkLabel5_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel5.LinkClicked
        Me.Hide()
        home.Show()
    End Sub
    Function select1()
        Dim contan As New MySqlConnection("server=localhost;userid=root;password=harish#1;database=qr")
        contan.Open()
        Try
            d1 = DateTimePicker1.Value.Date.ToString("yyyy-MM-dd")
            d2 = DateTimePicker2.Value.Date.ToString("yyyy-MM-dd 23:59:59")
            Dim s1 As String = "select * from login_table where log_in between '" + d1 + "' and '" + d2 + "'"
            Dim ci1 As New MySqlCommand(s1, contan)
            dr1 = ci1.ExecuteReader()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Function

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        select1()
        If dr1.HasRows Then
            Dim cn1 As New MySqlConnection("server=localhost;userid=root;password=harish#1;database=qr")
            cn1.Open()
            Try
                Dim res = Path.GetRandomFileName().Replace(".", vbEmpty)
                Dim s As String = "select * from login_table where log_in between '" + d1 + "' and '" + d2 + "' into outfile 'C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/" + res + ".csv' fields terminated by ',' lines terminated by '\r\n'"
                Dim cmd1 As New MySqlCommand(s, cn1)
                Dim dr1 As MySqlDataReader
                dr1 = cmd1.ExecuteReader()
                MsgBox("File Exported")
                Dim file As String = "C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/" + res + ".csv"
                If System.IO.File.Exists(file) Then
                    System.Diagnostics.Process.Start(file)
                End If
            Catch ex As Exception
                MsgBox("File does not exported")
            End Try
        Else
            MsgBox("NO Record Found")
        End If
    End Sub

    Private Sub DataGridView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseDoubleClick
        TextBox10.Text = DataGridView1.SelectedRows(0).Cells(0).Value.ToString()
        TextBox8.Text = DataGridView1.SelectedRows(0).Cells(1).Value.ToString()
        TextBox7.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString()
        TextBox6.Text = DataGridView1.SelectedRows(0).Cells(3).Value.ToString()
        TextBox5.Text = DataGridView1.SelectedRows(0).Cells(4).Value.ToString()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If RichTextBox1.Text = "" Then
            MsgBox("Please Provide Your Feedback")
        Else
            Try
                Dim smtp_server As New SmtpClient
                smtp_server.UseDefaultCredentials = False
                smtp_server.Credentials = New Net.NetworkCredential("stony1575@gmail.com", "Tony@123@")
                smtp_server.Port = 587
                smtp_server.EnableSsl = True
                smtp_server.Host = "smtp.gmail.com"
                Dim sentfeed As New MailMessage
                sentfeed.From = New MailAddress("stony1575@gmail.com")
                sentfeed.To.Add("soni277@gmail.com")
                sentfeed.Subject = "Feedback of SEQR-Scan"
                sentfeed.IsBodyHtml = False
                sentfeed.Body = RichTextBox1.Text
                smtp_server.Send(sentfeed)
                MsgBox("Feedback Sent")
                RichTextBox1.Clear()
            Catch ex As Exception
                MsgBox("Soory! We are unable to send your Feedback")
            End Try
        End If
    End Sub
End Class