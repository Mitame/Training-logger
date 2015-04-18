Public Class FrmEditCand

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Dim newform As New FrmEditUser(Me)
        newform.show()

    End Sub

    Private Sub FrmEditCand_FormClosing(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        Dim newform As New FrmAdmin
        newform.Show()
    End Sub

    Private Sub FrmEditCand_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadUsers()
    End Sub

    Public Sub loadUsers()
        LbData.Items.Clear()
        LbData.Items.Add("Username".PadRight(20) & vbTab & "First Name".PadRight(20) & vbTab & "Last Name".PadRight(20) & vbTab & "Contact Details".PadRight(40) & vbTab & "Password Hash")
        For userID As Integer = 0 To Login.users.Length - 1
            Dim user As Login.User = Login.getUser(userID)

            Try
                LbData.Items.Add(user.username.PadRight(20) & vbTab &
                                 user.firstName.PadRight(20) & vbTab &
                                 user.lastName.PadRight(20) & vbTab &
                                 user.contact.PadRight(40) & vbTab &
                                 user.passwordHash)
            Catch e As System.NullReferenceException
            End Try


        Next userID
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        If LbData.SelectedIndex < 0 Then
            MsgBox("You must select a candidate first.")
            Return
        End If
        Dim newform As New FrmEditUser(LbData.SelectedIndex - 1, Me)
        newform.Show()

    End Sub
End Class