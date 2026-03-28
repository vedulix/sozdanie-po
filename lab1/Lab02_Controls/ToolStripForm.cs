using System.Drawing;
using System.Windows.Forms;

namespace Lab02_Controls;

/// <summary>
/// Упражнение 3: ToolStrip.
/// </summary>
public class ToolStripForm : Form
{
    public ToolStripForm()
    {
        Text = "ToolStrip";
        Size = new Size(450, 300);
        StartPosition = FormStartPosition.CenterScreen;

        var toolStrip = new ToolStrip();

        var btnNew = new ToolStripButton("Создать");
        btnNew.Click += (_, _) =>
            MessageBox.Show("Создание нового документа", "Создать");

        var btnOpen = new ToolStripButton("Открыть");
        btnOpen.Click += (_, _) =>
            MessageBox.Show("Открытие документа", "Открыть");

        var btnSave = new ToolStripButton("Сохранить");
        btnSave.Click += (_, _) =>
            MessageBox.Show("Сохранение документа", "Сохранить");

        toolStrip.Items.Add(btnNew);
        toolStrip.Items.Add(btnOpen);
        toolStrip.Items.Add(btnSave);

        Controls.Add(toolStrip);

        var lbl = new Label
        {
            Text = "Нажмите на кнопки панели инструментов выше.",
            Font = new Font("Segoe UI", 11),
            AutoSize = true,
            Location = new Point(20, 60)
        };
        Controls.Add(lbl);
    }
}
