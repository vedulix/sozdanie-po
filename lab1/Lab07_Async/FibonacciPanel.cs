using System.ComponentModel;
using System.Numerics;

namespace Lab07_Async;

public class FibonacciPanel : Panel
{
    private readonly TextBox _inputBox;
    private readonly ProgressBar _progressBar;
    private readonly Label _statusLabel;
    private readonly TextBox _resultBox;
    private readonly Button _startBtn;
    private readonly Button _cancelBtn;
    private readonly BackgroundWorker _worker;

    public FibonacciPanel()
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
            Text = "Вычисление числа Фибоначчи в фоновом потоке.\n" +
                   "Введите N (порядковый номер числа), нажмите «Вычислить».\n" +
                   "Рекомендуемые значения: 100–10000.",
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 15)
        };

        var inputLabel = new Label { Text = "N =", AutoSize = true };
        _inputBox = new TextBox { Width = 200, Text = "1000" };

        var btnPanel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.LeftToRight,
            AutoSize = true,
            Margin = new Padding(0, 10, 0, 0)
        };
        _startBtn = new Button { Text = "Вычислить", Width = 120 };
        _cancelBtn = new Button { Text = "Отмена", Width = 100, Enabled = false };
        btnPanel.Controls.Add(_startBtn);
        btnPanel.Controls.Add(_cancelBtn);

        _progressBar = new ProgressBar
        {
            Width = 400,
            Height = 25,
            Minimum = 0,
            Maximum = 100,
            Margin = new Padding(0, 10, 0, 5)
        };

        _statusLabel = new Label
        {
            Text = "Готово",
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 5)
        };

        _resultBox = new TextBox
        {
            Multiline = true,
            ReadOnly = true,
            Width = 500,
            Height = 150,
            ScrollBars = ScrollBars.Vertical,
            Margin = new Padding(0, 5, 0, 0)
        };

        layout.Controls.Add(description);
        layout.Controls.Add(inputLabel);
        layout.Controls.Add(_inputBox);
        layout.Controls.Add(btnPanel);
        layout.Controls.Add(_progressBar);
        layout.Controls.Add(_statusLabel);
        layout.Controls.Add(_resultBox);
        Controls.Add(layout);

        // BackgroundWorker
        _worker = new BackgroundWorker
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };

        _worker.DoWork += (_, e) =>
        {
            int n = (int)e.Argument!;
            if (n <= 0) { e.Result = BigInteger.Zero; return; }
            if (n == 1) { e.Result = BigInteger.One; return; }

            BigInteger a = 0, b = 1;
            int lastPercent = 0;
            for (int i = 2; i <= n; i++)
            {
                if (_worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                BigInteger temp = a + b;
                a = b;
                b = temp;

                int percent = (int)((long)i * 100 / n);
                if (percent > lastPercent)
                {
                    lastPercent = percent;
                    _worker.ReportProgress(percent);
                }
            }
            e.Result = b;
        };

        _worker.ProgressChanged += (_, e) =>
        {
            _progressBar.Value = e.ProgressPercentage;
            _statusLabel.Text = $"Вычисление: {e.ProgressPercentage}%";
        };

        _worker.RunWorkerCompleted += (_, e) =>
        {
            _startBtn.Enabled = true;
            _cancelBtn.Enabled = false;
            if (e.Cancelled)
            {
                _statusLabel.Text = "Вычисление отменено";
                _resultBox.Text = "(отменено)";
            }
            else if (e.Error != null)
            {
                _statusLabel.Text = $"Ошибка: {e.Error.Message}";
                _resultBox.Text = e.Error.ToString();
            }
            else
            {
                var result = (BigInteger)e.Result!;
                string s = result.ToString();
                _statusLabel.Text = $"Готово! Число содержит {s.Length} цифр.";
                _resultBox.Text = s;
            }
        };

        _startBtn.Click += (_, _) =>
        {
            if (!int.TryParse(_inputBox.Text, out int n) || n < 0)
            {
                MessageBox.Show("Введите целое неотрицательное число.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _startBtn.Enabled = false;
            _cancelBtn.Enabled = true;
            _progressBar.Value = 0;
            _resultBox.Text = "";
            _statusLabel.Text = "Запуск...";
            _worker.RunWorkerAsync(n);
        };

        _cancelBtn.Click += (_, _) =>
        {
            _worker.CancelAsync();
            _cancelBtn.Enabled = false;
        };
    }
}
