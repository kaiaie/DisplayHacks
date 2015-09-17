Public Class GlowWorm
    Protected Shared _r As New Random()

    Protected Const segmentRadius As Integer = 8
    Protected _parent As Control
    Protected _color As Color = Color.White
    Protected _deltaX As Integer
    Protected _deltaY As Integer
    Protected _segments(10) As Point

    Public Property Color() As Color
        Get
            Return _color
        End Get
        Set(ByVal value As Color)
            _color = value
        End Set
    End Property

    Private Sub SetTrajectory()
        Do
            _deltaX = _r.Next(0, 8) - 4
            _deltaY = _r.Next(0, 8) - 4
        Loop While _deltaX = 0 And _deltaY = 0
    End Sub

    Public Sub New(ByVal parent As Control)
        _parent = parent
        SetTrajectory()
        _segments(0) = New Point(parent.ClientRectangle.Width \ 2, parent.ClientRectangle.Height \ 2)
        For ii As Integer = 1 To 9
            _segments(ii) = New Point(_segments(ii).X - _deltaX, _segments(ii).Y - _deltaY)
        Next
        AddHandler parent.Paint, AddressOf Draw
        AddHandler parent.Click, AddressOf Tweak
    End Sub

    Public Sub New(ByVal parent As Control, ByVal wormColor As Color)
        Me.New(parent)
        Color = wormColor
    End Sub

    Public Sub Draw(ByVal sender As Object, ByVal e As PaintEventArgs)
        Using backgroundBrush As New SolidBrush(_parent.BackColor)
            ' Paint over the last segment
            e.Graphics.FillEllipse(
                backgroundBrush,
                _segments(9).X - segmentRadius,
                _segments(9).Y - segmentRadius,
                segmentRadius * 2,
                segmentRadius * 2
            )
            ' Update segments
            For ii As Integer = 9 To 1 Step -1
                _segments(ii) = _segments(ii - 1)
            Next
            _segments(0).X += _deltaX
            _segments(0).Y += _deltaY
            If _segments(0).X < 0 Or _segments(0).X > _parent.ClientRectangle.Width Then _deltaX *= -1
            If _segments(0).Y < 0 Or _segments(0).Y > _parent.ClientRectangle.Height Then _deltaY *= -1
            ' Redraw segments
            For ii As Integer = 0 To 9
                Using segmentBrush As New SolidBrush(Color.FromArgb(255 - (25 * ii), Color))
                    e.Graphics.FillEllipse(segmentBrush,
                    _segments(ii).X - segmentRadius,
                    _segments(ii).Y - segmentRadius,
                    segmentRadius * 2,
                    segmentRadius * 2
                )
                End Using
            Next
        End Using
    End Sub

    Public Sub Tweak(ByVal sender As Object, ByVal e As EventArgs)
        SetTrajectory()
    End Sub
End Class
