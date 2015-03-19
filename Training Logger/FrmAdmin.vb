Public Class FrmAdmin
    Dim ignoreClose As Boolean = False
    Private Sub BtnEditCand_Click(sender As Object, e As EventArgs) Handles BtnEditCand.Click
        Me.ignoreClose = True
        Throw New NotImplementedException
    End Sub

    Private Sub BtnResults_Click(sender As Object, e As EventArgs) Handles BtnResults.Click
        Me.ignoreClose = True
        Throw New NotImplementedException
    End Sub

    Private Sub BtnViewLeague_Click(sender As Object, e As EventArgs) Handles BtnViewLeague.Click
        Me.ignoreClose = True
        Throw New NotImplementedException
    End Sub

    Private Sub onQuit(sender As Object, e As FormClosingEventArgs)
        If MsgBox("Are you sure you want to quit?", MsgBoxStyle.OkCancel, "Quit?") = vbOK Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub BtnQuit_Click(sender As Object, e As EventArgs) Handles BtnQuit.Click
        Me.Close()
    End Sub

    Private Sub FrmAdmin_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ignoreClose Then
            e.Cancel = False
        Else
            Me.onQuit(sender, e)
        End If
    End Sub
End Class