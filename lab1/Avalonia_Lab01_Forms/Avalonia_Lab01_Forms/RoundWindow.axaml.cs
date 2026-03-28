using Avalonia.Controls;
using Avalonia.Input;

namespace Avalonia_Lab01_Forms;

/// <summary>
/// Упражнение 2: Непрямоугольная (круглая) форма.
/// В Avalonia нет Region, поэтому используем TransparencyLevelHint + CornerRadius.
/// </summary>
public partial class RoundWindow : Window
{
    public RoundWindow()
    {
        InitializeComponent();

        // Делаем окно прозрачным для создания эффекта круглой формы
        TransparencyLevelHint = new[] { WindowTransparencyLevel.Transparent };
        Background = Avalonia.Media.Brushes.Transparent;
    }

    // Закрытие по нажатию Esc
    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
            Close();
    }
}
