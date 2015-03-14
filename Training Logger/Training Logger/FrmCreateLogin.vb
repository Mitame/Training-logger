Public Class FrmCreateLogin

    Private Sub TxtPassConfirm_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPassConfirm.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            ButtonSubmit.PerformClick()
        End If
    End Sub

    Private Sub TxtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            TxtPassConfirm.Focus()
        End If
    End Sub

    Private Sub TxtUsername_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtUsername.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            TxtPassword.Focus()
        End If
    End Sub

    Private Sub ButtonSubmit_Click(sender As Object, e As EventArgs) Handles ButtonSubmit.Click
        If TxtUsername.Text = "" Then
            MsgBox("Please enter a username.")
        End If

        If TxtPassword.Text = "" Then
            MsgBox("Please enter a password.")
            Return
        End If

        If TxtPassword.Text <> TxtPassConfirm.Text Then
            MsgBox("The passwords do not match. Please re-enter them and make sure they are the same.")
            TxtPassword.Text = ""
            TxtPassConfirm.Text = ""
            Return
        End If

        Login.saveLogin(TxtPassword.Text, TxtUsername.Text)
        MsgBox("Your login details have been saved. You may now log in using the username and password you just entered.")
        Dim newForm As New FrmLogin
        newForm.Show()
        Me.Close()
    End Sub
End Class