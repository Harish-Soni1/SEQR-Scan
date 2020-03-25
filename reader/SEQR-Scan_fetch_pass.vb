Imports MySql.Data.MySqlClient
Imports System.Net
Imports System.Web
Imports System.Collections.Specialized
Imports System.IO
Imports System.Text
Imports System.Net.Mail
Public Class password
    Dim cn As New MySqlConnection("server=localhost;userid="";password="";database=qr")
    Dim RandNumber As String
    Dim em As String
    Dim dr1 As MySqlDataReader
    Dim valid_mail As Boolean = False

    Private Sub password_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cn.Open()
        Button1.Enabled = False
    End Sub
    Public Sub select2()
        Try
            Dim contan As New MySqlConnection("server=localhost;userid="";password="";database=qr")
            contan.Open()
            Dim s As String = "select email from register where name='" + TextBox1.Text + "'"
            Dim ci1 As New MySqlCommand(s, contan)
            dr1 = ci1.ExecuteReader()
            While dr1.Read
                em = dr1.GetString("email")
            End While
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Function valid_email()
        Dim login_str As String = "SELECT email FROM register WHERE first_name='" + TextBox1.Text + "' and email='" + TextBox2.Text + "'"
        Dim cmd As New MySqlCommand(login_str, cn)
        Dim adapt As New MySqlDataAdapter(cmd)
        Dim table As New DataTable()
        adapt.Fill(table)
        Try
            If table.Rows.Count = 1 Then
                MsgBox("OTP Send")
                Return True
            Else
                MsgBox("Sorry OTP is not Send")
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Function

    Public Sub send_otp()
        Try
            Dim smtp_server As New SmtpClient
            Dim mail As New MailMessage
            smtp_server.UseDefaultCredentials = False
            smtp_server.Credentials = New Net.NetworkCredential("mail ID", "mail password")
            smtp_server.Port = 587
            smtp_server.EnableSsl = True
            smtp_server.Host = "smtp.gmail.com"
            mail = New MailMessage()
            mail.From = New MailAddress("mail ID")
            mail.To.Add(TextBox2.Text)
            mail.Subject = "One Time Password for Verification"
            mail.IsBodyHtml = False
            Dim rnd As New Random
            RandNumber = (rnd.Next(100000, 999999)).ToString
            mail.Body = "Your One Time Password for Fetch Credentials is:" + RandNumber
            smtp_server.Send(mail)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If valid_email() Then
            send_otp()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If TextBox3.Text = RandNumber Then
            MsgBox("OTP matched")
            Button1.Enabled = True
        Else
            MessageBox.Show("Invalid OTP")
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Hide()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        Button1.Enabled = False
        log_in.Show()
        Refresh()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim cnh1 As New MySqlConnection("server=localhost;userid="";password="";database=qr")
        cnh1.Open()
        Dim q As String = "select * from register where first_name='" + TextBox1.Text + "'"
        Dim cmd1 As New MySqlCommand(q, cnh1)
        'cmd1.Parameters.AddWithValue("name", TextBox1.Text)
        Dim dr1 As MySqlDataReader
        dr1 = cmd1.ExecuteReader()
        If dr1.HasRows Then
            Label4.Text = "valid"
        Else
            Label4.Text = "Invalid Name"
            'TextBox2.ReadOnly = True
        End If
        cnh1.Close()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        cn.Close()
        Try
            Dim cnh2 As New MySqlConnection("server=localhost;userid="";password="";database=qr")
            cnh2.Open()
            Dim q As String = "select password from register where first_name='" + TextBox1.Text + "'"
            Dim cmd1 As New MySqlCommand(q, cnh2)
            cmd1.Parameters.AddWithValue("first_name", TextBox1.Text)
            Dim dr1 As MySqlDataReader
            dr1 = cmd1.ExecuteReader()
            Dim pass As String
            While dr1.Read
                pass = dr1.GetString("password")
                MsgBox("Your password is " + pass)
            End While
            cnh2.Close()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Dim cnh1 As New MySqlConnection("server=localhost;userid="";password="";database=qr")
        cnh1.Open()
        Dim q As String = "select * from register where email='" + TextBox2.Text + "'"
        Dim cmd1 As New MySqlCommand(q, cnh1)
        'cmd1.Parameters.AddWithValue("email", TextBox2.Text)
        Dim dr1 As MySqlDataReader
        dr1 = cmd1.ExecuteReader()
        If dr1.HasRows Then
            Label5.Text = "Match"
        Else
            Label5.Text = "Wrong E-Mail"
        End If
        cnh1.Close()
    End Sub
End Class
