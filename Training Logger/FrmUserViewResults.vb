Public Class FrmUserViewResults
    Dim user As Login.User

    'store creation variables
    Public Sub New(User As Login.User)
        InitializeComponent()
        Me.user = User
    End Sub


    Private Sub loadResults()
        'get list of all results by a user
        Dim results() As ModResults.result = ModResults.getResults(user.id)

        LbData.Items.Clear() 'empty the table
        LbData.Items.Add("Timestamp".PadRight(40) & vbTab & "Activity".PadRight(40) & vbTab & "Time (Seconds)") 'add headers
        For resultIndex As Integer = 0 To results.Length - 1
            Dim dateToShow As DateTime = results(resultIndex).getTimestamp() 'convert the time
            LbData.Items.Add((dateToShow.Day.ToString() & "-" & dateToShow.Month.ToString() & "-" & dateToShow.Year.ToString() & " " &
                              dateToShow.Hour.ToString() & ":" & dateToShow.Minute.ToString() & ":" & dateToShow.Second.ToString()).PadRight(40) & vbTab &
                             results(resultIndex).getSport().ToString().PadRight(40) & vbTab &
                             results(resultIndex).getTime().ToString()) 'add the result to the column

        Next
    End Sub

    'load the results automatically when the form is loaded.
    Private Sub FrmUserViewResults_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadResults()
    End Sub

    'open the user form when this one is closed
    Private Sub FrmUserViewResults_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Dim newForm As New FrmUser(Me.user)
        newForm.Show()
    End Sub

    'open the form the add result when the add button is clicked
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim newForm As New FrmUserAddResult(Me.user, False)
        newForm.Show()
    End Sub


    'load the results again when the reload button is clicked
    Private Sub BtnReload_Click(sender As Object, e As EventArgs) Handles BtnReload.Click
        loadResults()
    End Sub

    'quit when the quit button is clicked.
    Private Sub BtnQuit_Click(sender As Object, e As EventArgs) Handles BtnQuit.Click
        Close()
    End Sub
End Class