using System.ComponentModel;

namespace Lab07_Async;

public class BackgroundWorkerPanel : Panel
{
    private readonly ProgressBar _progressBar;
    private readonly Label _label;
    private readonly Button _startBtn;
    private readonly Button _cancelBtn;
    private readonly BackgroundWorker _worker;

    public BackgroundWorkerPanel()
    {
        Padding = new Padding(20);

        var layout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoSize = true,
            Padding = new Padding(10)
        };

        var description = new Label
        {
            Text = "BackgroundWorker выполняет длительную операцию в фоновом потоке.\n" +
                   "ProgressBar обновляется через ReportProgress.",
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

        _label = new Label
        {
            Text = "Готово к запуску",
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 10)
        };

        var btnPanel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.LeftToRight,
            AutoSize = true
        };

        _startBtn = new Button { Text = "Старт", Width = 100 };
        _cancelBtn = new Button { Text = "Отмена", Width = 100, Enabled = false };

        btnPanel.Controls.Add(_startBtn);
        btnPanel.Controls.Add(_cancelBtn);

        layout.Controls.Add(description);
        layout.Controls.Add(_progressBar);
        layout.Controls.Add(_label);
        layout.Controls.Add(btnPanel);
        Controls.Add(layout);

        // BackgroundWorker setup
        _worker = new BackgroundWorker
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };

        _worker.DoWork += (_, e) =>
        {
            for (int i = 1; i <= 100; i++)
            {
                if (_worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Thread.Sleep(50);
                _worker.ReportProgress(i);
            }
        };

        _worker.ProgressChanged += (_, e) =>
        {
            _progressBar.Value = e.ProgressPercentage;
            _label.Text = $"Прогресс: {e.ProgressPercentage}%";
        };

        _worker.RunWorkerCompleted += (_, e) =>
        {
            _startBtn.Enabled = true;
            _cancelBtn.Enabled = false;
            if (e.Cancelled)
                _label.Text = "Операция отменена";
            else if (e.Error != null)
                _label.Text = $"Ошибка: {e.Error.Message}";
            else
                _label.Text = "Операция завершена!";
        };

        _startBtn.Click += (_, _) =>
        {
            _startBtn.Enabled = false;
            _cancelBtn.Enabled = true;
            _progressBar.Value = 0;
            _label.Text = "Выполнение...";
            _worker.RunWorkerAsync();
        };

        _cancelBtn.Click += (_, _) =>
        {
            _worker.CancelAsync();
            _cancelBtn.Enabled = false;
        };
    }
}
