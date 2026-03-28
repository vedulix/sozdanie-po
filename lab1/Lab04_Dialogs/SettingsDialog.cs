namespace Lab04_Dialogs;

/// <summary>
/// Упражнение 2: Пользовательский модальный диалог настроек.
/// </summary>
public class SettingsDialog : Form
{
    private readonly TextBox _txtUsername;
    private readonly ComboBox _cmbTheme;

    public string UserName => _txtUsername.Text;
    public string Theme => _cmbTheme.SelectedItem?.ToString() ?? "Light";

    public SettingsDialog(string currentName, string currentTheme)
    {
        Text = "Настройки";
        Size = new Size(350, 200);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;

        var lblName = new Label { Text = "Имя пользователя:", Location = new Point(15, 20), AutoSize = true };
        _txtUsername = new TextBox { Location = new Point(150, 17), Width = 160, Text = currentName };

        var lblTheme = new Label { Text = "Тема:", Location = new Point(15, 55), AutoSize = true };
        _cmbTheme = new ComboBox
        {
            Location = new Point(150, 52),
            Width = 160,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        _cmbTheme.Items.AddRange(new object[] { "Light", "Dark" });
        _cmbTheme.SelectedItem = currentTheme;

        var btnOk = new Button
        {
            Text = "OK",
            DialogResult = DialogResult.OK,
            Location = new Point(130, 110),
            Size = new Size(80, 30)
        };

        var btnCancel = new Button
        {
            Text = "Отмена",
            DialogResult = DialogResult.Cancel,
            Location = new Point(220, 110),
            Size = new Size(80, 30)
        };

        AcceptButton = btnOk;
        CancelButton = btnCancel;

        Controls.AddRange(new Control[] { lblName, _txtUsername, lblTheme, _cmbTheme, btnOk, btnCancel });
    }
}
