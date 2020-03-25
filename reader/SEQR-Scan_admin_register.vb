Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Public Class register
    Dim cn1 As New MySqlConnection("server=localhost;userid="";password="";database=qr")
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cn1.Open()
        Dim gender As String
        If RadioButton1.Checked Then
            gender = "Male"
        Else
            gender = "Female"
        End If
        Dim ins As String = "insert into register(first_name,last_name,department,email,password,gender) values('" & fntext.Text & "','" & lntext.Text & "','" & deptext.Text & "','" & emailtext.Text & "','" & passtxt.Text & "','" & gender & "')"
        Dim ci As New MySqlCommand(ins, cn1)
        ci.ExecuteNonQuery()
        MsgBox("Admin Saved")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        log_in.Show()
        Refresh()
    End Sub

    Private Sub mobtext_TextChanged(sender As Object, e As EventArgs) Handles emailtext.TextChanged
        Dim reg As Regex = New Regex("^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
        Dim isvalid As Boolean = reg.IsMatch(emailtext.Text.Trim)
        If isvalid Then
            Label8.Text = "E-Mail is valid"
            Label8.BackColor = Color.Green
        Else
            Label8.Text = "E-Mail is invalid"
            Label8.BackColor = Color.Red
        End If
    End Sub

    Private Sub fntext_TextChanged(sender As Object, e As EventArgs) Handles fntext.TextChanged
        Dim cnh As New MySqlConnection("server=localhost;userid="";password="";database=qr")
        cnh.Open()
        Dim q As String = "select * from membr where name='" + fntext.Text + "'"
        Dim cmd1 As New MySqlCommand(q, cnh)
        cmd1.Parameters.AddWithValue("name", fntext.Text)
        Dim dr1 As MySqlDataReader
        dr1 = cmd1.ExecuteReader()
        While dr1.Read
            Label9.BackColor = Color.Red
            Label9.Text = "Already Taken"
            Exit Sub
        End While
        Label9.Text = "Available"
        Label9.BackColor = Color.Green
        If fntext.Text = " " Then
            Label9.Visible = False
        End If
        cnh.Close()
    End Sub

    Private Sub register_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
