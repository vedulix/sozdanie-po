using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace Avalonia_Lab02_Controls.Views;

/// <summary>
/// Упражнение 8: Валидация ввода (только цифры, ошибка при пустом поле).
/// Аналог KeyPress + ErrorProvider из WinForms.
/// </summary>
public partial class ValidationWindow : Window
{
    public ValidationWindow()
    {
        InitializeComponent();
    }

    // Фильтр ввода — блокируем нецифровые символы
    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        // Разрешаем Backspace, Delete, стрелки, Tab
        if (e.Key is Key.Back or Key.Delete or Key.Left or Key.Right or Key.Tab)
            return;

        // Разрешаем цифры (основная клавиатура и NumPad)
        bool isDigit = e.Key >= Key.D0 && e.Key <= Key.D9;
        bool isNumPad = e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9;

        if (!isDigit && !isNumPad)
        {
            e.Handled = true; // Блокируем ввод
        }
    }

    // Проверка — показываем ошибку или результат
    private async void OnCheck(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TxtNumber.Text))
        {
            LblError.Text = "Поле не может быть пустым!";
        }
        else
        {
            LblError.Text = "";
            var box = MessageBoxManager.GetMessageBoxStandard(
                "Успех", $"Введено число: {TxtNumber.Text}", ButtonEnum.Ok);
            await box.ShowAsPopupAsync(this);
        }
    }
}
