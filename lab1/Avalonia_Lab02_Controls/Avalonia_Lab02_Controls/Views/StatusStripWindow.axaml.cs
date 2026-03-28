using System;
using Avalonia.Controls;
using Avalonia.Threading;

namespace Avalonia_Lab02_Controls.Views;

/// <summary>
/// Упражнение 4: Строка состояния + таймер (аналог StatusStrip + Timer).
/// </summary>
public partial class StatusStripWindow : Window
{
    private readonly DispatcherTimer _timer;

    public StatusStripWindow()
    {
        InitializeComponent();

        // Инициализация таймера — обновляем время каждую секунду
        _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _timer.Tick += (_, _) => UpdateTime();
        _timer.Start();

        UpdateTime();

        // Останавливаем таймер при закрытии окна
        Closed += (_, _) => _timer.Stop();
    }

    // Обновить текст в строке состояния текущим временем
    private void UpdateTime()
    {
        LblStatus.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
    }
}
