namespace Lab07_Async;

public class MainForm : Form
{
    public MainForm()
    {
        Text = "ЛР 7 — Асинхронное программирование";
        Size = new Size(850, 600);
        StartPosition = FormStartPosition.CenterScreen;

        var tabControl = new TabControl { Dock = DockStyle.Fill };

        var tab1 = new TabPage("Упр.1: BackgroundWorker");
        tab1.Controls.Add(new BackgroundWorkerPanel { Dock = DockStyle.Fill });

        var tab2 = new TabPage("Упр.2: Делегаты");
        tab2.Controls.Add(new DelegatePanel { Dock = DockStyle.Fill });

        var tab3 = new TabPage("Упр.3: Асинхронный вызов");
        tab3.Controls.Add(new AsyncInvocationPanel { Dock = DockStyle.Fill });

        var tab4 = new TabPage("Упр.4: async/await");
        tab4.Controls.Add(new AsyncAwaitPanel { Dock = DockStyle.Fill });

        var tab5 = new TabPage("Упр.5: Фибоначчи");
        tab5.Controls.Add(new FibonacciPanel { Dock = DockStyle.Fill });

        tabControl.TabPages.AddRange(new[] { tab1, tab2, tab3, tab4, tab5 });
        Controls.Add(tabControl);
    }
}
