using System.Drawing;
using System.Windows.Forms;

namespace Lab02_Controls;

/// <summary>
/// Упражнение 8: Валидация (KeyPress + ErrorProvider).
/// </summary>
public class ValidationForm : Form
{
    public ValidationForm()
    {
        Text = "Валидация ввода";
        Size = new Size(420, 220);
        StartPosition = FormStartPosition.CenterScreen;

        var errorProvider = new ErrorProvider { BlinkStyle = ErrorBlinkStyle.AlwaysBlink };

        Controls.Add(new Label
        {
            Text = "Введите число:",
            Font = new Font("Segoe UI", 11),
            AutoSize = true,
            Location = new Point(20, 25)
        });

        var txtNumber = new TextBox
        {
            Location = new Point(170, 23),
            Width = 200,
            Font = new Font("Segoe UI", 11)
        };
        Controls.Add(txtNumber);

        // KeyPress: блокируем нецифровые символы (кроме Backspace)
        txtNumber.KeyPress += (_, e) =>
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        };

        // Validating: показываем ошибку если поле пустое
        txtNumber.Validating += (_, e) =>
        {
            if (string.IsNullOrWhiteSpace(txtNumber.Text))
                errorProvider.SetError(txtNumber, "Поле не может быть пустым!");
            else
                errorProvider.SetError(txtNumber, "");
        };

        var btnCheck = new Button
        {
            Text = "Проверить",
            Size = new Size(130, 35),
            Location = new Point(170, 70)
        };
        btnCheck.Click += (_, _) =>
        {
            if (string.IsNullOrWhiteSpace(txtNumber.Text))
            {
                errorProvider.SetError(txtNumber, "Поле не может быть пустым!");
            }
            else
            {
                errorProvider.SetError(txtNumber, "");
                MessageBox.Show($"Введено число: {txtNumber.Text}",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        };
        Controls.Add(btnCheck);

        var lblHint = new Label
        {
            Text = "Допускаются только цифры. При пустом поле — ошибка.",
            ForeColor = Color.Gray,
            AutoSize = true,
            Location = new Point(20, 130)
        };
        Controls.Add(lblHint);
    }
}
