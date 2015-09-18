Public Class TunnelForm
    Private _tunnelGenerator As TunnelGenerator

    Private Sub TunnelForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim texture = My.Resources.Texture
        _tunnelGenerator = New TunnelGenerator(Me, texture)
    End Sub

    Private Sub animationTimer_Tick(sender As Object, e As EventArgs) Handles animationTimer.Tick
        Invalidate()
    End Sub
End Class
