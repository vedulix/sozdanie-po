namespace Lab07_Async;

public class AsyncInvocationPanel : Panel
{
    private readonly ProgressBar _progressBar;
    private readonly Label _statusLabel;
    private readonly Button _startBtn;

    public AsyncInvocationPanel()
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
            Text = "Имитация асинхронной загрузки файла.\n" +
                   "Используется IProgress<T> для обновления прогресса.",
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 15)
        };

        _progressBar = new ProgressBar
        {
            Width = 400,
            Height = 30,
            Minimum = 0,
            Maximum = 100,
            Margin = new Padding(0, 0, 0, 10)
        };

        _statusLabel = new Label
        {
            Text = "Нажмите «Загрузить» для начала",
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 10)
        };

        _startBtn = new Button { Text = "Загрузить файл", Width = 150 };

        layout.Controls.Add(description);
        layout.Controls.Add(_progressBar);
        layout.Controls.Add(_statusLabel);
        layout.Controls.Add(_startBtn);
        Controls.Add(layout);

        _startBtn.Click += async (_, _) =>
        {
            _startBtn.Enabled = false;
            _progressBar.Value = 0;
            _statusLabel.Text = "Загрузка...";

            var progress = new Progress<int>(percent =>
            {
                _progressBar.Value = percent;
                _statusLabel.Text = $"Загружено: {percent}%";
            });

            await SimulateDownloadAsync(progress);

            _statusLabel.Text = "Файл успешно загружен!";
            _startBtn.Enabled = true;
        };
    }

    private static async Task SimulateDownloadAsync(IProgress<int> progress)
    {
        for (int i = 0; i <= 100; i += 2)
        {
            await Task.Delay(50);
            progress.Report(i);
        }
    }
}
