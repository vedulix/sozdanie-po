using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia_Lab04_Dialogs;

/// <summary>
/// Упражнение 2: Пользовательский МОДАЛЬНЫЙ диалог настроек.
/// Открывается через ShowDialog — блокирует родительское окно.
/// </summary>
public partial class SettingsDialog : Window
{
    /// <summary>Имя пользователя, введённое в диалоге.</summary>
    public string UserName => TxtUsername.Text ?? "";

    /// <summary>Выбранная тема оформления.</summary>
    public string SelectedTheme
    {
        get
        {
            var item = CmbTheme.SelectedItem as ComboBoxItem;
            return item?.Content?.ToString() ?? "Light";
        }
    }

    public SettingsDialog()
    {
        InitializeComponent();
    }

    public SettingsDialog(string currentName, string currentTheme) : this()
    {
        // Заполняем текущие значения
        TxtUsername.Text = currentName;

        // Выбираем соответствующий элемент ComboBox
        for (int i = 0; i < CmbTheme.ItemCount; i++)
        {
            if (CmbTheme.Items[i] is ComboBoxItem item &&
                item.Content?.ToString() == currentTheme)
            {
                CmbTheme.SelectedIndex = i;
                break;
            }
        }

        BtnOk.Click += OnOkClick;
        BtnCancel.Click += OnCancelClick;
    }

    /// <summary>Закрытие с результатом OK (true).</summary>
    private void OnOkClick(object? sender, RoutedEventArgs e)
    {
        Close(true);
    }

    /// <summary>Закрытие с результатом Cancel (false).</summary>
    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }
}
