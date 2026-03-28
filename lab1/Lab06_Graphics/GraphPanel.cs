namespace Lab06_Graphics;

public class GraphPanel : Panel
{
    public GraphPanel()
    {
        DoubleBuffered = true;
        BackColor = Color.White;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        int w = ClientSize.Width;
        int h = ClientSize.Height;
        if (w < 80 || h < 80) return;

        int margin = 50;
        int plotW = w - 2 * margin;
        int plotH = h - 2 * margin;

        // Coordinate ranges
        float xMin = -2 * (float)Math.PI;
        float xMax = 2 * (float)Math.PI;
        float yMin = -1.5f;
        float yMax = 1.5f;

        // Helper: math -> screen
        float ToScreenX(float x) => margin + (x - xMin) / (xMax - xMin) * plotW;
        float ToScreenY(float y) => margin + (yMax - y) / (yMax - yMin) * plotH;

        // Grid
        using var gridPen = new Pen(Color.FromArgb(230, 230, 230));
        using var axisPen = new Pen(Color.Black, 2);
        using var font = new Font("Segoe UI", 8);
        using var brush = new SolidBrush(Color.DimGray);

        // Vertical grid lines (at multiples of PI)
        for (int i = -2; i <= 2; i++)
        {
            float xv = i * (float)Math.PI;
            float sx = ToScreenX(xv);
            g.DrawLine(gridPen, sx, margin, sx, margin + plotH);
            string label = i switch
            {
                -2 => "-2π",
                -1 => "-π",
                0 => "0",
                1 => "π",
                2 => "2π",
                _ => ""
            };
            g.DrawString(label, font, brush, sx - 10, margin + plotH + 5);
        }

        // Horizontal grid lines
        for (float yv = -1f; yv <= 1f; yv += 0.5f)
        {
            float sy = ToScreenY(yv);
            g.DrawLine(gridPen, margin, sy, margin + plotW, sy);
            g.DrawString(yv.ToString("0.0"), font, brush, 5, sy - 7);
        }

        // Axes
        float axisY = ToScreenY(0);
        float axisX = ToScreenX(0);
        g.DrawLine(axisPen, margin, axisY, margin + plotW, axisY); // X axis
        g.DrawLine(axisPen, axisX, margin, axisX, margin + plotH); // Y axis

        // Draw y = sin(x)
        using var curvePen = new Pen(Color.Blue, 2);
        int steps = plotW;
        var points = new PointF[steps];
        for (int i = 0; i < steps; i++)
        {
            float x = xMin + (xMax - xMin) * i / (steps - 1);
            float y = (float)Math.Sin(x);
            points[i] = new PointF(ToScreenX(x), ToScreenY(y));
        }
        g.DrawLines(curvePen, points);

        // Title
        using var titleFont = new Font("Segoe UI", 12, FontStyle.Bold);
        g.DrawString("y = sin(x)", titleFont, Brushes.DarkBlue, margin, 5);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        Invalidate();
    }
}
