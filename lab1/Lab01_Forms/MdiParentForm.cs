using System.Drawing;
using System.Windows.Forms;

namespace Lab01_Forms;

/// <summary>
/// Упражнение 4: MDI-контейнер с меню.
/// </summary>
public class MdiParentForm : Form
{
    private int _childCount;

    public MdiParentForm()
    {
        Text = "MDI-приложение";
        Size = new Size(600, 450);
        StartPosition = FormStartPosition.CenterScreen;
        IsMdiContainer = true;

        // Меню
        var menuStrip = new MenuStrip();

        var fileMenu = new ToolStripMenuItem("Файл");

        var newChildItem = new ToolStripMenuItem("Новое дочернее окно");
        newChildItem.Click += (_, _) =>
        {
            _childCount++;
            var child = new MdiChildForm(_childCount) { MdiParent = this };
            child.Show();
        };

        var exitItem = new ToolStripMenuItem("Выход");
        exitItem.Click += (_, _) => Close();

        fileMenu.DropDownItems.Add(newChildItem);
        fileMenu.DropDownItems.Add(new ToolStripSeparator());
        fileMenu.DropDownItems.Add(exitItem);

        menuStrip.Items.Add(fileMenu);
        MainMenuStrip = menuStrip;
        Controls.Add(menuStrip);
    }
}
