Public Class FrmCreateAdmin


    'For all text boxes, if the user presses enter, pass to the next text box. if it is the last text box, press the submit button
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

        'Display a message if the username is blank
        If TxtUsername.Text = "" Then
            MsgBox("Please enter a username.")
        End If

        'Display a message if the password is blank
        If TxtPassword.Text = "" Then
            MsgBox("Please enter a password.")
            Return
        End If

        'Display a message if the password does not equal the password confirmation
        If TxtPassword.Text <> TxtPassConfirm.Text Then
            MsgBox("The passwords do not match. Please re-enter them and make sure they are the same.")
            TxtPassword.Text = ""
            TxtPassConfirm.Text = ""
            Return
        End If


        Login.saveLogin(TxtUsername.Text, TxtPassword.Text, "Admin", "Big-Deallio", "I'm always listening...") 'save the login, fields {2,3,4} do not matter as they are for user details, so random data is inserted
        MsgBox("Your login details have been saved. You may now log in using the username and password you just entered.") 'Notify the user the their details have been saved.
        Dim newForm As New FrmLogin 'Create the new form
        newForm.Show() 'Make the newform visable
        Me.Close() 'close the current form
    End Sub
End Class