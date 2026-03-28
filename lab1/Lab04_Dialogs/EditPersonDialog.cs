namespace Lab04_Dialogs;

/// <summary>
/// Упражнение 3: Диалог редактирования объекта Person.
/// </summary>
public class EditPersonDialog : Form
{
    private readonly TextBox _txtName;
    private readonly NumericUpDown _nudAge;

    public Person PersonResult { get; private set; } = new();

    public EditPersonDialog(Person? existing = null)
    {
        Text = "Редактирование Person";
        Size = new Size(330, 190);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;

        var lblName = new Label { Text = "Имя:", Location = new Point(15, 20), AutoSize = true };
        _txtName = new TextBox
        {
            Location = new Point(100, 17),
            Width = 190,
            Text = existing?.Name ?? ""
        };

        var lblAge = new Label { Text = "Возраст:", Location = new Point(15, 55), AutoSize = true };
        _nudAge = new NumericUpDown
        {
            Location = new Point(100, 52),
            Width = 100,
            Minimum = 1,
            Maximum = 150,
            Value = existing != null && existing.Age >= 1 ? existing.Age : 18
        };

        var btnOk = new Button
        {
            Text = "OK",
            Location = new Point(110, 100),
            Size = new Size(80, 30),
            DialogResult = DialogResult.OK
        };
        btnOk.Click += (_, _) =>
        {
            PersonResult = new Person { Name = _txtName.Text, Age = (int)_nudAge.Value };
        };

        var btnCancel = new Button
        {
            Text = "Отмена",
            Location = new Point(200, 100),
            Size = new Size(80, 30),
            DialogResult = DialogResult.Cancel
        };

        AcceptButton = btnOk;
        CancelButton = btnCancel;

        Controls.AddRange(new Control[] { lblName, _txtName, lblAge, _nudAge, btnOk, btnCancel });
    }
}
