Public Class FrmEditCand

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim newform As New FrmEditUser
        newform.show()

    End Sub

    Private Sub FrmEditCand_FormClosing(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        Dim newform As New FrmAdmin
        newform.Show()
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs)
        Dim newform As New FrmEditUser(1)
        newform.Show()
    End Sub

    Private Sub FrmEditCand_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadUsers()
    End Sub

    Private Sub loadUsers()
        LbData.Items.Clear()
        LbData.Items.Add("Username".PadRight(20) & vbTab & "First Name".PadRight(20) & vbTab & "Last Name".PadRight(20) & vbTab & "Contact Details".PadRight(40) & vbTab & "Password Hash")
        For userID As Integer = 0 To Login.users.Length - 1
            Dim user As Login.User = Login.getUser(userID)


            LbData.Items.Add(user.username.PadRight(20) & vbTab &
                             user.firstName.PadRight(20) & vbTab &
                             user.lastName.PadRight(20) & vbTab &
                             user.contact.PadRight(40) & vbTab &
                             user.passwordHash)
        Next userID
    End Sub

End Class