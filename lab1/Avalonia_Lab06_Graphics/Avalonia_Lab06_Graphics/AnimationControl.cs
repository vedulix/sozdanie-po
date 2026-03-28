using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;

namespace Avalonia_Lab06_Graphics;

/// <summary>
/// Пользовательский элемент для анимации прыгающего мяча.
/// Использует DispatcherTimer вместо System.Windows.Forms.Timer.
/// </summary>
public class AnimationControl : Control
{
    private double _x = 100, _y = 80;
    private double _dx = 4, _dy = 3;
    private const int BallRadius = 20;
    private readonly DispatcherTimer _timer;

    public AnimationControl()
    {
        // Запуск таймера анимации (~60 FPS)
        _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(16) };
        _timer.Tick += OnTimerTick;
        _timer.Start();
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        // Обновление позиции мяча
        _x += _dx;
        _y += _dy;

        double w = Bounds.Width;
        double h = Bounds.Height;
        if (w < 1 || h < 1) return;

        // Отскок от стен
        if (_x - BallRadius < 0) { _x = BallRadius; _dx = Math.Abs(_dx); }
        if (_x + BallRadius > w) { _x = w - BallRadius; _dx = -Math.Abs(_dx); }
        if (_y - BallRadius < 0) { _y = BallRadius; _dy = Math.Abs(_dy); }
        if (_y + BallRadius > h) { _y = h - BallRadius; _dy = -Math.Abs(_dy); }

        // Перерисовка элемента
        InvalidateVisual();
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        // Белый фон
        context.DrawRectangle(Brushes.White, null, new Rect(0, 0, Bounds.Width, Bounds.Height));

        // Тень мяча
        var shadowBrush = new SolidColorBrush(Color.FromArgb(60, 0, 0, 0));
        context.DrawEllipse(shadowBrush, null,
            new Point(_x + 3, _y + 3), BallRadius, BallRadius);

        // Сам мяч (оранжево-красный)
        context.DrawEllipse(Brushes.OrangeRed, null,
            new Point(_x, _y), BallRadius, BallRadius);

        // Блик на мяче
        var highlightBrush = new SolidColorBrush(Color.FromArgb(120, 255, 255, 255));
        context.DrawEllipse(highlightBrush, null,
            new Point(_x - 5, _y - 7), BallRadius * 0.5, BallRadius * 0.35);

        // Контур мяча
        var borderPen = new Pen(new SolidColorBrush(Color.FromRgb(139, 0, 0)), 1.5); // DarkRed
        context.DrawEllipse(null, borderPen,
            new Point(_x, _y), BallRadius, BallRadius);
    }

    // Остановка таймера при удалении элемента
    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        _timer.Stop();
        base.OnDetachedFromVisualTree(e);
    }
}
