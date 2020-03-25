Public Class home
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.SelectedItem = "" Then
            MsgBox("Please select the User-Type")
        End If
        If ComboBox1.SelectedItem = "Admin" Then
            Me.Hide()
            log_in.Show()
        End If
        If ComboBox1.SelectedItem = "Member" Then
            Me.Hide()
            scan.Show()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = -1 Then
            ComboBox1.SelectedIndex = 0
        End If
    End Sub
End Class