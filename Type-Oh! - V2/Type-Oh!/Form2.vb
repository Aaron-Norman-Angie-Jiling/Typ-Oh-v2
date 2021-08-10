Public Module quakee
    Public quakeStatus As Boolean = False
End Module
Public Class Form2

        Private Sub btnToGame_Click(sender As Object, e As EventArgs) Handles btnToGame.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub btnShake_Click(sender As Object, e As EventArgs) Handles btnShake.Click
        Form1.Show()
        Me.Hide()
        quakeStatus = True
        Form3.Show()
    End Sub
End Class