using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia_Lab06_Graphics;

/// <summary>
/// Пользовательский элемент управления для отрисовки графика y = sin(x).
/// Использует DrawingContext вместо GDI+ Graphics.
/// </summary>
public class GraphControl : Control
{
    public GraphControl()
    {
        // Фон задаётся через стиль; перерисовка при изменении размера — автоматически
        Background = Brushes.White;
    }

    // Свойство Background для заливки фона
    public static readonly StyledProperty<IBrush?> BackgroundProperty =
        Border.BackgroundProperty.AddOwner<GraphControl>();

    public IBrush? Background
    {
        get => GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        double w = Bounds.Width;
        double h = Bounds.Height;

        // Заливка фона белым цветом
        if (Background != null)
            context.DrawRectangle(Background, null, new Rect(0, 0, w, h));

        if (w < 80 || h < 80) return;

        double margin = 50;
        double plotW = w - 2 * margin;
        double plotH = h - 2 * margin;

        // Диапазон координат
        double xMin = -2 * Math.PI;
        double xMax = 2 * Math.PI;
        double yMin = -1.5;
        double yMax = 1.5;

        // Преобразование математических координат в экранные
        double ToScreenX(double x) => margin + (x - xMin) / (xMax - xMin) * plotW;
        double ToScreenY(double y) => margin + (yMax - y) / (yMax - yMin) * plotH;

        // Кисти и перья для рисования
        var gridPen = new Pen(new SolidColorBrush(Color.FromRgb(230, 230, 230)), 1);
        var axisPen = new Pen(Brushes.Black, 2);
        var textBrush = new SolidColorBrush(Color.FromRgb(105, 105, 105)); // DimGray

        // Вертикальные линии сетки (кратные PI)
        for (int i = -2; i <= 2; i++)
        {
            double xv = i * Math.PI;
            double sx = ToScreenX(xv);
            context.DrawLine(gridPen, new Point(sx, margin), new Point(sx, margin + plotH));

            string label = i switch
            {
                -2 => "-2\u03c0",
                -1 => "-\u03c0",
                0 => "0",
                1 => "\u03c0",
                2 => "2\u03c0",
                _ => ""
            };
            // Подпись под вертикальной линией
            var text = CreateText(label, 8, textBrush);
            context.DrawText(text, new Point(sx - text.Width / 2, margin + plotH + 5));
        }

        // Горизонтальные линии сетки
        for (double yv = -1.0; yv <= 1.01; yv += 0.5)
        {
            double sy = ToScreenY(yv);
            context.DrawLine(gridPen, new Point(margin, sy), new Point(margin + plotW, sy));

            // Подпись слева от горизонтальной линии
            var text = CreateText(yv.ToString("0.0"), 8, textBrush);
            context.DrawText(text, new Point(5, sy - text.Height / 2));
        }

        // Оси координат
        double axisY = ToScreenY(0);
        double axisX = ToScreenX(0);
        context.DrawLine(axisPen, new Point(margin, axisY), new Point(margin + plotW, axisY)); // Ось X
        context.DrawLine(axisPen, new Point(axisX, margin), new Point(axisX, margin + plotH)); // Ось Y

        // Построение кривой y = sin(x) с помощью StreamGeometry
        int steps = (int)plotW;
        if (steps < 2) return;

        var curvePen = new Pen(Brushes.Blue, 2);
        var geometry = new StreamGeometry();
        using (var ctx = geometry.Open())
        {
            double x0 = xMin;
            double y0 = Math.Sin(x0);
            ctx.BeginFigure(new Point(ToScreenX(x0), ToScreenY(y0)), false);

            for (int i = 1; i < steps; i++)
            {
                double x = xMin + (xMax - xMin) * i / (steps - 1);
                double y = Math.Sin(x);
                ctx.LineTo(new Point(ToScreenX(x), ToScreenY(y)));
            }

            ctx.EndFigure(false);
        }
        context.DrawGeometry(null, curvePen, geometry);

        // Заголовок графика
        var title = CreateText("y = sin(x)", 12, Brushes.DarkBlue, true);
        context.DrawText(title, new Point(margin, 5));
    }

    /// <summary>
    /// Вспомогательный метод для создания форматированного текста
    /// </summary>
    private static FormattedText CreateText(string text, double size, IBrush foreground, bool bold = false)
    {
        return new FormattedText(
            text,
            System.Globalization.CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight,
            new Typeface("Inter", FontStyle.Normal, bold ? FontWeight.Bold : FontWeight.Normal),
            size,
            foreground);
    }
}
