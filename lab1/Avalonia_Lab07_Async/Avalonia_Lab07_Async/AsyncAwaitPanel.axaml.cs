using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia_Lab07_Async;

/// <summary>
/// Упр.4: Демонстрация async/await.
/// LoadDataAsync имитирует загрузку данных с задержкой 2 секунды.
/// UI остаётся отзывчивым благодаря await.
/// </summary>
public partial class AsyncAwaitPanel : UserControl
{
    public AsyncAwaitPanel()
    {
        InitializeComponent();
        LoadBtn.Click += OnLoad;
    }

    private async void OnLoad(object? sender, RoutedEventArgs e)
    {
        LoadBtn.IsEnabled = false;
        DataBox.Text = "Загрузка...";
        StatusLabel.Text = "Ожидание ответа сервера...";

        // await освобождает UI-поток на время задержки
        string data = await LoadDataAsync();

        DataBox.Text = data;
        StatusLabel.Text = "Данные загружены!";
        LoadBtn.IsEnabled = true;
    }

    /// <summary>
    /// Имитация загрузки данных с сервера (задержка 2 сек).
    /// </summary>
    private static async Task<string> LoadDataAsync()
    {
        // Имитация задержки сети
        await Task.Delay(2000);

        return
            "=== Данные с сервера ===\n\n" +
            "Пользователь: Иванов И.И.\n" +
            "Email: ivanov@example.com\n" +
            "Роль: Администратор\n\n" +
            "Последние заказы:\n" +
            "  1. Ноутбук — 75 000 ₽\n" +
            "  2. Монитор — 32 000 ₽\n" +
            "  3. Клавиатура — 5 500 ₽\n\n" +
            $"Время загрузки: {DateTime.Now:HH:mm:ss}";
    }
}
