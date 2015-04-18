Public Class FrmUser
    Dim user As Login.User

    Public Sub New(User As Login.User)
        InitializeComponent()
        Me.user = User
    End Sub

    Private Sub FrmUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LblWelcome.Text = "Welcome, " & Me.user.firstName & " " & Me.user.lastName
        BtnView.Enabled = ModResults.FileExists()
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim newform As New FrmUserAddResult(Me.user, True)
        newform.Show()
        Me.Close()
    End Sub

    Private Sub BtnView_Click(sender As Object, e As EventArgs) Handles BtnView.Click
        Dim newform As New FrmUserViewResults(Me.user)
        newform.Show()
        Me.Close()
    End Sub
End Class