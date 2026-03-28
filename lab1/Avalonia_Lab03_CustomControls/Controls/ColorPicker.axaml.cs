using System;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Markup.Xaml;

namespace Avalonia_Lab03_CustomControls.Controls;

/// <summary>
/// Упражнение 1: Композитный элемент управления (UserControl).
/// Содержит три ползунка (R, G, B) и панель предпросмотра цвета.
/// </summary>
public partial class ColorPicker : UserControl
{
    // Текущий выбранный цвет
    public Color SelectedColor { get; private set; } = Colors.Black;

    // Событие, вызываемое при изменении цвета
    public event EventHandler? ColorChanged;

    public ColorPicker()
    {
        AvaloniaXamlLoader.Load(this);

        // Получаем ссылки на элементы из AXAML
        var sliderR = this.FindControl<Slider>("SliderR")!;
        var sliderG = this.FindControl<Slider>("SliderG")!;
        var sliderB = this.FindControl<Slider>("SliderB")!;

        // Подписываемся на изменение значений ползунков
        sliderR.PropertyChanged += (_, e) => { if (e.Property == Slider.ValueProperty) UpdateColor(); };
        sliderG.PropertyChanged += (_, e) => { if (e.Property == Slider.ValueProperty) UpdateColor(); };
        sliderB.PropertyChanged += (_, e) => { if (e.Property == Slider.ValueProperty) UpdateColor(); };
    }

    /// <summary>
    /// Обновляет предпросмотр цвета и hex-код при изменении ползунков.
    /// </summary>
    private void UpdateColor()
    {
        var sliderR = this.FindControl<Slider>("SliderR")!;
        var sliderG = this.FindControl<Slider>("SliderG")!;
        var sliderB = this.FindControl<Slider>("SliderB")!;
        var preview = this.FindControl<Border>("PreviewBorder")!;
        var lblHex = this.FindControl<TextBlock>("LblHex")!;
        var lblRVal = this.FindControl<TextBlock>("LblRValue")!;
        var lblGVal = this.FindControl<TextBlock>("LblGValue")!;
        var lblBVal = this.FindControl<TextBlock>("LblBValue")!;

        byte r = (byte)sliderR.Value;
        byte g = (byte)sliderG.Value;
        byte b = (byte)sliderB.Value;

        SelectedColor = Color.FromRgb(r, g, b);
        preview.Background = new SolidColorBrush(SelectedColor);
        lblHex.Text = $"#{r:X2}{g:X2}{b:X2}";
        lblRVal.Text = r.ToString();
        lblGVal.Text = g.ToString();
        lblBVal.Text = b.ToString();

        ColorChanged?.Invoke(this, EventArgs.Empty);
    }
}
