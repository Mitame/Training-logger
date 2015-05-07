Public Class FrmEditCand

    'Show the candidate editing from when the button is clicked. Tell it that this form created it so it knows what to go back to.
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Dim newform As New FrmEditUser(Me)
        newform.show()
    End Sub

    'Open the admin form if this form is closing
    Private Sub FrmEditCand_FormClosing(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        Dim newform As New FrmAdmin
        newform.Show()
    End Sub

    'get a list of all users when the form is loaded
    Private Sub FrmEditCand_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadUsers()
    End Sub


    Public Sub loadUsers() 'Fills the table with the users information
        LbData.Items.Clear() 'clear any information in the tables (even if there shouldn't be any anyway)
        LbData.Items.Add("Username".PadRight(20) & vbTab & "First Name".PadRight(20) & vbTab & "Last Name".PadRight(20) & vbTab & "Contact Details".PadRight(40) & vbTab & "Password Hash") 'add headers with correct spacing
        For userID As Integer = 0 To Login.users.Length - 1 'cycle through all the users
            Dim user As Login.User = Login.getUser(userID) 'create a new variable to hold the user being scanned

            Try 'catch errors if the data was input incorrectly in the file.
                LbData.Items.Add(user.username.PadRight(20) & vbTab &
                                 user.firstName.PadRight(20) & vbTab &
                                 user.lastName.PadRight(20) & vbTab &
                                 user.contact.PadRight(40) & vbTab &
                                 user.passwordHash) 'adds all information to the table
            Catch e As System.NullReferenceException 'catch errors if a line was empty when scanning for login file originally
            End Try

        Next userID
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        If LbData.SelectedIndex < 0 Then 'output a message if the user has not selected a candidate first
            MsgBox("You must select a candidate first.")
            Return
        End If
        Dim newform As New FrmEditUser(LbData.SelectedIndex - 1, Me) 'open the user editing form
        newform.Show() 'make it visible

    End Sub
End Class