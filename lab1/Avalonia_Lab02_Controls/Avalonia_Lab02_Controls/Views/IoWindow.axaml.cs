using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia_Lab02_Controls.Views;

/// <summary>
/// Упражнение 6: Простой ввод/вывод данных (имя и возраст).
/// </summary>
public partial class IoWindow : Window
{
    public IoWindow()
    {
        InitializeComponent();
    }

    // Показать результат — вывести введённые данные
    private void OnShow(object? sender, RoutedEventArgs e)
    {
        LblResult.Text = $"Вас зовут {TxtName.Text}, вам {TxtAge.Text} лет.";
    }
}
