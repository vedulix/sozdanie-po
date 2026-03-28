using System.Drawing;
using System.Windows.Forms;

namespace Lab01_Forms;

/// <summary>
/// Упражнение 1: Прямоугольная форма с настроенными свойствами.
/// Главная форма приложения с кнопками для открытия остальных форм.
/// </summary>
public class MainForm : Form
{
    public MainForm()
    {
        // --- Упражнение 1: свойства формы ---
        Text = "Лаб 1 - Формы";
        Size = new Size(500, 400);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.LightBlue;

        // Приветственная надпись
        var lblWelcome = new Label
        {
            Text = "Добро пожаловать!",
            Font = new Font("Segoe UI", 18, FontStyle.Bold),
            AutoSize = true
        };
        Controls.Add(lblWelcome);
        // Центрируем после добавления на форму
        lblWelcome.Location = new Point(
            (ClientSize.Width - lblWelcome.PreferredWidth) / 2, 20);

        // --- Кнопки для открытия остальных форм ---
        int y = 80;
        int btnWidth = 320;

        var btnRound = new Button
        {
            Text = "Упр. 2: Круглая форма",
            Size = new Size(btnWidth, 35),
            Location = new Point((ClientSize.Width - btnWidth) / 2, y)
        };
        btnRound.Click += (_, _) => new RoundForm().Show();
        Controls.Add(btnRound);
        y += 50;

        var btnInherited = new Button
        {
            Text = "Упр. 3: Наследуемая форма",
            Size = new Size(btnWidth, 35),
            Location = new Point((ClientSize.Width - btnWidth) / 2, y)
        };
        btnInherited.Click += (_, _) => new ChildForm().Show();
        Controls.Add(btnInherited);
        y += 50;

        var btnMdi = new Button
        {
            Text = "Упр. 4: MDI-приложение",
            Size = new Size(btnWidth, 35),
            Location = new Point((ClientSize.Width - btnWidth) / 2, y)
        };
        btnMdi.Click += (_, _) => new MdiParentForm().Show();
        Controls.Add(btnMdi);
        y += 50;

        var btnRegistration = new Button
        {
            Text = "Упр. 5: Форма регистрации",
            Size = new Size(btnWidth, 35),
            Location = new Point((ClientSize.Width - btnWidth) / 2, y)
        };
        btnRegistration.Click += (_, _) => new RegistrationForm().Show();
        Controls.Add(btnRegistration);
    }
}
