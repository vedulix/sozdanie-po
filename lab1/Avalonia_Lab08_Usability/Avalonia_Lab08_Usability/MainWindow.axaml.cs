using System;
using System.Windows.Input;
using Avalonia.Controls;

namespace Avalonia_Lab08_Usability;

/// <summary>
/// Главное окно приложения с вкладками (аналог WinForms MainForm).
/// Содержит TabControl с панелями упражнений и горячие клавиши.
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        // Команды для горячих клавиш (DataContext = this)
        SwitchTabCommand = new RelayCommand(o =>
        {
            if (o is string s && int.TryParse(s, out int idx))
                TabControl.SelectedIndex = idx;
        });
        QuitCommand = new RelayCommand(_ => Close());

        DataContext = this;
        InitializeComponent();

        // Заполняем вкладки контентом программно
        var tabs = TabControl.Items;
        ((TabItem)tabs[0]!).Content = new SerializationPanel();
        ((TabItem)tabs[1]!).Content = new HelpPanel();
        ((TabItem)tabs[2]!).Content = new TooltipPanel();
        ((TabItem)tabs[3]!).Content = new LanguageDetectPanel();
        ((TabItem)tabs[4]!).Content = new LocalizationPanel();
    }

    /// <summary>Команда переключения вкладки по индексу (Ctrl+1..5).</summary>
    public ICommand SwitchTabCommand { get; }

    /// <summary>Команда выхода из приложения (Ctrl+Q).</summary>
    public ICommand QuitCommand { get; }
}

/// <summary>
/// Простая реализация ICommand для привязки горячих клавиш.
/// </summary>
public class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;
    public RelayCommand(Action<object?> execute) => _execute = execute;
    #pragma warning disable CS0067
    public event EventHandler? CanExecuteChanged;
    #pragma warning restore CS0067
    public bool CanExecute(object? parameter) => true;
    public void Execute(object? parameter) => _execute(parameter);
}
