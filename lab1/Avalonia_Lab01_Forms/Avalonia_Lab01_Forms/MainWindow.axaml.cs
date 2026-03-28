using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia_Lab01_Forms;

/// <summary>
/// Упражнение 1: Главная форма приложения.
/// Содержит кнопки для открытия остальных окон-упражнений.
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    // Открыть круглое окно (Упр. 2)
    private void OnRoundClick(object? sender, RoutedEventArgs e)
    {
        new RoundWindow().Show();
    }

    // Открыть наследуемую форму (Упр. 3)
    private void OnInheritedClick(object? sender, RoutedEventArgs e)
    {
        new ChildWindow().Show();
    }

    // Открыть MDI-приложение (Упр. 4)
    private void OnMdiClick(object? sender, RoutedEventArgs e)
    {
        new MdiParentWindow().Show();
    }

    // Открыть форму регистрации (Упр. 5)
    private void OnRegistrationClick(object? sender, RoutedEventArgs e)
    {
        new RegistrationWindow().Show();
    }
}
