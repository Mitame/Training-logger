Public Class FrmUser
    Dim user As Login.User

    'store the user when the form is created
    Public Sub New(User As Login.User)
        InitializeComponent()
        Me.user = User
    End Sub

    'display the users name on the form when it is loaded
    Private Sub FrmUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LblWelcome.Text = "Welcome, " & Me.user.firstName & " " & Me.user.lastName
        BtnView.Enabled = ModResults.FileExists()
    End Sub

    'open the add result form when the add button is clicked
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim newform As New FrmUserAddResult(Me.user, True)
        newform.Show()
        Me.Close()
    End Sub

    'open the results viewing form when the view results button is clicked.
    Private Sub BtnView_Click(sender As Object, e As EventArgs) Handles BtnView.Click
        Dim newform As New FrmUserViewResults(Me.user)
        newform.Show()
        Me.Close()
    End Sub
End Class