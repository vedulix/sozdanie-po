namespace Lab04_Dialogs;

/// <summary>
/// Главная форма ЛР 4: демонстрация диалоговых окон.
/// </summary>
public class MainForm : Form
{
    private readonly TabControl _tabs;
    private string _userName = "Пользователь";
    private string _theme = "Light";

    public MainForm()
    {
        Text = "ЛР 4 — Использование окон диалога";
        Size = new Size(750, 550);
        StartPosition = FormStartPosition.CenterScreen;

        _tabs = new TabControl { Dock = DockStyle.Fill };
        Controls.Add(_tabs);

        _tabs.TabPages.Add(CreateStandardDialogsTab());
        _tabs.TabPages.Add(CreateCustomDialogTab());
        _tabs.TabPages.Add(CreatePersonTab());
        _tabs.TabPages.Add(CreateFindTab());
        _tabs.TabPages.Add(CreateCalculatorTab());
    }

    // ── Вкладка 1: Стандартные диалоги (Упражнение 1) ──
    private TabPage CreateStandardDialogsTab()
    {
        var page = new TabPage("Стандартные диалоги");

        var txtPath = new TextBox { Location = new Point(15, 15), Width = 500, ReadOnly = true };

        var richBox = new RichTextBox { Location = new Point(15, 80), Size = new Size(690, 300) };

        var btnOpen = new Button { Text = "Открыть файл", Location = new Point(15, 47), Size = new Size(120, 28) };
        btnOpen.Click += (_, _) =>
        {
            using var dlg = new OpenFileDialog { Filter = "Текстовые файлы|*.txt|Все файлы|*.*" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = dlg.FileName;
                richBox.Text = File.ReadAllText(dlg.FileName);
            }
        };

        var btnSave = new Button { Text = "Сохранить", Location = new Point(145, 47), Size = new Size(100, 28) };
        btnSave.Click += (_, _) =>
        {
            using var dlg = new SaveFileDialog { Filter = "Текстовые файлы|*.txt|Все файлы|*.*" };
            if (dlg.ShowDialog() == DialogResult.OK)
                File.WriteAllText(dlg.FileName, richBox.Text);
        };

        var btnColor = new Button { Text = "Цвет формы", Location = new Point(255, 47), Size = new Size(100, 28) };
        btnColor.Click += (_, _) =>
        {
            using var dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
                BackColor = dlg.Color;
        };

        var btnFont = new Button { Text = "Шрифт", Location = new Point(365, 47), Size = new Size(80, 28) };
        btnFont.Click += (_, _) =>
        {
            using var dlg = new FontDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
                richBox.Font = dlg.Font;
        };

        page.Controls.AddRange(new Control[] { txtPath, btnOpen, btnSave, btnColor, btnFont, richBox });
        return page;
    }

    // ── Вкладка 2: Пользовательский модальный диалог (Упражнение 2) ──
    private TabPage CreateCustomDialogTab()
    {
        var page = new TabPage("Настройки");

        var lblCurrent = new Label
        {
            Location = new Point(15, 15),
            AutoSize = true,
            Text = $"Пользователь: {_userName}, тема: {_theme}"
        };

        var btnSettings = new Button
        {
            Text = "Открыть настройки",
            Location = new Point(15, 45),
            Size = new Size(150, 30)
        };
        btnSettings.Click += (_, _) =>
        {
            using var dlg = new SettingsDialog(_userName, _theme);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _userName = dlg.UserName;
                _theme = dlg.Theme;
                lblCurrent.Text = $"Пользователь: {_userName}, тема: {_theme}";
                ApplyTheme();
            }
        };

