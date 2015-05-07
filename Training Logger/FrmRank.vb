Public Class FrmRank

    'fill the table when the form loads
    Private Sub FrmRank_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fillTable()
    End Sub

    Private Sub fillTable()
        LbRanks.Items.Add("Username".PadRight(40) & vbTab & "Contact Details".PadRight(40) & vbTab & "Rank".PadRight(40) & vbTab) 'add the table headers
        Dim users() As Login.User = {} 'create a list of users
        Dim selectUser As Login.User 'create a variable for the user being scanned

        For user As Integer = 0 To Login.users.Length - 1 'cycle through the users
            selectUser = Login.getUser(user) 'get the user object for the user
            If Not IsNothing(selectUser) Then 'check there is a user assigned to the variable
                selectUser.getRank() 'get their rank so that it is calculated
                Array.Resize(users, users.Length + 1) 'make the array 1 larger
                users(users.Length - 1) = selectUser 'add the user to the array in the final spot
            End If


        Next

        'created an array of the users scanned previously and sort them by rank
        Dim sorted = users.OrderBy(Function(x) x.getRank())

        'add each user to the table
        For user As Integer = 0 To users.Length - 1
            LbRanks.Items.Add(sorted(user).username.PadRight(40) & vbTab & sorted(user).contact.PadRight(40) & vbTab & sorted(user).getRank().ToString().PadRight(40) & vbTab)
        Next
    End Sub

    'on close, reopen the admin form.
    Private Sub FrmRank_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim newform As New FrmAdmin
        newform.Show()
    End Sub
End Class