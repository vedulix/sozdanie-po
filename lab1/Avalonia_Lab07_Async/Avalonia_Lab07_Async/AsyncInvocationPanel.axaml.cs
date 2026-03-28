using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia_Lab07_Async;

/// <summary>
/// Упр.3: Асинхронная загрузка файла с IProgress.
/// Прогресс обновляется через IProgress&lt;int&gt;, который
/// автоматически маршалит вызовы в UI-поток.
/// </summary>
public partial class AsyncInvocationPanel : UserControl
{
    public AsyncInvocationPanel()
    {
        InitializeComponent();
        StartBtn.Click += OnStart;
    }

    private async void OnStart(object? sender, RoutedEventArgs e)
    {
        StartBtn.IsEnabled = false;
        ProgressBar.Value = 0;
        StatusLabel.Text = "Загрузка...";

        // Progress<T> захватывает SynchronizationContext и обновляет UI безопасно
        var progress = new Progress<int>(percent =>
        {
            ProgressBar.Value = percent;
            StatusLabel.Text = $"Загружено: {percent}%";
        });

        await SimulateDownloadAsync(progress);

        StatusLabel.Text = "Файл успешно загружен!";
        StartBtn.IsEnabled = true;
    }

    /// <summary>
    /// Имитация загрузки файла — каждые 50 мс увеличиваем прогресс на 2%.
    /// </summary>
    private static async Task SimulateDownloadAsync(IProgress<int> progress)
    {
        for (int i = 0; i <= 100; i += 2)
        {
            await Task.Delay(50);
            progress.Report(i);
        }
    }
}
