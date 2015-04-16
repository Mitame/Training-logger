Public Class FrmEditCand

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim newform As New FrmEditUser
        newform.show()

    End Sub

    Private Sub FrmEditCand_FormClosing(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        Dim newform As New FrmAdmin
        newform.Show()
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim newform As New FrmEditUser(1)
        newform.Show()
    End Sub
End Class