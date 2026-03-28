using System.Drawing;
using System.Windows.Forms;

namespace Lab01_Forms;

/// <summary>
/// Упражнение 3: Базовая форма для наследования.
/// </summary>
public class BaseForm : Form
{
    protected Label lblBase;
    protected Button btnBase;

    public BaseForm()
    {
        Text = "Базовая форма";
        Size = new Size(400, 300);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.Lavender;

        lblBase = new Label
        {
            Text = "Это элемент базовой формы",
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(20, 20)
        };
        Controls.Add(lblBase);

        btnBase = new Button
        {
            Text = "Кнопка базовой формы",
            Size = new Size(200, 35),
            Location = new Point(20, 60)
        };
        btnBase.Click += (_, _) =>
            MessageBox.Show("Нажата кнопка базовой формы!", "Базовая форма");
        Controls.Add(btnBase);
    }
}
