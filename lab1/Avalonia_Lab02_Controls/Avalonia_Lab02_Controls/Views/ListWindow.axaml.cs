using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia_Lab02_Controls.Views;

/// <summary>
/// Упражнение 2: Работа со списками (ListBox).
/// </summary>
public partial class ListWindow : Window
{
    public ListWindow()
    {
        InitializeComponent();
    }

    // Добавить элемент в список
    private void OnAdd(object? sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TxtInput.Text))
        {
            ListItems.Items.Add(new ListBoxItem { Content = TxtInput.Text });
            TxtInput.Text = "";
        }
    }

    // Удалить выбранный элемент из списка
    private void OnRemove(object? sender, RoutedEventArgs e)
    {
        if (ListItems.SelectedItem is ListBoxItem item)
        {
            ListItems.Items.Remove(item);
        }
    }
}
