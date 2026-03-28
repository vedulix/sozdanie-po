using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia_Lab04_Dialogs;

/// <summary>
/// Упражнение 3: МОДАЛЬНЫЙ диалог редактирования объекта Person.
/// Открывается через ShowDialog — возвращает true при нажатии OK.
/// </summary>
public partial class EditPersonDialog : Window
{
    /// <summary>Результат редактирования — новый объект Person.</summary>
    public Person PersonResult { get; private set; } = new();

    public EditPersonDialog()
    {
        InitializeComponent();
    }

    public EditPersonDialog(Person? existing) : this()
    {
        // Заполняем поля текущими данными
        TxtName.Text = existing?.Name ?? "";
        NudAge.Value = existing != null && existing.Age >= 1 ? existing.Age : 18;

        BtnOk.Click += OnOkClick;
        BtnCancel.Click += OnCancelClick;
    }

    private void OnOkClick(object? sender, RoutedEventArgs e)
    {
        // Сохраняем результат и закрываем с true
        PersonResult = new Person
        {
            Name = TxtName.Text ?? "",
            Age = (int)(NudAge.Value ?? 18)
        };
        Close(true);
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }
}