        page.Controls.AddRange(new Control[] { lblCurrent, btnSettings });
        return page;
    }

    private void ApplyTheme()
    {
        if (_theme == "Dark")
        {
            BackColor = Color.FromArgb(45, 45, 48);
            ForeColor = Color.White;
        }
        else
        {
            BackColor = SystemColors.Control;
            ForeColor = SystemColors.ControlText;
        }
    }

    // ── Вкладка 3: Person (Упражнение 3) ──
    private TabPage CreatePersonTab()
    {
        var page = new TabPage("Person");

        var person = new Person { Name = "Иван", Age = 20 };
        var lblPerson = new Label
        {
            Location = new Point(15, 15),
            AutoSize = true,
            Text = person.ToString()
        };

        var btnEdit = new Button
        {
            Text = "Редактировать Person",
            Location = new Point(15, 45),
            Size = new Size(170, 30)
        };
        btnEdit.Click += (_, _) =>
        {
            using var dlg = new EditPersonDialog(person);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                person = dlg.PersonResult;
                lblPerson.Text = person.ToString();
            }
        };

        page.Controls.AddRange(new Control[] { lblPerson, btnEdit });
        return page;
    }

    // ── Вкладка 4: Немодальный поиск (Упражнение 4) ──
    private TabPage CreateFindTab()
    {
        var page = new TabPage("Поиск");

        var richBox = new RichTextBox
        {
            Location = new Point(15, 50),
            Size = new Size(690, 350),
            Text = "Пример текста для поиска.\nВведите текст и нажмите «Поиск».\n" +
                   "Совпадения будут подсвечены жёлтым цветом.\n" +
                   "Этот текстовый редактор демонстрирует немодальный диалог поиска."
        };

        FindDialog? findDialog = null;

        var btnFind = new Button
        {
            Text = "Открыть поиск",
            Location = new Point(15, 15),
            Size = new Size(130, 28)
        };
        btnFind.Click += (_, _) =>
        {
            findDialog ??= new FindDialog(richBox);
            findDialog.Show();
            findDialog.BringToFront();
        };

        page.Controls.AddRange(new Control[] { btnFind, richBox });
        return page;
    }

    // ── Вкладка 5: Калькулятор функций (Упражнение 5) ──
    private TabPage CreateCalculatorTab()
    {
        var page = new TabPage("Калькулятор");

        int y = 15;
        var lblA = new Label { Text = "a =", Location = new Point(15, y + 3), AutoSize = true };
        var txtA = new TextBox { Location = new Point(60, y), Width = 80, Text = "1" };

        var lblB = new Label { Text = "b =", Location = new Point(160, y + 3), AutoSize = true };
        var txtB = new TextBox { Location = new Point(200, y), Width = 80, Text = "0" };

        var lblX = new Label { Text = "x =", Location = new Point(300, y + 3), AutoSize = true };
        var txtX = new TextBox { Location = new Point(340, y), Width = 80, Text = "1" };
        y += 35;

        var lblFunc = new Label { Text = "Функция:", Location = new Point(15, y + 3), AutoSize = true };
        var cmbFunc = new ComboBox
        {
            Location = new Point(90, y),
            Width = 150,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        cmbFunc.Items.AddRange(new object[] { "sin(ax+b)", "cos(ax+b)", "exp(ax+b)", "ln(ax+b)" });
        cmbFunc.SelectedIndex = 0;
        y += 35;

        var btnCalc = new Button { Text = "Вычислить", Location = new Point(15, y), Size = new Size(100, 30) };

        var lblResult = new Label
        {
            Location = new Point(15, y + 45),
            AutoSize = true,
            Font = new Font("Segoe UI", 14, FontStyle.Bold)
        };

        btnCalc.Click += (_, _) =>
        {
            if (!double.TryParse(txtA.Text, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out double a) ||
                !double.TryParse(txtB.Text, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out double b) ||
                !double.TryParse(txtX.Text, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out double x))
            {
                lblResult.Text = "Ошибка: введите числовые значения";
                lblResult.ForeColor = Color.Red;
                return;
            }

            double arg = a * x + b;
            double result;
            string funcName = cmbFunc.SelectedItem!.ToString()!;

            try
            {
                result = funcName switch
                {
                    "sin(ax+b)" => Math.Sin(arg),
                    "cos(ax+b)" => Math.Cos(arg),
                    "exp(ax+b)" => Math.Exp(arg),
                    "ln(ax+b)" => Math.Log(arg),
                    _ => double.NaN
                };

                lblResult.ForeColor = Color.DarkBlue;
                lblResult.Text = $"{funcName} = {result:F6}  (при a={a}, b={b}, x={x})";
            }
            catch (Exception ex)
            {
                lblResult.Text = $"Ошибка вычисления: {ex.Message}";
                lblResult.ForeColor = Color.Red;
            }
        };

        page.Controls.AddRange(new Control[]
        {
            lblA, txtA, lblB, txtB, lblX, txtX,
            lblFunc, cmbFunc, btnCalc, lblResult
        });
        return page;
    }
}
