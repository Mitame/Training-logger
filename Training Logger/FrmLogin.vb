Public Class FrmLogin

    Private Sub FrmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.skipLogin = "True" Then
            Dim newForm As New FrmAdmin
            newForm.Show()
            Me.Close()
        End If

        If Not Login.loadLogin() Then
            MsgBox("As this is the first time you have launched this program, you will have to create a new user." & vbLf &
                   "Please enter a memorable username and password.")
            Dim newForm As New FrmCreateAdmin
            newForm.Show()
            Me.Close()
        End If

    End Sub

    Private Sub ButtonSubmit_Click(sender As Object, e As EventArgs) Handles ButtonSubmit.Click

        Dim user As Login.User = Login.checkLogin(TxtUsername.Text, TxtPassword.Text)

        If user.id <> -1 Then
            MsgBox("Success!")
            If user.id = 0 Then
                Dim newform As New FrmAdmin
                newform.Show()
                Me.Close()
            Else
                Dim newform As New FrmUser(user)
                newform.Show()
                Me.Close()
            End If
        Else
            MsgBox("The username and password do not match. Please try again.")
            TxtPassword.Text = ""
        End If

    End Sub

    Private Sub TxtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPassword.KeyDown

        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            ButtonSubmit.PerformClick()
        End If

    End Sub

    Private Sub TxtUsername_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtUsername.KeyDown

        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            TxtPassword.Focus()
        End If

    End Sub
End Class
