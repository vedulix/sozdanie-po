using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Avalonia_Lab01_Forms;

/// <summary>
/// Упражнение 4: MDI-контейнер с меню.
/// В Avalonia MDI отсутствует, поэтому дочерние окна представлены вкладками TabControl.
/// </summary>
public partial class MdiParentWindow : Window
{
    private int _childCount;

    public MdiParentWindow()
    {
        InitializeComponent();
    }

    // Создать новую вкладку-дочернее окно
    private void OnNewChild(object? sender, RoutedEventArgs e)
    {
        _childCount++;

        // Цвет фона зависит от номера (как в оригинале)
        var r = (byte)(200 + _childCount * 10 % 55);
        var g = (byte)(220 + _childCount * 15 % 35);
        var b = (byte)255;

        var tab = new TabItem
        {
            Header = $"Окно #{_childCount}",
            Content = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(r, g, b)),
                Child = new TextBlock
                {
                    Text = $"Это дочернее MDI-окно #{_childCount}",
                    FontSize = 14,
                    Margin = new Avalonia.Thickness(20)
                }
            }
        };

        TabChildren.Items.Add(tab);
        TabChildren.SelectedItem = tab;
    }

    // Закрыть окно
    private void OnExit(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}
