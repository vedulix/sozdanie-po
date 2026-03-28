using System.Drawing;
using System.Windows.Forms;

namespace Lab02_Controls;

/// <summary>
/// Главная форма ЛР2 с кнопками для открытия упражнений.
/// </summary>
public class MainForm : Form
{
    public MainForm()
    {
        Text = "Лаб 2 - Элементы управления";
        Size = new Size(460, 480);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.AliceBlue;

        var lblTitle = new Label
        {
            Text = "Работа с элементами управления",
            Font = new Font("Segoe UI", 16, FontStyle.Bold),
            AutoSize = true
        };
        Controls.Add(lblTitle);
        lblTitle.Location = new Point(
            (ClientSize.Width - lblTitle.PreferredWidth) / 2, 15);

        int y = 70, btnW = 350;
        var exercises = new (string title, System.Func<Form> factory)[]
        {
            ("Упр. 1: Click и MouseMove", () => new ClickMoveForm()),
            ("Упр. 2: Работа со списками", () => new ListForm()),
            ("Упр. 3: ToolStrip", () => new ToolStripForm()),
            ("Упр. 4: StatusStrip + Timer", () => new StatusStripForm()),
            ("Упр. 6: Ввод/вывод данных", () => new IoForm()),
            ("Упр. 8: Валидация (KeyPress + ErrorProvider)", () => new ValidationForm()),
            ("Упр. 9: Адресная книга (DataGridView)", () => new AddressBookForm()),
        };

        foreach (var (title, factory) in exercises)
        {
            var btn = new Button
            {
                Text = title,
                Size = new Size(btnW, 38),
                Location = new Point((ClientSize.Width - btnW) / 2, y)
            };
            var f = factory; // capture
            btn.Click += (_, _) => f().Show();
            Controls.Add(btn);
            y += 48;
        }
    }
}
