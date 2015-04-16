Public Class FrmEditUser

    Dim isEditting As Integer = -1

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(userID As Integer)
        InitializeComponent()
        isEditting = userID


        Dim user = Login.getUser(userID)
        Me.Text = "Training Logger - Editing " & user.firstName & " " & user.lastName

        TxtUsername.Text = user.username
        TxtFirstName.Text = user.firstName
        TxtLastName.Text = user.lastName
        TxtContact.Text = user.contact

        TxtPassword.Text = "Unchanged"
        'TxtPassword.Enabled = False
        TxtPassword.PasswordChar = ""
        BtnAdd.Text = "Commit"
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click

        If TxtUsername.Text.Contains(",") Or TxtFirstName.Text.Contains(",") Or TxtLastName.Text.Contains(",") Or TxtContact.Text.Contains(",") Then
            MsgBox("You may not use commas (',') in any of the following fields:" & vbLf &
                   " - Username" & vbLf &
                   " - First Name" & vbLf &
                   " - Last Name" & vbLf &
                   " - Contact"
                   )
            Return
        End If

        If Login.username.Contains(TxtUsername.Text) And isEditting = -1 Then
            MsgBox("Username has already been used.")
        End If

        If TxtContact.Text <> "" And TxtFirstName.Text <> "" And TxtLastName.Text <> "" And TxtPassword.Text <> "" And TxtUsername.Text <> "" Then
            If isEditting <> -1 Then
                Login.editLogin(isEditting, TxtUsername.Text, TxtPassword.Text, TxtFirstName.Text, TxtLastName.Text, TxtContact.Text)
                MsgBox("User '" & TxtUsername.Text & "' editted!")
                Login.loadLogin()
                Me.Close()
            Else
                Login.saveLogin(TxtUsername.Text, TxtPassword.Text, TxtFirstName.Text, TxtLastName.Text, TxtContact.Text)
                MsgBox("User '" & TxtUsername.Text & "' created!")
                Login.loadLogin()
                Me.Close()
            End If

        Else
            MsgBox("Please fill in all fields.")
        End If
    End Sub

    Private Sub ResetTxtPassword(sender As Object, e As EventArgs) Handles TxtPassword.GotFocus
        TxtPassword.Text = ""
        'TxtPassword.Enabled = True
        TxtPassword.PasswordChar = "•"
    End Sub

    Private Sub testTxtPassword(sender As Object, e As EventArgs) Handles TxtPassword.LostFocus
        If TxtPassword.Text = "" And Me.isEditting Then
            TxtPassword.Text = "Unchanged"
            'TxtPassword.Enabled = False
            TxtPassword.PasswordChar = ""
        End If
    End Sub

    Private Sub TxtPassword_onClick(sender As Object, e As EventArgs) Handles TxtPassword.Click
        ResetTxtPassword(sender, e)
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub
End Class