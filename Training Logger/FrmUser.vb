Public Class FrmUser
    Dim user As Login.User

    Public Sub New(User As Login.User)
        InitializeComponent()
        Me.user = User
    End Sub

    Private Sub FrmUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LblWelcome.Text = "Welcome, " & Me.user.firstName & " " & Me.user.lastName
    End Sub
End Class