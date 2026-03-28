namespace Lab07_Async;

public class AsyncAwaitPanel : Panel
{
    private readonly TextBox _dataBox;
    private readonly Button _loadBtn;
    private readonly Label _statusLabel;

    public AsyncAwaitPanel()
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
            Text = "Демонстрация async/await.\n" +
                   "Метод LoadDataAsync имитирует загрузку данных с сервера.\n" +
                   "UI остаётся отзывчивым во время ожидания.",
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 15)
        };

        _loadBtn = new Button { Text = "Загрузить данные", Width = 180 };

        _statusLabel = new Label
        {
            Text = "",
            AutoSize = true,
            Margin = new Padding(0, 10, 0, 5)
        };

        _dataBox = new TextBox
        {
            Multiline = true,
            ReadOnly = true,
            Width = 500,
            Height = 200,
            ScrollBars = ScrollBars.Vertical,
            Margin = new Padding(0, 5, 0, 0)
        };

        layout.Controls.Add(description);
        layout.Controls.Add(_loadBtn);
        layout.Controls.Add(_statusLabel);
        layout.Controls.Add(_dataBox);
        Controls.Add(layout);

        _loadBtn.Click += async (_, _) =>
        {
            _loadBtn.Enabled = false;
            _dataBox.Text = "Загрузка...";
            _statusLabel.Text = "Ожидание ответа сервера...";

            string data = await LoadDataAsync();

            _dataBox.Text = data;
            _statusLabel.Text = "Данные загружены!";
            _loadBtn.Enabled = true;
        };
    }

    private static async Task<string> LoadDataAsync()
    {
        // Имитация задержки сети
        await Task.Delay(2000);

        return
            "=== Данные с сервера ===\r\n\r\n" +
            "Пользователь: Иванов И.И.\r\n" +
            "Email: ivanov@example.com\r\n" +
            "Роль: Администратор\r\n\r\n" +
            "Последние заказы:\r\n" +
            "  1. Ноутбук — 75 000 ₽\r\n" +
            "  2. Монитор — 32 000 ₽\r\n" +
            "  3. Клавиатура — 5 500 ₽\r\n\r\n" +
            $"Время загрузки: {DateTime.Now:HH:mm:ss}";
    }
}
