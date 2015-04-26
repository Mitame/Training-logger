Public Class FrmRank

    Private Sub FrmRank_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fillTable()
    End Sub

    Private Sub fillTable()
        LbRanks.Items.Add("Username".PadRight(40) & vbTab & "Contact Details".PadRight(40) & vbTab & "Rank".PadRight(40) & vbTab)
        Dim users() As Login.User = {}
        Dim selectUser As Login.User

        For user As Integer = 0 To Login.users.Length - 1
            selectUser = Login.getUser(user)
            If Not IsNothing(selectUser) Then
                selectUser.getRank()
                Array.Resize(users, users.Length + 1)
                users(users.Length - 1) = selectUser
            End If


        Next

        Dim sorted = users.OrderBy(Function(x) x.getRank())

        For user As Integer = 0 To users.Length - 1
            LbRanks.Items.Add(sorted(user).username.PadRight(40) & vbTab & sorted(user).contact.PadRight(40) & vbTab & sorted(user).getRank().ToString().PadRight(40) & vbTab)
        Next
    End Sub

    Private Sub FrmRank_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim newform As New FrmAdmin
        newform.Show()
    End Sub
End Class