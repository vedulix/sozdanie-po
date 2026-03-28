using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;

namespace Avalonia_Lab01_Forms;

/// <summary>
/// Упражнение 3: Форма-наследник BaseWindow.
/// Демонстрирует наследование окон: меняет унаследованные элементы
/// и добавляет собственные.
/// В Avalonia наследование окон с AXAML ограничено, поэтому
/// ChildWindow создаётся программно, наследуя от BaseWindow.
/// </summary>
public class ChildWindow : BaseWindow
{
    public ChildWindow()
    {
        // Переопределяем свойства базовой формы
        Title = "Дочерняя форма (наследник)";
        Background = new SolidColorBrush(Colors.LightGreen);

        // Меняем текст унаследованной метки
        var lblBase = this.FindControl<TextBlock>("LblBase");
        if (lblBase != null)
            lblBase.Text = "Унаследованный элемент (из BaseWindow)";

        // Добавляем собственные элементы в панель базовой формы
        var basePanel = this.FindControl<StackPanel>("BasePanel");
        if (basePanel != null)
        {
            // Метка дочерней формы
            basePanel.Children.Add(new TextBlock
            {
                Text = "Это элемент дочерней формы",
                FontSize = 14,
                Foreground = new SolidColorBrush(Colors.DarkGreen)
            });

            // Кнопка дочерней формы
            var btnChild = new Button
            {
                Content = "Кнопка дочерней формы",
                Width = 200,
                Height = 35,
                HorizontalAlignment = HorizontalAlignment.Left,
                Background = new SolidColorBrush(Colors.PaleGreen),
                HorizontalContentAlignment = HorizontalAlignment.Center
            };
            btnChild.Click += OnChildButtonClick;
            basePanel.Children.Add(btnChild);
        }
    }

    // Обработчик кнопки дочерней формы
    private async void OnChildButtonClick(object? sender, RoutedEventArgs e)
    {
        var dialog = new Window
        {
            Title = "Дочерняя форма",
            Width = 300, Height = 150,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Content = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Spacing = 15,
                Children =
                {
                    new TextBlock
                    {
                        Text = "Нажата кнопка дочерней формы!",
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
