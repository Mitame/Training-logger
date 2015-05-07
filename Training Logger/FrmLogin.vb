Public Class FrmLogin

    Private Sub FrmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.skipLogin = "True" Then 'check the settings file to see if we should skip the login screen, now redundant
            Dim newForm As New FrmAdmin 'create a new form
            newForm.Show() 'make it visible
            Me.Close() 'close the current form
        End If

        If Not Login.loadLogin() Then 'if loading logins failed, assume that no login file has been created and, therefore, this is the first user
            MsgBox("As this is the first time you have launched this program, you will have to create a new user." & vbLf &
                   "Please enter a memorable username and password.") 'tell the user what is happening
            Dim newForm As New FrmCreateAdmin 'create a new form
            newForm.Show() 'make it visible
            Me.Close() 'close the current form
        End If

    End Sub

    Private Sub ButtonSubmit_Click(sender As Object, e As EventArgs) Handles ButtonSubmit.Click

        'check the login and assign it to a variable
        Dim user As Login.User = Login.checkLogin(TxtUsername.Text, TxtPassword.Text)


        If user.id <> -1 Then 'if the user logged in correctly
            MsgBox("Success!") 'tell the user they logged into successfully
            If user.id = 0 Then 'if the user is the admin/first user
                Dim newform As New FrmAdmin 'create the adminform
                newform.Show() 'make it visible
                Me.Close() 'close the current form
            Else
                Dim newform As New FrmUser(user) 'create the standard user form
                newform.Show() 'make it visible
                Me.Close() 'close the current form
            End If
        Else
            MsgBox("The username and password do not match. Please try again.") 'tell the user that they logged in incorrectly.
            TxtPassword.Text = "" 'empty the password field as it is usually the incorrect field
        End If

    End Sub

    'catch if the user presses enter and pass them on to the next text box
    Private Sub TxtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            ButtonSubmit.PerformClick()
        End If
    End Sub

    'catch if the user presses enter and click the login button for them
    Private Sub TxtUsername_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtUsername.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            TxtPassword.Focus()
        End If
    End Sub
End Class
