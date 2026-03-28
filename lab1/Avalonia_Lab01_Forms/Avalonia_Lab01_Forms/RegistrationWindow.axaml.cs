using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;

namespace Avalonia_Lab01_Forms;

/// <summary>
/// Упражнение 5 (контрольное): Форма регистрации.
/// Собирает данные пользователя и показывает результат в диалоговом окне.
/// </summary>
public partial class RegistrationWindow : Window
{
    public RegistrationWindow()
    {
        InitializeComponent();
    }

    // Обработчик кнопки "Отправить"
    private async void OnSubmitClick(object? sender, RoutedEventArgs e)
    {
        // Проверка: пользователь должен принять условия
        if (!ChkAgree.IsChecked.GetValueOrDefault())
        {
            await ShowMessage("Ошибка", "Необходимо принять условия!");
            return;
        }

        // Получаем выбранную роль из ComboBox
        var role = (CmbRole.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "";

        var msg = $"Имя: {TxtName.Text}\n" +
                  $"Email: {TxtEmail.Text}\n" +
                  $"Роль: {role}\n" +
                  $"Согласие: Да";

        await ShowMessage("Данные регистрации", msg);
    }

    /// <summary>
    /// Вспомогательный метод для показа диалогового окна (аналог MessageBox).
    /// </summary>
    private async System.Threading.Tasks.Task ShowMessage(string title, string text)
    {
        var dialog = new Window
        {
            Title = title,
            Width = 350, Height = 180,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            CanResize = false,
            Content = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Spacing = 15,
                Margin = new Avalonia.Thickness(20),
                Children =
                {
                    new TextBlock
                    {
                        Text = text,
                        TextWrapping = Avalonia.Media.TextWrapping.Wrap,
                        HorizontalAlignment = HorizontalAlignment.Center
                    },
                    new Button
                    {
                        Content = "OK",
                        Width = 80,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center
                    }
                }
            }
        };

        if (dialog.Content is StackPanel sp && sp.Children[1] is Button okBtn)
            okBtn.Click += (_, _) => dialog.Close();

        await dialog.ShowDialog(this);
    }
}
