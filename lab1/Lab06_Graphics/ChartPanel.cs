namespace Lab06_Graphics;

public class ChartPanel : Panel
{
    private readonly (string Name, int Value, Color BarColor)[] _data =
    {
        ("Янв", 50, Color.SteelBlue),
        ("Фев", 80, Color.IndianRed),
        ("Мар", 65, Color.SeaGreen),
        ("Апр", 90, Color.DarkOrange),
        ("Май", 70, Color.MediumPurple),
    };

    public ChartPanel()
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
        if (w < 100 || h < 100) return;

        int margin = 60;
        int plotW = w - 2 * margin;
        int plotH = h - 2 * margin;
        int maxValue = 100;

        using var axisPen = new Pen(Color.Black, 2);
        using var gridPen = new Pen(Color.FromArgb(220, 220, 220));
        using var font = new Font("Segoe UI", 9);
        using var titleFont = new Font("Segoe UI", 12, FontStyle.Bold);
        using var textBrush = new SolidBrush(Color.DimGray);

        // Title
        g.DrawString("Продажи по месяцам", titleFont, Brushes.DarkBlue, margin, 5);

        // Y axis grid & labels
        for (int v = 0; v <= maxValue; v += 20)
        {
            float y = margin + plotH - (float)v / maxValue * plotH;
            g.DrawLine(gridPen, margin, y, margin + plotW, y);
            g.DrawString(v.ToString(), font, textBrush, 5, y - 8);
        }

        // Axes
        g.DrawLine(axisPen, margin, margin, margin, margin + plotH);
        g.DrawLine(axisPen, margin, margin + plotH, margin + plotW, margin + plotH);

        // Bars
        int barCount = _data.Length;
        float barWidth = (float)plotW / barCount * 0.6f;
        float gap = (float)plotW / barCount * 0.4f / 2;

        for (int i = 0; i < barCount; i++)
        {
            var (name, value, color) = _data[i];
            float barH = (float)value / maxValue * plotH;
            float x = margin + (float)plotW / barCount * i + gap;
            float y = margin + plotH - barH;

            using var barBrush = new SolidBrush(color);
            g.FillRectangle(barBrush, x, y, barWidth, barH);
            using var borderPen2 = new Pen(Color.FromArgb(80, 0, 0, 0));
            g.DrawRectangle(borderPen2, x, y, barWidth, barH);

            // Value on top
            g.DrawString(value.ToString(), font, Brushes.Black, x + barWidth / 2 - 10, y - 18);

            // Label below
            g.DrawString(name, font, textBrush, x + barWidth / 2 - 12, margin + plotH + 5);
        }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        Invalidate();
    }
}
