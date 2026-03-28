using System.Drawing;
using System.Windows.Forms;

namespace Lab02_Controls;

/// <summary>
/// Упражнение 2: Работа со списками (ListBox).
/// </summary>
public class ListForm : Form
{
    public ListForm()
    {
        Text = "Работа со списками";
        Size = new Size(400, 350);
        StartPosition = FormStartPosition.CenterScreen;

        var listBox = new ListBox
        {
            Location = new Point(20, 20),
            Size = new Size(200, 250)
        };
        listBox.Items.AddRange(new object[]
        {
            "Элемент 1", "Элемент 2", "Элемент 3", "Элемент 4"
        });
        Controls.Add(listBox);

        var txtInput = new TextBox
        {
            Location = new Point(240, 20),
            Width = 130
        };
        Controls.Add(txtInput);

        var btnAdd = new Button
        {
            Text = "Добавить",
            Location = new Point(240, 55),
            Size = new Size(130, 30)
        };
        btnAdd.Click += (_, _) =>
        {
            if (!string.IsNullOrWhiteSpace(txtInput.Text))
            {
                listBox.Items.Add(txtInput.Text);
                txtInput.Clear();
            }
        };
        Controls.Add(btnAdd);

        var btnRemove = new Button
        {
            Text = "Удалить",
            Location = new Point(240, 95),
            Size = new Size(130, 30)
        };
        btnRemove.Click += (_, _) =>
        {
            if (listBox.SelectedIndex >= 0)
                listBox.Items.RemoveAt(listBox.SelectedIndex);
        };
        Controls.Add(btnRemove);
    }
}
