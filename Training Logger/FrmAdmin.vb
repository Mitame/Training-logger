Public Class FrmAdmin
    Dim ignoreClose As Boolean = False 'make a value for use later on if to check if we shoudl ask about closing the form

    Private Sub BtnEditCand_Click(sender As Object, e As EventArgs) Handles BtnEditCand.Click
        Me.ignoreClose = True 'state that the form should close without warning
        Dim newform As New FrmEditCand 'create a new form
        newform.Show() 'make it visable
        Me.Close() 'close the current form
    End Sub

    Private Sub BtnViewLeague_Click(sender As Object, e As EventArgs) Handles BtnViewLeague.Click
        Me.ignoreClose = True 'state that the form should close without warning
        Dim newform As New FrmRank 'create a new form
        newform.Show() 'make it visable
        Me.Close() 'close the current form
    End Sub

    Private Sub onQuit(sender As Object, e As FormClosingEventArgs)
        If MsgBox("Are you sure you want to quit?", MsgBoxStyle.OkCancel, "Quit?") = vbOK Then 'Ask the user if they're sure they want to close the form
            e.Cancel = False 'if yes, do nothing and allow the form to close
        Else
            e.Cancel = True 'otherwise, cancel the event of closing the form
        End If
    End Sub

    Private Sub BtnQuit_Click(sender As Object, e As EventArgs) Handles BtnQuit.Click
        Me.Close() 'start closing the form if someone clicks quit
    End Sub



    Private Sub FrmAdmin_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ignoreClose Then 'check if we definately want the form to close
            e.Cancel = False 'if we do then do nothing and allow the form the close
        Else
            Me.onQuit(sender, e) 'otherwise, ask the user if they're sure
        End If
    End Sub


End Class