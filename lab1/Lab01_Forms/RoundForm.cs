using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Lab01_Forms;

/// <summary>
/// Упражнение 2: Непрямоугольная (круглая) форма.
/// </summary>
public class RoundForm : Form
{
    public RoundForm()
    {
        Text = "Круглая форма";
        Size = new Size(300, 300);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.None;
        BackColor = Color.MediumSlateBlue;

        var lblInfo = new Label
        {
            Text = "Круглая форма!\nНажмите Esc для закрытия.",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 11),
            AutoSize = true
        };
        Controls.Add(lblInfo);
        lblInfo.Location = new Point(
            (ClientSize.Width - lblInfo.PreferredWidth) / 2,
            (ClientSize.Height - lblInfo.PreferredHeight) / 2);

        // Закрытие по Esc
        KeyPreview = true;
        KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Escape) Close();
        };
    }

    protected override void OnResize(System.EventArgs e)
    {
        base.OnResize(e);
        var path = new GraphicsPath();
        path.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
        Region = new Region(path);
    }
}
