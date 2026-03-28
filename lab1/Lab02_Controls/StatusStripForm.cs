using System.Drawing;
using System.Windows.Forms;

namespace Lab02_Controls;

/// <summary>
/// Упражнение 4: StatusStrip + Timer.
/// </summary>
public class StatusStripForm : Form
{
    public StatusStripForm()
    {
        Text = "StatusStrip + Timer";
        Size = new Size(450, 250);
        StartPosition = FormStartPosition.CenterScreen;

        var statusStrip = new StatusStrip();
        var statusLabel = new ToolStripStatusLabel
        {
            Text = System.DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")
        };
        statusStrip.Items.Add(statusLabel);
        Controls.Add(statusStrip);

        var timer = new System.Windows.Forms.Timer { Interval = 1000 };
        timer.Tick += (_, _) =>
            statusLabel.Text = System.DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        timer.Start();

        // Останавливаем таймер при закрытии формы
        FormClosed += (_, _) => timer.Dispose();

        var lbl = new Label
        {
            Text = "Текущие дата и время обновляются\nкаждую секунду в StatusStrip.",
            Font = new Font("Segoe UI", 11),
            AutoSize = true,
            Location = new Point(20, 30)
        };
        Controls.Add(lbl);
    }
}
