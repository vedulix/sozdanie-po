using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;

namespace Avalonia_Lab08_Usability;

/// <summary>
/// Упр.2: Контекстная справка.
/// При нажатии F1 показывается подсказка для элемента в фокусе.
/// В Avalonia нет HelpProvider, поэтому реализуем через Tag + KeyBinding.
/// </summary>
public class HelpPanel : UserControl
{
    private readonly TextBlock _helpDisplay;

    public HelpPanel()
    {
        var description = new TextBlock
        {
            Text = "Контекстная справка.\nНажмите F1 при фокусе на любом элементе, чтобы увидеть подсказку.",
            Margin = new Thickness(0, 0, 0, 20),
            TextWrapping = TextWrapping.Wrap
        };

        // Поля формы с подсказками через Tag
        var nameLabel = new TextBlock { Text = "Имя:" };
        var nameBox = new TextBox { Width = 300, Tag = "Введите ваше полное имя (Фамилия Имя Отчество)." };
        ToolTip.SetTip(nameBox, "Имя — Фамилия Имя Отчество");

        var emailLabel = new TextBlock { Text = "Email:", Margin = new Thickness(0, 10, 0, 0) };
        var emailBox = new TextBox { Width = 300, Tag = "Введите адрес электронной почты в формате user@domain.com." };
        ToolTip.SetTip(emailBox, "Email в формате user@domain.com");

        var phoneLabel = new TextBlock { Text = "Телефон:", Margin = new Thickness(0, 10, 0, 0) };
        var phoneBox = new TextBox { Width = 300, Tag = "Введите номер телефона в формате +7 (XXX) XXX-XX-XX." };
        ToolTip.SetTip(phoneBox, "Телефон: +7 (XXX) XXX-XX-XX");

        var saveBtn = new Button { Content = "Сохранить", Width = 120, Margin = new Thickness(0, 15, 0, 0), Tag = "Нажмите, чтобы сохранить введённые данные." };
        var clearBtn = new Button { Content = "Очистить", Width = 120, Tag = "Нажмите, чтобы очистить все поля формы." };

        // Область вывода справки при F1
        _helpDisplay = new TextBlock
        {
            Text = "",
            Foreground = Brushes.Blue,
            FontStyle = FontStyle.Italic,
            Margin = new Thickness(0, 15, 0, 0),
            TextWrapping = TextWrapping.Wrap
        };

        saveBtn.Click += async (_, _) =>
        {
            var owner = TopLevel.GetTopLevel(this) as Window;
            if (owner == null) return;
            var dlg = new Window
            {
                Title = "Информация", Width = 300, Height = 120,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                CanResize = false,
                Content = new StackPanel
                {
                    Margin = new Thickness(20), Spacing = 10,
                    Children =
                    {
                        new TextBlock { Text = "Данные сохранены!" },
                        new Button { Content = "OK", HorizontalAlignment = HorizontalAlignment.Center, Width = 80 }
                    }
                }
            };
            ((Button)((StackPanel)dlg.Content).Children[1]).Click += (_, _) => dlg.Close();
            await dlg.ShowDialog(owner);
        };

        clearBtn.Click += (_, _) =>
        {
            nameBox.Text = "";
            emailBox.Text = "";
            phoneBox.Text = "";
        };

        var layout = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 4
        };
        layout.Children.Add(description);
        layout.Children.Add(nameLabel);
        layout.Children.Add(nameBox);
        layout.Children.Add(emailLabel);
        layout.Children.Add(emailBox);
        layout.Children.Add(phoneLabel);
        layout.Children.Add(phoneBox);
        layout.Children.Add(saveBtn);
        layout.Children.Add(clearBtn);
        layout.Children.Add(_helpDisplay);

        Content = layout;

        // Обработка F1 — показываем справку из Tag текущего элемента в фокусе
        this.KeyDown += (_, e) =>
        {
            if (e.Key == Key.F1)
            {
                var focused = TopLevel.GetTopLevel(this)?.FocusManager?.GetFocusedElement() as Control;
                if (focused?.Tag is string helpText)
                    _helpDisplay.Text = $"Справка: {helpText}";
                else
                    _helpDisplay.Text = "Справка: для данного элемента подсказка не задана.";
                e.Handled = true;
            }
        };
    }
}
