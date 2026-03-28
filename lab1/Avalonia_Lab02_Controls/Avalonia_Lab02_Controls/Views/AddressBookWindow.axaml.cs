using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia_Lab02_Controls.Views;

/// <summary>
/// Модель контакта для таблицы адресной книги.
/// </summary>
public class Contact
{
    public string Name { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";
}

/// <summary>
/// Упражнение 9 (контрольное): Адресная книга с DataGrid (аналог DataGridView).
/// </summary>
public partial class AddressBookWindow : Window
{
    // Коллекция контактов — привязана к DataGrid
    private readonly ObservableCollection<Contact> _contacts = new()
    {
        new Contact { Name = "Иванов Иван", Phone = "+7 (999) 111-22-33", Email = "ivanov@mail.ru" },
        new Contact { Name = "Петрова Анна", Phone = "+7 (999) 444-55-66", Email = "petrova@mail.ru" },
    };

    public AddressBookWindow()
    {
        InitializeComponent();
        DgContacts.ItemsSource = _contacts;
    }

    // Добавить новый контакт
    private void OnAdd(object? sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TxtName.Text))
        {
            _contacts.Add(new Contact
            {
                Name = TxtName.Text ?? "",
                Phone = TxtPhone.Text ?? "",
                Email = TxtEmail.Text ?? ""
            });
            TxtName.Text = "";
            TxtPhone.Text = "";
            TxtEmail.Text = "";
        }
    }

    // Удалить выбранный контакт
    private void OnDelete(object? sender, RoutedEventArgs e)
    {
        if (DgContacts.SelectedItem is Contact contact)
        {
            _contacts.Remove(contact);
        }
    }
}
