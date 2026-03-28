using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Avalonia_Lab04_Dialogs;

/// <summary>
/// Модальный диалог выбора цвета.
/// Аналог WinForms ColorDialog — реализован через Avalonia ColorPicker.
/// </summary>
public partial class ColorPickerDialog : Window
{
    /// <summary>Выбранный пользователем цвет.</summary>
    public Color SelectedColor { get; private set; } = Colors.White;

    public ColorPickerDialog()
    {
        InitializeComponent();
        BtnOk.Click += OnOkClick;
        BtnCancel.Click += OnCancelClick;
    }

    private void OnOkClick(object? sender, RoutedEventArgs e)
    {
        SelectedColor = Picker.Color;
        Close(true);
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }
}
