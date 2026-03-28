namespace Lab03_CustomControls;

/// <summary>
/// Упражнение 1: Композитный элемент управления (UserControl).
/// Содержит три TrackBar (R, G, B) и панель предпросмотра цвета.
/// </summary>
public class ColorPicker : UserControl
{
    private readonly TrackBar _trackR;
    private readonly TrackBar _trackG;
    private readonly TrackBar _trackB;
    private readonly Panel _previewPanel;
    private readonly Label _lblR;
    private readonly Label _lblG;
    private readonly Label _lblB;
    private readonly Label _lblHex;

    public Color SelectedColor => _previewPanel.BackColor;

    public event EventHandler? ColorChanged;

    public ColorPicker()
    {
        Size = new Size(320, 200);

        _lblR = new Label { Text = "R:", Location = new Point(5, 10), AutoSize = true, ForeColor = Color.Red };
        _trackR = CreateTrackBar(30, 5);

        _lblG = new Label { Text = "G:", Location = new Point(5, 50), AutoSize = true, ForeColor = Color.Green };
        _trackG = CreateTrackBar(30, 45);

        _lblB = new Label { Text = "B:", Location = new Point(5, 90), AutoSize = true, ForeColor = Color.Blue };
        _trackB = CreateTrackBar(30, 85);

        _previewPanel = new Panel
        {
            Location = new Point(5, 130),
            Size = new Size(200, 40),
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = Color.Black
        };

        _lblHex = new Label
        {
            Location = new Point(210, 140),
            AutoSize = true,
            Text = "#000000"
        };

        Controls.AddRange(new Control[] { _lblR, _trackR, _lblG, _trackG, _lblB, _trackB, _previewPanel, _lblHex });
    }

    private TrackBar CreateTrackBar(int x, int y)
    {
        var tb = new TrackBar
        {
            Minimum = 0,
            Maximum = 255,
            Value = 0,
            TickFrequency = 32,
            Location = new Point(x, y),
            Size = new Size(280, 30)
        };
        tb.ValueChanged += OnTrackBarValueChanged;
        return tb;
    }

    private void OnTrackBarValueChanged(object? sender, EventArgs e)
    {
        var color = Color.FromArgb(_trackR.Value, _trackG.Value, _trackB.Value);
        _previewPanel.BackColor = color;
        _lblHex.Text = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        ColorChanged?.Invoke(this, EventArgs.Empty);
    }
}
