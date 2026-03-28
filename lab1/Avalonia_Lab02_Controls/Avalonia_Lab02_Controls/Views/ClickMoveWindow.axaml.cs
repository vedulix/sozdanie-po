using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Avalonia_Lab02_Controls.Views;

/// <summary>
/// Упражнение 1: События Click и PointerMoved (аналог MouseMove).
/// </summary>
public partial class ClickMoveWindow : Window
{
    private int _clickCount;

    // Массив цветов для циклической смены фона кнопки
    private readonly IBrush[] _colors =
    {
        new SolidColorBrush(Color.FromRgb(255, 127, 80)),   // Coral
        new SolidColorBrush(Color.FromRgb(60, 179, 113)),   // MediumSeaGreen
        new SolidColorBrush(Color.FromRgb(30, 144, 255)),   // DodgerBlue
        new SolidColorBrush(Color.FromRgb(255, 215, 0)),    // Gold
        new SolidColorBrush(Color.FromRgb(186, 85, 211)),   // MediumOrchid
        new SolidColorBrush(Color.FromRgb(255, 99, 71)),    // Tomato
    };

    public ClickMoveWindow()
    {
        InitializeComponent();
        // Подписка на движение мыши по окну
        PointerMoved += OnPointerMoved;
    }

    // Обработчик нажатия кнопки — увеличиваем счётчик и меняем цвет
    private void OnClickButton(object? sender, RoutedEventArgs e)
    {
        _clickCount++;
        BtnClick.Background = _colors[_clickCount % _colors.Length];
        BtnClick.Content = $"Нажатий: {_clickCount}";
    }

    // Обработчик движения мыши — обновляем координаты
    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        var pos = e.GetPosition(this);
        LblCoords.Text = $"Координаты мыши: ({(int)pos.X}, {(int)pos.Y})";
    }
}
