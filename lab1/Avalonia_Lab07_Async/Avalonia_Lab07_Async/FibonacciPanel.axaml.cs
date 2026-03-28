using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;

namespace Avalonia_Lab07_Async;

/// <summary>
/// Упр.5: Вычисление числа Фибоначчи в фоновом потоке.
/// Используется Task.Run + CancellationToken + Dispatcher для обновления UI.
/// </summary>
public partial class FibonacciPanel : UserControl
{
    private CancellationTokenSource? _cts;

    public FibonacciPanel()
    {
        InitializeComponent();
        StartBtn.Click += OnStart;
        CancelBtn.Click += OnCancel;
    }

    private async void OnStart(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(InputBox.Text, out int n) || n < 0)
        {
            StatusLabel.Text = "Ошибка: введите целое неотрицательное число";
            return;
        }

        StartBtn.IsEnabled = false;
        CancelBtn.IsEnabled = true;
        ProgressBar.Value = 0;
        ResultBox.Text = "";
        StatusLabel.Text = "Запуск...";

        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        try
        {
            // Вычисление Фибоначчи в фоновом потоке
            BigInteger result = await Task.Run(() => ComputeFibonacci(n, token), token);

            string s = result.ToString();
            StatusLabel.Text = $"Готово! Число содержит {s.Length} цифр.";
            ResultBox.Text = s;
        }
        catch (OperationCanceledException)
        {
            StatusLabel.Text = "Вычисление отменено";
            ResultBox.Text = "(отменено)";
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Ошибка: {ex.Message}";
            ResultBox.Text = ex.ToString();
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

    /// <summary>
    /// Вычисление N-го числа Фибоначчи с отчётом прогресса через Dispatcher.
    /// </summary>
    private BigInteger ComputeFibonacci(int n, CancellationToken token)
    {
        if (n <= 0) return BigInteger.Zero;
        if (n == 1) return BigInteger.One;

        BigInteger a = 0, b = 1;
        int lastPercent = 0;

        for (int i = 2; i <= n; i++)
        {
            token.ThrowIfCancellationRequested();

            BigInteger temp = a + b;
            a = b;
            b = temp;

            // Обновление прогресса через Dispatcher (из фонового потока в UI)
            int percent = (int)((long)i * 100 / n);
            if (percent > lastPercent)
            {
                lastPercent = percent;
                int p = percent;
                Dispatcher.UIThread.Post(() =>
                {
                    ProgressBar.Value = p;
                    StatusLabel.Text = $"Вычисление: {p}%";
                });
            }
        }

        return b;
    }
}
