using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia_Lab01_Forms;

/// <summary>
/// Упражнение 3: Базовая форма для наследования.
/// Содержит метку и кнопку, которые будут доступны в дочерней форме.
/// </summary>
public partial class BaseWindow : Window
{
    public BaseWindow()
    {
        InitializeComponent();
    }

    // Обработчик кнопки базовой формы
    private async void OnBaseButtonClick(object? sender, RoutedEventArgs e)
    {
        // Показываем диалог (аналог MessageBox в WinForms)
        var dialog = new Window
        {
            Title = "Базовая форма",
            Width = 300, Height = 150,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Content = new StackPanel
            {
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                Spacing = 15,
                Children =
                {
                    new TextBlock
                    {
                        Text = "Нажата кнопка базовой формы!",
                        HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
                    },
                    new Button
                    {
                        Content = "OK",
                        Width = 80,
                        HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                        HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center
                    }
                }
            }
        };

        // Закрытие диалога по нажатию OK
        if (dialog.Content is StackPanel sp && sp.Children[1] is Button okBtn)
            okBtn.Click += (_, _) => dialog.Close();

        await dialog.ShowDialog(this);
    }
}
