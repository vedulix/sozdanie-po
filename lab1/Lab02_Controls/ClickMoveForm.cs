using System.Drawing;
using System.Windows.Forms;

namespace Lab02_Controls;

/// <summary>
/// Упражнение 1: События Click и MouseMove.
/// </summary>
public class ClickMoveForm : Form
{
    private int _clickCount;
    private readonly Color[] _colors =
    {
        Color.Coral, Color.MediumSeaGreen, Color.DodgerBlue,
        Color.Gold, Color.MediumOrchid, Color.Tomato
    };

    public ClickMoveForm()
    {
        Text = "Click и MouseMove";
        Size = new Size(420, 300);
        StartPosition = FormStartPosition.CenterScreen;

        var btnClick = new Button
        {
            Text = "Нажми меня!",
            Size = new Size(200, 50),
            Location = new Point(110, 40),
            Font = new Font("Segoe UI", 12)
        };
        btnClick.Click += (_, _) =>
        {
            _clickCount++;
            btnClick.BackColor = _colors[_clickCount % _colors.Length];
            btnClick.Text = $"Нажатий: {_clickCount}";
        };
        Controls.Add(btnClick);

        var lblCoords = new Label
        {
            Text = "Координаты мыши: (-, -)",
            Font = new Font("Segoe UI", 11),
            AutoSize = true,
            Location = new Point(20, 130)
        };
        Controls.Add(lblCoords);

        MouseMove += (_, e) =>
            lblCoords.Text = $"Координаты мыши: ({e.X}, {e.Y})";
    }
}
