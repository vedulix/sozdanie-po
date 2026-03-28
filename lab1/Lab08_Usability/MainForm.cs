namespace Lab08_Usability;

public class MainForm : Form
{
    public MainForm()
    {
        Text = "ЛР 8 — Повышение удобства использования";
        Size = new Size(900, 650);
        StartPosition = FormStartPosition.CenterScreen;

        var tabControl = new TabControl { Dock = DockStyle.Fill };

        var tab1 = new TabPage("Упр.1: Сериализация");
        tab1.Controls.Add(new SerializationPanel { Dock = DockStyle.Fill });

        var tab2 = new TabPage("Упр.2: Контекстная справка");
        tab2.Controls.Add(new HelpPanel { Dock = DockStyle.Fill });

        var tab4 = new TabPage("Упр.4: Подсказки (ToolTip)");
        tab4.Controls.Add(new TooltipPanel { Dock = DockStyle.Fill });

        var tab5 = new TabPage("Упр.5: Определение языка");
        tab5.Controls.Add(new LanguageDetectPanel { Dock = DockStyle.Fill });

        var tab6 = new TabPage("Упр.6: Локализация");
        tab6.Controls.Add(new LocalizationPanel { Dock = DockStyle.Fill });

        tabControl.TabPages.AddRange(new[] { tab1, tab2, tab4, tab5, tab6 });
        Controls.Add(tabControl);
    }
}
