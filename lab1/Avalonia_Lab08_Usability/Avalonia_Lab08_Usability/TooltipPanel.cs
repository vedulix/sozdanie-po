using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace Avalonia_Lab08_Usability;

/// <summary>
/// Упр.4: Подсказки (ToolTip).
/// Форма входа с всплывающими подсказками на каждом элементе.
/// </summary>
public class TooltipPanel : UserControl
{
    public TooltipPanel()
    {
        var description = new TextBlock
        {
            Text = "Наведите курсор на любой элемент, чтобы увидеть всплывающую подсказку.",
            Margin = new Thickness(0, 0, 0, 20),
            TextWrapping = TextWrapping.Wrap
        };

        // Поля с подсказками
        var nameLabel = new TextBlock { Text = "Имя пользователя:" };
        var nameBox = new TextBox { Width = 300 };
        ToolTip.SetTip(nameBox, "Введите имя пользователя (от 3 до 50 символов)");

        var passLabel = new TextBlock { Text = "Пароль:", Margin = new Thickness(0, 10, 0, 0) };
        var passBox = new TextBox { Width = 300, PasswordChar = '\u2022' }; // символ-маска
        ToolTip.SetTip(passBox, "Пароль должен содержать не менее 8 символов, включая цифры и буквы");

        var rememberCheck = new CheckBox
        {
            Content = "Запомнить меня",
            Margin = new Thickness(0, 10, 0, 0)
        };
        ToolTip.SetTip(rememberCheck, "Установите, чтобы оставаться в системе после закрытия приложения");

        var loginBtn = new Button { Content = "Войти", Width = 120, Margin = new Thickness(0, 15, 0, 0) };
        ToolTip.SetTip(loginBtn, "Нажмите для входа в систему с указанными учётными данными");

        var registerBtn = new Button { Content = "Регистрация", Width = 120 };
        ToolTip.SetTip(registerBtn, "Нажмите для создания нового аккаунта");

        // Ссылка «Забыли пароль?» — стилизованная кнопка
        var forgotLink = new Button
        {
            Content = "Забыли пароль?",
            Margin = new Thickness(0, 10, 0, 0),
            // Делаем вид ссылки
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            Foreground = Brushes.Blue,
            Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Hand)
        };
        ToolTip.SetTip(forgotLink, "Нажмите для восстановления пароля по email");

        // Валидация при нажатии «Войти»
        loginBtn.Click += async (_, _) =>
        {
            string msg;
            if (string.IsNullOrWhiteSpace(nameBox.Text) || string.IsNullOrWhiteSpace(passBox.Text))
                msg = "Заполните все поля!";
            else
                msg = "Вход выполнен!";

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
                        new TextBlock { Text = msg, TextWrapping = TextWrapping.Wrap },
                        new Button { Content = "OK", HorizontalAlignment = HorizontalAlignment.Center, Width = 80 }
                    }
                }
            };
            ((Button)((StackPanel)dlg.Content).Children[1]).Click += (_, _) => dlg.Close();
            await dlg.ShowDialog(owner);
        };

        var layout = new StackPanel { Margin = new Thickness(20), Spacing = 4 };
        layout.Children.Add(description);
        layout.Children.Add(nameLabel);
        layout.Children.Add(nameBox);
        layout.Children.Add(passLabel);
        layout.Children.Add(passBox);
        layout.Children.Add(rememberCheck);
        layout.Children.Add(loginBtn);
        layout.Children.Add(registerBtn);
        layout.Children.Add(forgotLink);

        Content = layout;
    }
}
