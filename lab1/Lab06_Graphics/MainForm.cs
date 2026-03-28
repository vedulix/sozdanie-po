namespace Lab06_Graphics;

public class MainForm : Form
{
    public MainForm()
    {
        Text = "ЛР 6 — Графика и анимация";
        Size = new Size(800, 600);
        StartPosition = FormStartPosition.CenterScreen;

        var tabControl = new TabControl { Dock = DockStyle.Fill };

        var tabGraph = new TabPage("Упр.1: График функции");
        tabGraph.Controls.Add(new GraphPanel { Dock = DockStyle.Fill });

        var tabAnimation = new TabPage("Упр.2: Анимация");
        tabAnimation.Controls.Add(new AnimationPanel { Dock = DockStyle.Fill });

        var tabChart = new TabPage("Упр.3: Диаграмма");
        tabChart.Controls.Add(new ChartPanel { Dock = DockStyle.Fill });

        tabControl.TabPages.AddRange(new[] { tabGraph, tabAnimation, tabChart });
        Controls.Add(tabControl);
    }
}
