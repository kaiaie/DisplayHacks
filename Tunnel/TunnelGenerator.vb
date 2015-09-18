' Draws a rotating 3D tunnel
' Algorithm based on http://www.student.kuleuven.be/~m0216922/CG/tunnel.html
Public NotInheritable Class TunnelGenerator
    Private _drawingSurface As Control
    Private _tunnelTexture As Bitmap
    Private _frameCount As Integer = -1

    Private _distances(1, 1) As Integer
    Private _angles(1, 1) As Integer

    Public Property DrawingSurface As Control
        Get
            Return _drawingSurface
        End Get

        Private Set(value As Control)
            _drawingSurface = value
        End Set
    End Property

    Public Property TunnelTexture As Bitmap
        Get
            Return _tunnelTexture
        End Get
        Private Set(value As Bitmap)
            _tunnelTexture = value
        End Set
    End Property

    Private Function GetFrameCount() As Integer
        _frameCount += 1
        Return _frameCount
    End Function


    Public Sub New(surface As Control, texture As Bitmap)
        DrawingSurface = surface
        AddHandler surface.Paint, AddressOf Draw
        TunnelTexture = texture
        InitializeLookupTables()
    End Sub

    Private Sub InitializeLookupTables()
        Dim textureWidth = TunnelTexture.Width
        Dim textureHeight = TunnelTexture.Height
        Dim w = DrawingSurface.ClientRectangle.Width
        Dim h = DrawingSurface.ClientRectangle.Height
        ReDim _distances(0 To w - 1, 0 To h - 1)
        ReDim _angles(0 To w - 1, 0 To h - 1)

        For x As Integer = 0 To w - 1
            For y As Integer = 0 To h - 1
                Dim ratio As Double = 32.0

                _distances(x, y) = CInt(ratio * textureHeight / Math.Sqrt((x - w / 2.0) * (x - w / 2.0) + (y - h / 2.0) * (y - h / 2.0))) Mod textureHeight
                _angles(x, y) = 0.5 * textureWidth * Math.Atan2(y - h / 2.0, x - w / 2.0) / Math.PI
            Next
        Next
    End Sub

    Public Sub Draw(ByVal sender As Object, ByVal e As PaintEventArgs)
        Dim textureWidth = TunnelTexture.Width
        Dim textureHeight = TunnelTexture.Height
        Dim w = DrawingSurface.ClientRectangle.Width
        Dim h = DrawingSurface.ClientRectangle.Height
        Dim currentFrame = GetFrameCount()

        ' Calculate the shift values out of the animation value
        Dim shiftX = CInt(textureWidth * 1.0 * currentFrame)
        Dim shiftY = CInt(textureHeight * 0.25 * currentFrame)

        Using b As New Bitmap(w, h, e.Graphics)
            For x As Integer = 0 To w - 1
                For y As Integer = 0 To h - 1
                    ' Get the texel from the texture by using the tables, shifted with the animation values
                    Dim newX = (_distances(x, y) + shiftX) Mod textureWidth
                    Dim newY = Math.Abs((_angles(x, y) + shiftY) Mod textureHeight)
                    Dim c = TunnelTexture.GetPixel(newX, newY)
                    b.SetPixel(x, y, c)
                Next
            Next
            e.Graphics.DrawImage(b, 0, 0)
        End Using
    End Sub
End Class
