using System.Drawing;
using System.Windows.Forms;

namespace Lab01_Forms;

/// <summary>
/// Упражнение 5 (контрольное): Форма регистрации.
/// </summary>
public class RegistrationForm : Form
{
    public RegistrationForm()
    {
        Text = "Форма регистрации";
        Size = new Size(400, 350);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.WhiteSmoke;

        int x = 20, y = 20, lblW = 100, ctrlW = 230;

        // Имя
        Controls.Add(new Label
        {
            Text = "Имя:", Location = new Point(x, y + 3), AutoSize = true
        });
        var txtName = new TextBox
        {
            Location = new Point(x + lblW, y), Width = ctrlW
        };
        Controls.Add(txtName);
        y += 40;

        // Email
        Controls.Add(new Label
        {
            Text = "Email:", Location = new Point(x, y + 3), AutoSize = true
        });
        var txtEmail = new TextBox
        {
            Location = new Point(x + lblW, y), Width = ctrlW
        };
        Controls.Add(txtEmail);
        y += 40;

        // Роль
        Controls.Add(new Label
        {
            Text = "Роль:", Location = new Point(x, y + 3), AutoSize = true
        });
        var cmbRole = new ComboBox
        {
            Location = new Point(x + lblW, y),
            Width = ctrlW,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        cmbRole.Items.AddRange(new object[]
        {
            "Студент", "Преподаватель", "Администратор"
        });
        cmbRole.SelectedIndex = 0;
        Controls.Add(cmbRole);
        y += 40;

        // Согласие
        var chkAgree = new CheckBox
        {
            Text = "Я согласен с условиями",
            Location = new Point(x + lblW, y),
            AutoSize = true
        };
        Controls.Add(chkAgree);
        y += 40;

        // Кнопка
        var btnSubmit = new Button
        {
            Text = "Отправить",
            Size = new Size(120, 35),
            Location = new Point(x + lblW, y),
            BackColor = Color.SteelBlue,
            ForeColor = Color.White
        };
        btnSubmit.Click += (_, _) =>
        {
            if (!chkAgree.Checked)
            {
                MessageBox.Show("Необходимо принять условия!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var msg = $"Имя: {txtName.Text}\n" +
                      $"Email: {txtEmail.Text}\n" +
                      $"Роль: {cmbRole.SelectedItem}\n" +
                      $"Согласие: Да";
            MessageBox.Show(msg, "Данные регистрации",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        };
        Controls.Add(btnSubmit);
    }
}
