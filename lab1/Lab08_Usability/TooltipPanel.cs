namespace Lab08_Usability;

public class TooltipPanel : Panel
{
    public TooltipPanel()
    {
        Padding = new Padding(20);

        var layout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Padding = new Padding(10)
        };

        var toolTip = new ToolTip
        {
            AutoPopDelay = 5000,
            InitialDelay = 500,
            ReshowDelay = 200,
            ShowAlways = true
        };

        var description = new Label
        {
            Text = "Наведите курсор на любой элемент, чтобы увидеть всплывающую подсказку.",
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 20)
        };

        var nameLabel = new Label { Text = "Имя пользователя:", AutoSize = true };
        var nameBox = new TextBox { Width = 300 };
        toolTip.SetToolTip(nameBox, "Введите имя пользователя (от 3 до 50 символов)");

        var passLabel = new Label { Text = "Пароль:", AutoSize = true, Margin = new Padding(0, 10, 0, 0) };
        var passBox = new TextBox { Width = 300, UseSystemPasswordChar = true };
        toolTip.SetToolTip(passBox, "Пароль должен содержать не менее 8 символов, включая цифры и буквы");

        var rememberCheck = new CheckBox
        {
            Text = "Запомнить меня",
            AutoSize = true,
            Margin = new Padding(0, 10, 0, 0)
        };
        toolTip.SetToolTip(rememberCheck, "Установите, чтобы оставаться в системе после закрытия приложения");

        var loginBtn = new Button { Text = "Войти", Width = 120, Margin = new Padding(0, 15, 0, 0) };
        toolTip.SetToolTip(loginBtn, "Нажмите для входа в систему с указанными учётными данными");

        var registerBtn = new Button { Text = "Регистрация", Width = 120 };
        toolTip.SetToolTip(registerBtn, "Нажмите для создания нового аккаунта");

        var forgotLink = new LinkLabel
        {
            Text = "Забыли пароль?",
            AutoSize = true,
            Margin = new Padding(0, 10, 0, 0)
        };
        toolTip.SetToolTip(forgotLink, "Нажмите для восстановления пароля по email");

        loginBtn.Click += (_, _) =>
        {
            if (string.IsNullOrWhiteSpace(nameBox.Text) || string.IsNullOrWhiteSpace(passBox.Text))
                MessageBox.Show("Заполните все поля!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Вход выполнен!", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        };

        layout.Controls.Add(description);
        layout.Controls.Add(nameLabel);
        layout.Controls.Add(nameBox);
        layout.Controls.Add(passLabel);
        layout.Controls.Add(passBox);
        layout.Controls.Add(rememberCheck);
        layout.Controls.Add(loginBtn);
        layout.Controls.Add(registerBtn);
        layout.Controls.Add(forgotLink);
        Controls.Add(layout);
    }
}
