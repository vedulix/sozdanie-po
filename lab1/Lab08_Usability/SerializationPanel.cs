using System.Text.Json;

namespace Lab08_Usability;

public class SerializationPanel : Panel
{
    private readonly List<Contact> _contacts = new();
    private readonly DataGridView _grid;
    private readonly string _filePath;

    public SerializationPanel()
    {
        _filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "contacts.json");

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(10)
        };
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        _grid = new DataGridView
        {
            Dock = DockStyle.Fill,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            AllowUserToAddRows = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect
        };
        _grid.Columns.Add("Name", "Имя");
        _grid.Columns.Add("Phone", "Телефон");
        _grid.Columns.Add("Email", "Email");

        var btnPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            AutoSize = true,
            Padding = new Padding(0, 5, 0, 0)
        };

        var addBtn = new Button { Text = "Добавить", Width = 110 };
        var removeBtn = new Button { Text = "Удалить", Width = 110 };
        var saveBtn = new Button { Text = "Сохранить (JSON)", Width = 140 };
        var loadBtn = new Button { Text = "Загрузить (JSON)", Width = 140 };
        var pathLabel = new Label
        {
            Text = $"Файл: {_filePath}",
            AutoSize = true,
            Margin = new Padding(10, 7, 0, 0),
            ForeColor = Color.Gray
        };

        btnPanel.Controls.Add(addBtn);
        btnPanel.Controls.Add(removeBtn);
        btnPanel.Controls.Add(saveBtn);
        btnPanel.Controls.Add(loadBtn);
        btnPanel.Controls.Add(pathLabel);

        layout.Controls.Add(_grid, 0, 0);
        layout.Controls.Add(btnPanel, 0, 1);
        Controls.Add(layout);

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
            if (_grid.CurrentRow != null && _grid.CurrentRow.Index >= 0 && _grid.CurrentRow.Index < _contacts.Count)
            {
                _contacts.RemoveAt(_grid.CurrentRow.Index);
                RefreshGrid();
            }
        };

        saveBtn.Click += (_, _) =>
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(_contacts, options);
                File.WriteAllText(_filePath, json);
                MessageBox.Show($"Сохранено {_contacts.Count} контактов в:\n{_filePath}",
                    "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };

        loadBtn.Click += (_, _) =>
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    MessageBox.Show("Файл не найден. Сначала сохраните контакты.",
                        "Загрузка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string json = File.ReadAllText(_filePath);
                var loaded = JsonSerializer.Deserialize<List<Contact>>(json);
                if (loaded != null)
                {
                    _contacts.Clear();
                    _contacts.AddRange(loaded);
                    RefreshGrid();
                    MessageBox.Show($"Загружено {_contacts.Count} контактов.",
                        "Загрузка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };

        // Add sample data
        _contacts.Add(new Contact { Name = "Иванов И.И.", Phone = "+7 (901) 123-45-67", Email = "ivanov@mail.ru" });
        _contacts.Add(new Contact { Name = "Петрова А.С.", Phone = "+7 (902) 234-56-78", Email = "petrova@mail.ru" });
        RefreshGrid();
    }

    private void RefreshGrid()
    {
        _grid.Rows.Clear();
        foreach (var c in _contacts)
            _grid.Rows.Add(c.Name, c.Phone, c.Email);
    }
}
