using System.Drawing;
using System.Windows.Forms;

namespace Lab01_Forms;

/// <summary>
/// Упражнение 4: Дочерняя MDI-форма.
/// </summary>
public class MdiChildForm : Form
{
    public MdiChildForm(int number)
    {
        Text = $"Дочернее окно #{number}";
        Size = new Size(300, 200);
        BackColor = Color.FromArgb(
            200 + number * 10 % 55,
            220 + number * 15 % 35,
            255);

        var lbl = new Label
        {
            Text = $"Это дочернее MDI-окно #{number}",
            Font = new Font("Segoe UI", 11),
            AutoSize = true,
            Location = new Point(20, 20)
        };
        Controls.Add(lbl);
    }
}
