using System.Drawing;
using System.Windows.Forms;

namespace Lab01_Forms;

/// <summary>
/// Упражнение 3: Форма-наследник BaseForm.
/// </summary>
public class ChildForm : BaseForm
{
    public ChildForm()
    {
        Text = "Дочерняя форма (наследник)";
        BackColor = Color.LightGreen;

        // Меняем текст унаследованной метки
        lblBase.Text = "Унаследованный элемент (из BaseForm)";

        // Добавляем собственные элементы
        var lblChild = new Label
        {
            Text = "Это элемент дочерней формы",
            Font = new Font("Segoe UI", 11),
            ForeColor = Color.DarkGreen,
            AutoSize = true,
            Location = new Point(20, 120)
        };
        Controls.Add(lblChild);

        var btnChild = new Button
        {
            Text = "Кнопка дочерней формы",
            Size = new Size(200, 35),
            Location = new Point(20, 160),
            BackColor = Color.PaleGreen
        };
        btnChild.Click += (_, _) =>
            MessageBox.Show("Нажата кнопка дочерней формы!", "Дочерняя форма");
        Controls.Add(btnChild);
    }
}
