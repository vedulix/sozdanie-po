using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace Avalonia_Lab02_Controls.Views;

/// <summary>
/// Упражнение 3: Панель инструментов (аналог ToolStrip из WinForms).
/// Кнопки показывают сообщение при нажатии.
/// </summary>
public partial class ToolStripWindow : Window
{
    public ToolStripWindow()
    {
        InitializeComponent();
    }

    // Показать диалоговое окно с сообщением (аналог MessageBox)
    private async Task ShowMsg(string title, string message)
    {
        var box = MessageBoxManager.GetMessageBoxStandard(title, message, ButtonEnum.Ok);
        await box.ShowAsPopupAsync(this);
    }

    private async void OnNew(object? sender, RoutedEventArgs e)
        => await ShowMsg("Создать", "Создание нового документа");

    private async void OnOpen(object? sender, RoutedEventArgs e)
        => await ShowMsg("Открыть", "Открытие документа");

    private async void OnSave(object? sender, RoutedEventArgs e)
        => await ShowMsg("Сохранить", "Сохранение документа");
}
