using System.Drawing;
using System.Windows.Forms;

namespace Lab02_Controls;

/// <summary>
/// Упражнение 6: Простой ввод/вывод данных.
/// </summary>
public class IoForm : Form
{
    public IoForm()
    {
        Text = "Ввод/вывод данных";
        Size = new Size(400, 280);
        StartPosition = FormStartPosition.CenterScreen;

        int x = 20, y = 20, lblW = 80, ctrlW = 250;

        Controls.Add(new Label
        {
            Text = "Имя:", Location = new Point(x, y + 3), AutoSize = true
        });
        var txtName = new TextBox
        {
            Location = new Point(x + lblW, y), Width = ctrlW
        };
        Controls.Add(txtName);
        y += 35;

        Controls.Add(new Label
        {
            Text = "Возраст:", Location = new Point(x, y + 3), AutoSize = true
        });
        var txtAge = new TextBox
        {
            Location = new Point(x + lblW, y), Width = ctrlW
        };
        Controls.Add(txtAge);
        y += 45;

        var btnShow = new Button
        {
            Text = "Показать",
            Size = new Size(120, 35),
            Location = new Point(x + lblW, y)
        };
        Controls.Add(btnShow);
        y += 50;

        var lblResult = new Label
        {
            Text = "",
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            ForeColor = Color.DarkBlue,
            AutoSize = true,
            Location = new Point(x, y)
        };
        Controls.Add(lblResult);

        btnShow.Click += (_, _) =>
        {
            lblResult.Text = $"Вас зовут {txtName.Text}, вам {txtAge.Text} лет.";
        };
    }
}
