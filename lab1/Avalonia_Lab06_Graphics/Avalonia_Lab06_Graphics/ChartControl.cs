using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia_Lab06_Graphics;

/// <summary>
/// Столбчатая диаграмма «Продажи по месяцам».
/// Рисуется через DrawingContext (аналог GDI+ Graphics).
/// </summary>
public class ChartControl : Control
{
    // Данные для диаграммы: название месяца, значение, цвет столбца
    private readonly (string Name, int Value, Color BarColor)[] _data =
    {
        ("\u042f\u043d\u0432", 50, Color.FromRgb(70, 130, 180)),   // SteelBlue
        ("\u0424\u0435\u0432", 80, Color.FromRgb(205, 92, 92)),    // IndianRed
        ("\u041c\u0430\u0440", 65, Color.FromRgb(46, 139, 87)),    // SeaGreen
        ("\u0410\u043f\u0440", 90, Color.FromRgb(255, 140, 0)),    // DarkOrange
        ("\u041c\u0430\u0439", 70, Color.FromRgb(147, 112, 219)),  // MediumPurple
    };

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        double w = Bounds.Width;
        double h = Bounds.Height;

        // Белый фон
        context.DrawRectangle(Brushes.White, null, new Rect(0, 0, w, h));

        if (w < 100 || h < 100) return;

        double margin = 60;
        double plotW = w - 2 * margin;
        double plotH = h - 2 * margin;
        int maxValue = 100;

        var axisPen = new Pen(Brushes.Black, 2);
        var gridPen = new Pen(new SolidColorBrush(Color.FromRgb(220, 220, 220)), 1);
        var textBrush = new SolidColorBrush(Color.FromRgb(105, 105, 105)); // DimGray

        // Заголовок диаграммы
        var title = CreateText("Продажи по месяцам", 12, Brushes.DarkBlue, true);
        context.DrawText(title, new Point(margin, 5));

        // Горизонтальные линии сетки и подписи по оси Y
        for (int v = 0; v <= maxValue; v += 20)
        {
            double y = margin + plotH - (double)v / maxValue * plotH;
            context.DrawLine(gridPen, new Point(margin, y), new Point(margin + plotW, y));

            var label = CreateText(v.ToString(), 9, textBrush);
            context.DrawText(label, new Point(5, y - label.Height / 2));
        }

        // Оси координат
        context.DrawLine(axisPen, new Point(margin, margin), new Point(margin, margin + plotH));        // Ось Y
        context.DrawLine(axisPen, new Point(margin, margin + plotH), new Point(margin + plotW, margin + plotH)); // Ось X

        // Отрисовка столбцов
        int barCount = _data.Length;
        double barWidth = plotW / barCount * 0.6;
        double gap = plotW / barCount * 0.4 / 2;

        for (int i = 0; i < barCount; i++)
        {
            var (name, value, color) = _data[i];
            double barH = (double)value / maxValue * plotH;
            double x = margin + plotW / barCount * i + gap;
            double y = margin + plotH - barH;

            // Заливка столбца
            var barBrush = new SolidColorBrush(color);
            context.DrawRectangle(barBrush, null, new Rect(x, y, barWidth, barH));

            // Рамка столбца
            var borderPen = new Pen(new SolidColorBrush(Color.FromArgb(80, 0, 0, 0)), 1);
            context.DrawRectangle(null, borderPen, new Rect(x, y, barWidth, barH));

            // Значение над столбцом
            var valText = CreateText(value.ToString(), 9, Brushes.Black);
            context.DrawText(valText, new Point(x + barWidth / 2 - valText.Width / 2, y - 18));

            // Подпись под столбцом (название месяца)
            var nameText = CreateText(name, 9, textBrush);
            context.DrawText(nameText, new Point(x + barWidth / 2 - nameText.Width / 2, margin + plotH + 5));
        }
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
