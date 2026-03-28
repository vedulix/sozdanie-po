using System.Globalization;
using System.Resources;

namespace Lab08_Usability;

public class LocalizationPanel : Panel
{
    private readonly ResourceManager _rm;
    private readonly Label _greetingLabel;
    private readonly Label _nameLabel;
    private readonly Label _emailLabel;
    private readonly Label _langLabel;
    private readonly Label _statusLabel;
    private readonly Button _saveBtn;
    private readonly Button _cancelBtn;
    private readonly TextBox _nameBox;
    private readonly TextBox _emailBox;

    public LocalizationPanel()
    {
        _rm = new ResourceManager("Lab08_Usability.Resources.Strings",
            typeof(LocalizationPanel).Assembly);

        Padding = new Padding(20);

        var layout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Padding = new Padding(10)
        };

        var description = new Label
        {
            Text = "Переключение языка интерфейса через ресурсные файлы (.resx).\n" +
                   "Выберите язык в ComboBox ниже — все подписи обновятся.",
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 15)
        };

        // Language selector
        var langRow = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.LeftToRight,
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 15)
        };
        _langLabel = new Label { Text = "Язык:", AutoSize = true, Margin = new Padding(0, 5, 5, 0) };
        var langCombo = new ComboBox
        {
            Width = 200,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        langCombo.Items.Add("Русский (по умолчанию)");
        langCombo.Items.Add("English");
        langCombo.SelectedIndex = 0;
        langRow.Controls.Add(_langLabel);
        langRow.Controls.Add(langCombo);

        _greetingLabel = new Label
        {
            AutoSize = true,
            Font = new Font("Segoe UI", 16, FontStyle.Bold),
            Margin = new Padding(0, 0, 0, 15)
        };

        _nameLabel = new Label { AutoSize = true };
        _nameBox = new TextBox { Width = 300 };

        _emailLabel = new Label { AutoSize = true, Margin = new Padding(0, 10, 0, 0) };
        _emailBox = new TextBox { Width = 300 };

        var btnRow = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.LeftToRight,
            AutoSize = true,
            Margin = new Padding(0, 15, 0, 0)
        };
        _saveBtn = new Button { Width = 120 };
        _cancelBtn = new Button { Width = 120 };
        btnRow.Controls.Add(_saveBtn);
        btnRow.Controls.Add(_cancelBtn);

        _statusLabel = new Label
        {
            AutoSize = true,
            ForeColor = Color.Green,
            Margin = new Padding(0, 10, 0, 0)
        };

        layout.Controls.Add(description);
        layout.Controls.Add(langRow);
        layout.Controls.Add(_greetingLabel);
        layout.Controls.Add(_nameLabel);
        layout.Controls.Add(_nameBox);
        layout.Controls.Add(_emailLabel);
        layout.Controls.Add(_emailBox);
        layout.Controls.Add(btnRow);
        layout.Controls.Add(_statusLabel);
        Controls.Add(layout);

        // Apply initial locale
        ApplyLocale(CultureInfo.CurrentUICulture);

        langCombo.SelectedIndexChanged += (_, _) =>
        {
            var culture = langCombo.SelectedIndex == 1
                ? new CultureInfo("en")
                : new CultureInfo("ru");
            ApplyLocale(culture);
        };

        _saveBtn.Click += (_, _) =>
        {
            var culture = Thread.CurrentThread.CurrentUICulture;
            string msg = _rm.GetString("SavedMessage", culture) ?? "Saved!";
            _statusLabel.Text = msg;
        };

        _cancelBtn.Click += (_, _) =>
        {
            _nameBox.Clear();
            _emailBox.Clear();
            _statusLabel.Text = "";
        };
    }

    private void ApplyLocale(CultureInfo culture)
    {
        Thread.CurrentThread.CurrentUICulture = culture;

        _greetingLabel.Text = _rm.GetString("Greeting", culture) ?? "Welcome!";
        _nameLabel.Text = _rm.GetString("NameLabel", culture) ?? "Name:";
        _emailLabel.Text = _rm.GetString("EmailLabel", culture) ?? "Email:";
        _langLabel.Text = _rm.GetString("LanguageLabel", culture) ?? "Language:";
        _saveBtn.Text = _rm.GetString("SaveButton", culture) ?? "Save";
        _cancelBtn.Text = _rm.GetString("CancelButton", culture) ?? "Cancel";
        _statusLabel.Text = _rm.GetString("StatusReady", culture) ?? "Ready";
    }
}
