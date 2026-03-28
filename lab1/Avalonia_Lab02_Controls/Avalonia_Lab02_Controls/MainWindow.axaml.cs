using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia_Lab02_Controls.Views;

namespace Avalonia_Lab02_Controls;

/// <summary>
/// Главное окно приложения с кнопками для открытия упражнений.
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    // Обработчики нажатий кнопок — каждый открывает отдельное окно упражнения
    private void OpenClickMove(object? sender, RoutedEventArgs e) => new ClickMoveWindow().Show();
    private void OpenList(object? sender, RoutedEventArgs e) => new ListWindow().Show();
    private void OpenToolStrip(object? sender, RoutedEventArgs e) => new ToolStripWindow().Show();
    private void OpenStatusStrip(object? sender, RoutedEventArgs e) => new StatusStripWindow().Show();
    private void OpenIo(object? sender, RoutedEventArgs e) => new IoWindow().Show();
    private void OpenValidation(object? sender, RoutedEventArgs e) => new ValidationWindow().Show();
    private void OpenAddressBook(object? sender, RoutedEventArgs e) => new AddressBookWindow().Show();
}
