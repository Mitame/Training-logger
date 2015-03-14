Public Class FrmLogin

    Private Sub TableLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub

    Private Sub FrmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not Login.loadLogin() Then
            MsgBox("As this is the first time you have launched this program, you will have to create a new user." & vbCrLf &
                   "Please enter a memorable username and password.")
            Dim newForm As New FrmCreateLogin
            newForm.Show()
            Me.Close()
        End If
    End Sub

    Private Sub ButtonSubmit_Click(sender As Object, e As EventArgs) Handles ButtonSubmit.Click
        If Login.checkLogin(TxtUsername.Text, TxtPassword.Text) Then
            MsgBox("Success!")
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
