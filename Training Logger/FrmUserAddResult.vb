Public Class FrmUserAddResult


    Dim user As Login.User
    Dim openUser As Boolean

    Public Sub New(user As Login.User, openUser As Boolean)
        InitializeComponent()
        Me.user = user
        Me.openUser = openUser
    End Sub

    Public Sub New(user As Login.User)
        InitializeComponent()
        Me.user = user
    End Sub

    Private Sub FrmUserAddResult_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For x As Integer = 0 To ModResults.activites.length - 1
            CmBSport.Items.Add(ModResults.activites(x))
        Next
        CmBSport.SelectedIndex = 0

    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If Val(TxtTime.Text) <= 0 Then
            MsgBox("Please enter a realistic time.")
            Return
        End If

        If CmBSport.SelectedIndex <> -1 And TxtTime.Text <> "" Then
            ModResults.saveResult(user.id, DateTime.Now, TxtTime.Text, CmBSport.Items(CmBSport.SelectedIndex))
            MsgBox("Result Saved")
            Close()
        Else
            MsgBox("Please fill in all fields")
        End If
    End Sub

    Private Sub CmBSport_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmBSport.KeyPress
        e.Handled = True
    End Sub

    Private Sub TxtTime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtTime.KeyPress
        If "01234567890".Contains(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar = "." Then
            e.Handled = TxtTime.Text.Contains(".")
        ElseIf e.KeyChar = vbBack Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub


    Private Sub FrmUserAddResult_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If openUser Then
            Dim Newform As New FrmUser(Me.user)
            Newform.Show()
        End If
    End Sub

End Class