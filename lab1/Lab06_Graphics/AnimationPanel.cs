namespace Lab06_Graphics;

public class AnimationPanel : Panel
{
    private float _x = 100, _y = 80;
    private float _dx = 4, _dy = 3;
    private const int BallRadius = 20;
    private readonly System.Windows.Forms.Timer _timer;

    public AnimationPanel()
    {
        DoubleBuffered = true;
        BackColor = Color.White;

        _timer = new System.Windows.Forms.Timer { Interval = 16 }; // ~60 FPS
        _timer.Tick += (_, _) =>
        {
            _x += _dx;
            _y += _dy;

            if (_x - BallRadius < 0) { _x = BallRadius; _dx = Math.Abs(_dx); }
            if (_x + BallRadius > ClientSize.Width) { _x = ClientSize.Width - BallRadius; _dx = -Math.Abs(_dx); }
            if (_y - BallRadius < 0) { _y = BallRadius; _dy = Math.Abs(_dy); }
            if (_y + BallRadius > ClientSize.Height) { _y = ClientSize.Height - BallRadius; _dy = -Math.Abs(_dy); }

            Invalidate();
        };
        _timer.Start();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        // Ball shadow
        using var shadowBrush = new SolidBrush(Color.FromArgb(60, 0, 0, 0));
        g.FillEllipse(shadowBrush, _x - BallRadius + 3, _y - BallRadius + 3, BallRadius * 2, BallRadius * 2);

        // Ball
        using var ballBrush = new SolidBrush(Color.OrangeRed);
        g.FillEllipse(ballBrush, _x - BallRadius, _y - BallRadius, BallRadius * 2, BallRadius * 2);

        // Highlight
        using var highlightBrush = new SolidBrush(Color.FromArgb(120, 255, 255, 255));
        g.FillEllipse(highlightBrush, _x - BallRadius + 5, _y - BallRadius + 3, BallRadius, BallRadius * 0.7f);

        // Border
        using var borderPen = new Pen(Color.DarkRed, 1.5f);
        g.DrawEllipse(borderPen, _x - BallRadius, _y - BallRadius, BallRadius * 2, BallRadius * 2);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) _timer.Dispose();
        base.Dispose(disposing);
    }
}
