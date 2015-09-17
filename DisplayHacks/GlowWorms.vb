Public Class GlowWorms
    Protected _glow(10) As GlowWorm

    Protected _colors As Color() = {
        Color.Blue,
        Color.Red,
        Color.Orange,
        Color.Magenta,
        Color.Green,
        Color.YellowGreen,
        Color.Cyan,
        Color.Yellow,
        Color.White,
        Color.Violet
    }

    Private Sub Timer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer.Tick
        Invalidate()
    End Sub

    Private Sub GlowWorms_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        For ii As Integer = 0 To 9
            _glow(ii) = New GlowWorm(Me, _colors(ii))
        Next
    End Sub

    Private Sub Slower()
        Timer.Interval += 10
    End Sub

    Private Sub Faster()
        If Timer.Interval > 10 Then
            Timer.Interval -= 10
        End If
    End Sub

    Private Sub GlowWorms_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Add Then
            Faster()
        ElseIf e.KeyCode = Keys.Subtract Then
            Slower()
        End If
    End Sub
End Class
