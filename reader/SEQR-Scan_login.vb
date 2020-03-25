Imports MySql.Data.MySqlClient
Public Class log_in
    Dim cn As New MySqlConnection("server=localhost;user id="";password="";database=qr")

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim u As String = TextBox1.Text
        Dim p As String = TextBox2.Text
        Dim s As String = "select first_name,password from register where first_name='" + u + "' and password='" + p + "'"
        Dim ci As New MySqlCommand(s, cn)
        Dim adapt As New MySqlDataAdapter(ci)
        Dim table As New DataTable()
        adapt.Fill(table)
        If table.Rows.Count = 0 Then
            MsgBox("Invalid Username and Password")
        Else
            MsgBox("Welcome Mr. " + u)
            Me.Hide()
            mm1.Show()
            Refresh()
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Hide()
        register.Show()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Me.Hide()
        password.Show()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            TextBox2.UseSystemPasswordChar = True
        Else
            TextBox2.UseSystemPasswordChar = False
        End If
    End Sub
End Class
