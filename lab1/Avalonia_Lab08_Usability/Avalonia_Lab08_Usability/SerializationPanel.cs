using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace Avalonia_Lab08_Usability;

/// <summary>
/// Упр.1: Сериализация контактов в JSON.
/// Таблица контактов с кнопками добавления, удаления, сохранения и загрузки.
/// </summary>
public class SerializationPanel : UserControl
{
    private readonly List<Contact> _contacts = new();
    private readonly DataGrid _grid;
    private readonly string _filePath;

    public SerializationPanel()
    {
        // Путь к файлу на рабочем столе
        _filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "contacts.json");

        // Таблица контактов (DataGrid)
        _grid = new DataGrid
        {
            AutoGenerateColumns = false,
            CanUserReorderColumns = false,
            IsReadOnly = true,
            SelectionMode = DataGridSelectionMode.Single,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch
        };
        _grid.Columns.Add(new DataGridTextColumn { Header = "Имя", Binding = new Avalonia.Data.Binding("Name"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
        _grid.Columns.Add(new DataGridTextColumn { Header = "Телефон", Binding = new Avalonia.Data.Binding("Phone"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
        _grid.Columns.Add(new DataGridTextColumn { Header = "Email", Binding = new Avalonia.Data.Binding("Email"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });

        // Кнопки управления
        var addBtn = new Button { Content = "Добавить", Width = 110 };
        ToolTip.SetTip(addBtn, "Добавить новый контакт");

        var removeBtn = new Button { Content = "Удалить", Width = 110 };
        ToolTip.SetTip(removeBtn, "Удалить выбранный контакт");

        var saveBtn = new Button { Content = "Сохранить (JSON)", Width = 150 };
        ToolTip.SetTip(saveBtn, "Сохранить контакты в JSON-файл");

        var loadBtn = new Button { Content = "Загрузить (JSON)", Width = 150 };
        ToolTip.SetTip(loadBtn, "Загрузить контакты из JSON-файла");

        var pathLabel = new TextBlock
        {
            Text = $"Файл: {_filePath}",
            Foreground = Brushes.Gray,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(10, 0, 0, 0)
        };

        var btnPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 5,
            Margin = new Thickness(0, 5, 0, 0)
        };
        btnPanel.Children.Add(addBtn);
        btnPanel.Children.Add(removeBtn);
        btnPanel.Children.Add(saveBtn);
        btnPanel.Children.Add(loadBtn);
        btnPanel.Children.Add(pathLabel);

        var layout = new DockPanel { Margin = new Thickness(10) };
        DockPanel.SetDock(btnPanel, Dock.Bottom);
        layout.Children.Add(btnPanel);
        layout.Children.Add(_grid);

        Content = layout;

        // Обработчики кнопок
        addBtn.Click += (_, _) =>
        {
            _contacts.Add(new Contact
            {
                Name = $"Контакт {_contacts.Count + 1}",
                Phone = "+7 (999) 000-00-00",
                Email = $"user{_contacts.Count + 1}@example.com"
            });
            RefreshGrid();
        };

        removeBtn.Click += (_, _) =>
        {
            if (_grid.SelectedIndex >= 0 && _grid.SelectedIndex < _contacts.Count)
            {
                _contacts.RemoveAt(_grid.SelectedIndex);
                RefreshGrid();
            }
        };

        saveBtn.Click += async (_, _) =>
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(_contacts, options);
                await File.WriteAllTextAsync(_filePath, json);
                await ShowMessage("Сохранение", $"Сохранено {_contacts.Count} контактов в:\n{_filePath}");
            }
            catch (Exception ex)
            {
                await ShowMessage("Ошибка", $"Ошибка сохранения: {ex.Message}");
            }
        };

        loadBtn.Click += async (_, _) =>
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    await ShowMessage("Загрузка", "Файл не найден. Сначала сохраните контакты.");
                    return;
                }
                string json = await File.ReadAllTextAsync(_filePath);
                var loaded = JsonSerializer.Deserialize<List<Contact>>(json);
                if (loaded != null)
                {
                    _contacts.Clear();
                    _contacts.AddRange(loaded);
                    RefreshGrid();
                    await ShowMessage("Загрузка", $"Загружено {_contacts.Count} контактов.");
                }
            }
            catch (Exception ex)
            {
                await ShowMessage("Ошибка", $"Ошибка загрузки: {ex.Message}");
            }
        };

        // Начальные данные
        _contacts.Add(new Contact { Name = "Иванов И.И.", Phone = "+7 (901) 123-45-67", Email = "ivanov@mail.ru" });
        _contacts.Add(new Contact { Name = "Петрова А.С.", Phone = "+7 (902) 234-56-78", Email = "petrova@mail.ru" });
        RefreshGrid();
    }

    /// <summary>Обновление таблицы из списка контактов.</summary>
    private void RefreshGrid()
    {
        _grid.ItemsSource = null;
        _grid.ItemsSource = new ObservableCollection<Contact>(_contacts);
    }

    /// <summary>Показать диалоговое окно с сообщением (аналог MessageBox).</summary>
    private async System.Threading.Tasks.Task ShowMessage(string title, string message)
    {
        var dialog = new Window
        {
            Title = title,
            Width = 400,
            Height = 160,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            CanResize = false,
            Content = new StackPanel
            {
                Margin = new Thickness(20),
                Spacing = 15,
                Children =
                {
                    new TextBlock { Text = message, TextWrapping = TextWrapping.Wrap },
                    new Button { Content = "OK", HorizontalAlignment = HorizontalAlignment.Center, Width = 80 }
                }
            }
        };
        ((Button)((StackPanel)dialog.Content).Children[1]).Click += (_, _) => dialog.Close();
        var owner = TopLevel.GetTopLevel(this) as Window;
        if (owner != null)
            await dialog.ShowDialog(owner);
    }
}
