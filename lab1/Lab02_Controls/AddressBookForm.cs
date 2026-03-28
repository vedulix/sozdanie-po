using System.Drawing;
using System.Windows.Forms;

namespace Lab02_Controls;

/// <summary>
/// Упражнение 9 (контрольное): Адресная книга с DataGridView.
/// </summary>
public class AddressBookForm : Form
{
    public AddressBookForm()
    {
        Text = "Адресная книга";
        Size = new Size(550, 420);
        StartPosition = FormStartPosition.CenterScreen;

        var dgv = new DataGridView
        {
            Location = new Point(20, 20),
            Size = new Size(500, 280),
            AllowUserToAddRows = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        };
        dgv.Columns.Add("colName", "Имя");
        dgv.Columns.Add("colPhone", "Телефон");
        dgv.Columns.Add("colEmail", "Email");

        // Начальные данные
        dgv.Rows.Add("Иванов Иван", "+7 (999) 111-22-33", "ivanov@mail.ru");
        dgv.Rows.Add("Петрова Анна", "+7 (999) 444-55-66", "petrova@mail.ru");
        Controls.Add(dgv);

        // Поля ввода
        int y = 315;
        var txtName = new TextBox
        {
            Location = new Point(20, y), Width = 140,
            PlaceholderText = "Имя"
        };
        var txtPhone = new TextBox
        {
            Location = new Point(170, y), Width = 140,
            PlaceholderText = "Телефон"
        };
        var txtEmail = new TextBox
        {
            Location = new Point(320, y), Width = 140,
            PlaceholderText = "Email"
        };
        Controls.Add(txtName);
        Controls.Add(txtPhone);
        Controls.Add(txtEmail);

        y += 35;
        var btnAdd = new Button
        {
            Text = "Добавить",
            Location = new Point(20, y),
            Size = new Size(100, 30)
        };
        btnAdd.Click += (_, _) =>
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                dgv.Rows.Add(txtName.Text, txtPhone.Text, txtEmail.Text);
                txtName.Clear();
                txtPhone.Clear();
                txtEmail.Clear();
            }
        };
        Controls.Add(btnAdd);

        var btnDelete = new Button
        {
            Text = "Удалить",
            Location = new Point(130, y),
            Size = new Size(100, 30)
        };
        btnDelete.Click += (_, _) =>
        {
            if (dgv.SelectedRows.Count > 0)
                dgv.Rows.Remove(dgv.SelectedRows[0]);
        };
        Controls.Add(btnDelete);
    }
}
