namespace Lab08_Usability;

public class HelpPanel : Panel
{
    public HelpPanel()
    {
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
            Text = "Контекстная справка через HelpProvider.\n" +
                   "Нажмите F1 при фокусе на любом элементе, чтобы увидеть подсказку.",
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 20)
        };

        var helpProvider = new HelpProvider();

        var nameLabel = new Label { Text = "Имя:", AutoSize = true };
        var nameBox = new TextBox { Width = 300 };
        helpProvider.SetHelpString(nameBox, "Введите ваше полное имя (Фамилия Имя Отчество).");
        helpProvider.SetShowHelp(nameBox, true);

        var emailLabel = new Label { Text = "Email:", AutoSize = true, Margin = new Padding(0, 10, 0, 0) };
        var emailBox = new TextBox { Width = 300 };
        helpProvider.SetHelpString(emailBox, "Введите адрес электронной почты в формате user@domain.com.");
        helpProvider.SetShowHelp(emailBox, true);

        var phoneLabel = new Label { Text = "Телефон:", AutoSize = true, Margin = new Padding(0, 10, 0, 0) };
        var phoneBox = new TextBox { Width = 300 };
        helpProvider.SetHelpString(phoneBox, "Введите номер телефона в формате +7 (XXX) XXX-XX-XX.");
        helpProvider.SetShowHelp(phoneBox, true);

        var saveBtn = new Button { Text = "Сохранить", Width = 120, Margin = new Padding(0, 15, 0, 0) };
        helpProvider.SetHelpString(saveBtn, "Нажмите, чтобы сохранить введённые данные.");
        helpProvider.SetShowHelp(saveBtn, true);

        var clearBtn = new Button { Text = "Очистить", Width = 120 };
        helpProvider.SetHelpString(clearBtn, "Нажмите, чтобы очистить все поля формы.");
        helpProvider.SetShowHelp(clearBtn, true);

        saveBtn.Click += (_, _) => MessageBox.Show("Данные сохранены!", "Информация",
            MessageBoxButtons.OK, MessageBoxIcon.Information);

        clearBtn.Click += (_, _) =>
        {
            nameBox.Clear();
            emailBox.Clear();
            phoneBox.Clear();
        };

        layout.Controls.Add(description);
        layout.Controls.Add(nameLabel);
        layout.Controls.Add(nameBox);
        layout.Controls.Add(emailLabel);
        layout.Controls.Add(emailBox);
        layout.Controls.Add(phoneLabel);
        layout.Controls.Add(phoneBox);
        layout.Controls.Add(saveBtn);
        layout.Controls.Add(clearBtn);
        Controls.Add(layout);
    }
}
