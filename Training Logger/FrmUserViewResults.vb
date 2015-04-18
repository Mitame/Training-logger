Public Class FrmUserViewResults
    Dim user As Login.User
    Public Sub New(User As Login.User)
        InitializeComponent()
        Me.user = User
    End Sub

    Private Sub loadResults()
        Dim results() As ModResults.result = ModResults.getResults(user.id)
        results.ToString()
        Console.WriteLine(results(0).getUserID())
        LbData.Items.Clear()
        LbData.Items.Add("Timestamp".PadRight(40) & vbTab & "Activity".PadRight(40) & vbTab & "Time (Seconds)")
        For resultIndex As Integer = 0 To results.Length - 1
            Console.WriteLine(results(resultIndex).getUserID())
            Dim dateToShow As DateTime = results(resultIndex).getTimestamp()
            LbData.Items.Add((dateToShow.Day.ToString() & "-" & dateToShow.Month.ToString() & "-" & dateToShow.Year.ToString() & " " &
                              dateToShow.Hour.ToString() & ":" & dateToShow.Minute.ToString() & ":" & dateToShow.Second.ToString()).PadRight(40) & vbTab &
                             results(resultIndex).getSport().ToString().PadRight(40) & vbTab &
                             results(resultIndex).getTime().ToString())

        Next
    End Sub

    Private Sub FrmUserViewResults_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadResults()
    End Sub

    Private Sub FrmUserViewResults_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Dim newForm As New FrmUser(Me.user)
        newForm.Show()
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim newForm As New FrmUserAddResult(Me.user, False)
        newForm.Show()
    End Sub

    Private Sub LbData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LbData.SelectedIndexChanged

    End Sub

    Private Sub BtnReload_Click(sender As Object, e As EventArgs) Handles BtnReload.Click
        loadResults()
    End Sub

    Private Sub BtnQuit_Click(sender As Object, e As EventArgs) Handles BtnQuit.Click
        Close()
    End Sub
End Class