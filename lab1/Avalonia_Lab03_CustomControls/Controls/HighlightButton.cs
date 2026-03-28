using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace Avalonia_Lab03_CustomControls.Controls;

/// <summary>
/// Упражнение 3: Расширенный элемент управления.
/// Кнопка, меняющая цвет фона при наведении курсора.
/// </summary>
public class HighlightButton : Button
{
    private IBrush? _normalBackground;

    /// <summary>
    /// Цвет подсветки при наведении курсора.
    /// </summary>
    public IBrush HighlightBrush { get; set; } = new SolidColorBrush(Colors.LightSkyBlue);

    /// <summary>
    /// При наведении — сохраняем текущий фон и подсвечиваем.
    /// </summary>
    protected override void OnPointerEntered(PointerEventArgs e)
    {
        _normalBackground = Background;
        Background = HighlightBrush;
        base.OnPointerEntered(e);
    }

    /// <summary>
    /// При уходе курсора — восстанавливаем обычный фон.
    /// </summary>
    protected override void OnPointerExited(PointerEventArgs e)
    {
        Background = _normalBackground;
        base.OnPointerExited(e);
    }
}
