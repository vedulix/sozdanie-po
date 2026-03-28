using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia_Lab04_Dialogs;

/// <summary>
/// Модальный диалог выбора размера шрифта.
/// Упрощённый аналог WinForms FontDialog — в Avalonia нет встроенного.
/// </summary>
public partial class FontSizeDialog : Window
{
    /// <summary>Выбранный размер шрифта.</summary>
    public double SelectedFontSize { get; private set; } = 14;

    public FontSizeDialog()
    {
        InitializeComponent();
        BtnOk.Click += OnOkClick;
        BtnCancel.Click += OnCancelClick;
    }

    public FontSizeDialog(double currentSize) : this()
    {
        NudFontSize.Value = (decimal)currentSize;
    }

    private void OnOkClick(object? sender, RoutedEventArgs e)
    {
        SelectedFontSize = (double)(NudFontSize.Value ?? 14);
        Close(true);
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }
}
