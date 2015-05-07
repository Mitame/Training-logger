Public Class FrmEditUser

    Dim isEditting As Integer = -1
    Dim parform As FrmEditCand

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()


    End Sub

    'take arguments on creation to store the form that created it and what user it is editing, if any.
    Public Sub New(parent As FrmEditCand)
        Me.New()
        Me.parform = parent
    End Sub

    Public Sub New(UserID As Integer, parent As FrmEditCand)
        Me.New(UserID)
        Me.parform = parent
    End Sub

    Public Sub New(userID As Integer)
        InitializeComponent()
        isEditting = userID 'store the user id


        Dim user = Login.getUser(userID) 'get the user associated with the ID
        Me.Text = "Training Logger - Editing " & user.firstName & " " & user.lastName ' set the window title to display the user being editted

        'set the fields to contain the users data
        TxtUsername.Text = user.username
        TxtFirstName.Text = user.firstName
        TxtLastName.Text = user.lastName
        TxtContact.Text = user.contact

        'set the password field to show that the password has not been changed. Cannot show password for security reasons and because the original password is not stored.
        TxtPassword.Text = "Unchanged"
        'TxtPassword.Enabled = False
        TxtPassword.PasswordChar = ""

        'change the text on the button to show text in the context of editing
        BtnAdd.Text = "Commit"

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        'check that the data input is not going to cause errors in the storage file. Otherwise, display a message saying that it causes problems
        If TxtUsername.Text.Contains(",") Or TxtFirstName.Text.Contains(",") Or TxtLastName.Text.Contains(",") Or TxtContact.Text.Contains(",") Then
            MsgBox("You may not use commas (',') in any of the following fields:" & vbLf &
                   " - Username" & vbLf &
                   " - First Name" & vbLf &
                   " - Last Name" & vbLf &
                   " - Contact"
                   )
            Return
        End If

        'check that the username is not in use, otherwise, print a message stating that the username must be changed as it is in use.
        If Login.username.Contains(TxtUsername.Text) And isEditting = -1 Then
            MsgBox("Username has already been used.")
            Return
        End If

        'make sure all fields have a value in them
        If TxtContact.Text <> "" And TxtFirstName.Text <> "" And TxtLastName.Text <> "" And TxtPassword.Text <> "" And TxtUsername.Text <> "" Then
            If isEditting <> -1 Then 'if there is a user being editted
                'check to see if the password has been changed
                If TxtPassword.Text = "Unchanged" Then
                    TxtPassword.Text = ""
                End If


                Login.editLogin(isEditting, TxtUsername.Text, TxtPassword.Text, TxtFirstName.Text, TxtLastName.Text, TxtContact.Text) 'update the login entry
                MsgBox("User '" & TxtUsername.Text & "' editted!") 'display a message to the user stating that the user has been editted

                Login.loadLogin() 'reload the logins
                Me.parform.loadUsers() 'force the previous form to reload the users being displayed
                Me.Close() 'close itself
            Else
                Login.saveLogin(TxtUsername.Text, TxtPassword.Text, TxtFirstName.Text, TxtLastName.Text, TxtContact.Text) 'save the login
                MsgBox("User '" & TxtUsername.Text & "' created!") 'display a message to the user stating that the user has been created
                Login.loadLogin() 'reload logins from file
                Me.parform.loadUsers() 'force the previous form to reload the users being displayed
                Me.Close() 'close the current form
            End If

        Else
            MsgBox("Please fill in all fields.") 'display a message if all fields are not filled.
        End If
    End Sub

    Private Sub ResetTxtPassword(sender As Object, e As EventArgs) Handles TxtPassword.GotFocus
        TxtPassword.Text = "" 'empty the password field
        'TxtPassword.Enabled = True
        TxtPassword.PasswordChar = "•" 'enable obfusication again
    End Sub

    Private Sub testTxtPassword(sender As Object, e As EventArgs) Handles TxtPassword.LostFocus
        If TxtPassword.Text = "" And Me.isEditting Then 'check that a user is being editted and not created and that the password field is blank
            TxtPassword.Text = "Unchanged" 'show the user that the password has not been changed since before
            'TxtPassword.Enabled = False
            TxtPassword.PasswordChar = "" 'deobfusicate the text to the user can read it.
        End If
    End Sub

    'reset the password field when the text box is clicked on
    Private Sub TxtPassword_onClick(sender As Object, e As EventArgs) Handles TxtPassword.Click
        ResetTxtPassword(sender, e)
    End Sub

    'close the form if the user decides to cancel creating or editting a user
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub
End Class