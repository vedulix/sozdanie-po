using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;

namespace Avalonia_Lab07_Async;

/// <summary>
/// Упр.1: Имитация BackgroundWorker через Task.Run + CancellationToken.
/// В Avalonia нет BackgroundWorker, поэтому используем Task.Run
/// с обновлением UI через Dispatcher.UIThread.
/// </summary>
public partial class BackgroundWorkerPanel : UserControl
{
    private CancellationTokenSource? _cts;

    public BackgroundWorkerPanel()
    {
        InitializeComponent();

        // Обработчик кнопки «Старт»
        StartBtn.Click += OnStart;
        // Обработчик кнопки «Отмена»
        CancelBtn.Click += OnCancel;
    }

    private async void OnStart(object? sender, RoutedEventArgs e)
    {
        StartBtn.IsEnabled = false;
        CancelBtn.IsEnabled = true;
        ProgressBar.Value = 0;
        StatusLabel.Text = "Выполнение...";

        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        try
        {
            // Запуск длительной операции в фоновом потоке
            await Task.Run(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(50);

                    int progress = i;
                    // Обновление UI из фонового потока через Dispatcher
                    Dispatcher.UIThread.Post(() =>
                    {
                        ProgressBar.Value = progress;
                        StatusLabel.Text = $"Прогресс: {progress}%";
                    });
                }
            }, token);

            StatusLabel.Text = "Операция завершена!";
        }
        catch (OperationCanceledException)
        {
            StatusLabel.Text = "Операция отменена";
        }
        finally
        {
            StartBtn.IsEnabled = true;
            CancelBtn.IsEnabled = false;
            _cts?.Dispose();
            _cts = null;
        }
    }

    private void OnCancel(object? sender, RoutedEventArgs e)
    {
        _cts?.Cancel();
        CancelBtn.IsEnabled = false;
    }
}
