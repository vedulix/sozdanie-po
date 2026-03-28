namespace Lab03_CustomControls;

/// <summary>
/// Главная форма ЛР 3: демонстрация пользовательских элементов управления.
/// </summary>
public class MainForm : Form
{
    private readonly TabControl _tabs;

    public MainForm()
    {
        Text = "ЛР 3 — Создание элементов управления";
        Size = new Size(700, 550);
        StartPosition = FormStartPosition.CenterScreen;

        _tabs = new TabControl { Dock = DockStyle.Fill };
        Controls.Add(_tabs);

        _tabs.TabPages.Add(CreateColorPickerTab());
        _tabs.TabPages.Add(CreateHighlightButtonTab());
        _tabs.TabPages.Add(CreateValidationTab());
    }

    // ── Вкладка 1: ColorPicker (Упражнение 1) ──
    private TabPage CreateColorPickerTab()
    {
        var page = new TabPage("ColorPicker");

        var picker = new ColorPicker { Location = new Point(20, 20) };

        var lblInfo = new Label
        {
            Location = new Point(20, 230),
            AutoSize = true,
            Text = "Перемещайте ползунки для выбора цвета"
        };

        picker.ColorChanged += (_, _) =>
            lblInfo.Text = $"Выбранный цвет: {picker.SelectedColor.Name}";

        page.Controls.AddRange(new Control[] { picker, lblInfo });
        return page;
    }

    // ── Вкладка 2: HighlightButton (Упражнение 3) ──
    private TabPage CreateHighlightButtonTab()
    {
        var page = new TabPage("HighlightButton");

        var info = new Label
        {
            Text = "Наведите курсор на кнопки — они подсвечиваются:",
            Location = new Point(20, 15),
            AutoSize = true
        };
        page.Controls.Add(info);

        var colors = new[] { Color.LightCoral, Color.LightGreen, Color.LightSkyBlue, Color.Khaki };
        for (int i = 0; i < 4; i++)
        {
            var btn = new HighlightButton
            {
                Text = $"Кнопка {i + 1}",
                Location = new Point(20 + i * 140, 50),
                Size = new Size(120, 40),
                HighlightColor = colors[i]
            };
            page.Controls.Add(btn);
        }

        return page;
    }

    // ── Вкладка 3: Форма валидации данных (Упражнение 4) ──
    private TabPage CreateValidationTab()
    {
        var page = new TabPage("Валидация");
        var errorProvider = new ErrorProvider { BlinkStyle = ErrorBlinkStyle.NeverBlink };

        int y = 15;

        var lblName = new Label { Text = "Имя:", Location = new Point(20, y + 3), AutoSize = true };
        var txtName = new TextBox { Location = new Point(120, y), Width = 200 };
        y += 35;

        var lblEmail = new Label { Text = "Email:", Location = new Point(20, y + 3), AutoSize = true };
        var txtEmail = new TextBox { Location = new Point(120, y), Width = 200 };
        y += 35;

        var lblPhone = new Label { Text = "Телефон:", Location = new Point(20, y + 3), AutoSize = true };
        var txtPhone = new TextBox { Location = new Point(120, y), Width = 200 };
        y += 35;

        var lblAge = new Label { Text = "Возраст:", Location = new Point(20, y + 3), AutoSize = true };
        var txtAge = new NumericTextBox { Location = new Point(120, y), Width = 200 };
        y += 35;

        var lblResult = new Label
        {
            Location = new Point(20, y + 45),
            AutoSize = true,
            ForeColor = Color.DarkGreen
        };

        var btnValidate = new HighlightButton
        {
            Text = "Проверить",
            Location = new Point(120, y),
            Size = new Size(120, 30),
            HighlightColor = Color.LightGreen
        };

        btnValidate.Click += (_, _) =>
        {
            bool valid = true;
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorProvider.SetError(txtName, "Введите имя");
                valid = false;
            }

            if (!txtEmail.Text.Contains('@') || !txtEmail.Text.Contains('.'))
            {
                errorProvider.SetError(txtEmail, "Некорректный email");
                valid = false;
            }

            if (txtPhone.Text.Length < 6)
            {
                errorProvider.SetError(txtPhone, "Телефон слишком короткий");
                valid = false;
            }

            if (txtAge.Value < 1 || txtAge.Value > 150)
            {
                errorProvider.SetError(txtAge, "Возраст от 1 до 150");
                valid = false;
            }

            lblResult.Text = valid
                ? $"Данные корректны: {txtName.Text}, {txtEmail.Text}, {txtPhone.Text}, возраст {txtAge.Value}"
                : "Исправьте ошибки и повторите";
            lblResult.ForeColor = valid ? Color.DarkGreen : Color.Red;
        };

        page.Controls.AddRange(new Control[]
        {
            lblName, txtName, lblEmail, txtEmail,
            lblPhone, txtPhone, lblAge, txtAge,
            btnValidate, lblResult
        });

        return page;
    }
}
