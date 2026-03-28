using System;
using System.Globalization;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using Avalonia.Styling;

namespace Avalonia_Lab04_Dialogs;

/// <summary>
/// Главное окно ЛР 4: демонстрация диалоговых окон в Avalonia.
/// Порт WinForms-приложения Lab04_Dialogs.
/// </summary>
public partial class MainWindow : Window
{
    // Текущие настройки пользователя (Упражнение 2)
    private string _userName = "Пользователь";
    private string _theme = "Light";

    // Объект Person для Упражнения 3
    private Person _person = new() { Name = "Иван", Age = 20 };

    // Немодальный диалог поиска (Упражнение 4) — хранится, чтобы не создавать заново
    private FindDialog? _findDialog;

    public MainWindow()
    {
        InitializeComponent();

        // Привязка обработчиков кнопок
        BtnOpen.Click += OnOpenFileClick;
        BtnSave.Click += OnSaveFileClick;
        BtnColor.Click += OnColorClick;
        BtnFontSize.Click += OnFontSizeClick;
        BtnSettings.Click += OnSettingsClick;
        BtnEditPerson.Click += OnEditPersonClick;
        BtnFind.Click += OnFindClick;
        BtnCalc.Click += OnCalcClick;
    }

    // ── Вкладка 1: Стандартные диалоги (Упражнение 1) ──

    /// <summary>
    /// Открытие файла через системный диалог (аналог OpenFileDialog).
    /// В Avalonia используется StorageProvider.
    /// </summary>
    private async void OnOpenFileClick(object? sender, RoutedEventArgs e)
    {
        var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Открыть файл",
            AllowMultiple = false,
            FileTypeFilter = new[]
            {
                new FilePickerFileType("Текстовые файлы") { Patterns = new[] { "*.txt" } },
                new FilePickerFileType("Все файлы") { Patterns = new[] { "*.*" } }
            }
        });

        if (files.Count > 0)
        {
            var path = files[0].Path.LocalPath;
            TxtPath.Text = path;
            RichBox.Text = await File.ReadAllTextAsync(path);
        }
    }

    /// <summary>
    /// Сохранение файла через системный диалог (аналог SaveFileDialog).
    /// </summary>
    private async void OnSaveFileClick(object? sender, RoutedEventArgs e)
    {
        var file = await StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Сохранить файл",
            DefaultExtension = "txt",
            FileTypeChoices = new[]
            {
                new FilePickerFileType("Текстовые файлы") { Patterns = new[] { "*.txt" } },
                new FilePickerFileType("Все файлы") { Patterns = new[] { "*.*" } }
            }
        });

        if (file != null)
        {
            await File.WriteAllTextAsync(file.Path.LocalPath, RichBox.Text ?? "");
        }
    }

    /// <summary>
    /// Выбор цвета фона окна через МОДАЛЬНЫЙ диалог с ColorPicker.
    /// Аналог WinForms ColorDialog. Демонстрирует ShowDialog.
    /// </summary>
    private async void OnColorClick(object? sender, RoutedEventArgs e)
    {
        var dlg = new ColorPickerDialog();
        // ShowDialog — модальный вызов, блокирует родительское окно
        var result = await dlg.ShowDialog<bool>(this);
        if (result)
        {
            Background = new SolidColorBrush(dlg.SelectedColor);
        }
    }

    /// <summary>
    /// Изменение размера шрифта текстового поля (упрощённый аналог FontDialog).
    /// </summary>
    private async void OnFontSizeClick(object? sender, RoutedEventArgs e)
    {
        var dlg = new FontSizeDialog(RichBox.FontSize);
        // ShowDialog — модальный вызов
        var result = await dlg.ShowDialog<bool>(this);
        if (result)
        {
            RichBox.FontSize = dlg.SelectedFontSize;
        }
    }

    // ── Вкладка 2: Пользовательский модальный диалог (Упражнение 2) ──

    /// <summary>
    /// Открытие МОДАЛЬНОГО диалога настроек (ShowDialog).
    /// Блокирует главное окно до закрытия диалога.
    /// </summary>
    private async void OnSettingsClick(object? sender, RoutedEventArgs e)
    {
        var dlg = new SettingsDialog(_userName, _theme);
        var result = await dlg.ShowDialog<bool>(this);
        if (result)
        {
            _userName = dlg.UserName;
            _theme = dlg.SelectedTheme;
            LblCurrent.Text = $"Пользователь: {_userName}, тема: {_theme}";
            ApplyTheme();
        }
    }

    /// <summary>
    /// Применение темы оформления: Light / Dark.
    /// </summary>
    private void ApplyTheme()
    {
        if (_theme == "Dark")
        {
            Background = new SolidColorBrush(Color.FromRgb(45, 45, 48));
            if (Application.Current != null)
                Application.Current.RequestedThemeVariant = ThemeVariant.Dark;
        }
        else
        {
            Background = Brushes.Transparent;
            if (Application.Current != null)
                Application.Current.RequestedThemeVariant = ThemeVariant.Light;
        }
    }

    // ── Вкладка 3: Person (Упражнение 3) ──

    /// <summary>
    /// Открытие МОДАЛЬНОГО диалога редактирования Person (ShowDialog).
    /// </summary>
    private async void OnEditPersonClick(object? sender, RoutedEventArgs e)
    {
        var dlg = new EditPersonDialog(_person);
        var result = await dlg.ShowDialog<bool>(this);
        if (result)
        {
            _person = dlg.PersonResult;
            LblPerson.Text = _person.ToString();
        }
    }

    // ── Вкладка 4: Немодальный поиск (Упражнение 4) ──

    /// <summary>
    /// Открытие НЕМОДАЛЬНОГО диалога поиска (Show).
    /// Диалог не блокирует главное окно — можно продолжать редактировать текст.
    /// </summary>
    private void OnFindClick(object? sender, RoutedEventArgs e)
    {
        // Создаём диалог один раз и переиспользуем
        if (_findDialog == null || !_findDialog.IsVisible)
        {
            _findDialog = new FindDialog(SearchRichBox);
            // Show — НЕМОДАЛЬНЫЙ вызов (в отличие от ShowDialog)
            _findDialog.Show(this);
        }
        else
        {
            _findDialog.Activate();
        }
    }

    // ── Вкладка 5: Калькулятор функций (Упражнение 5) ──

    /// <summary>
    /// Вычисление математической функции f(ax+b).
    /// </summary>
    private void OnCalcClick(object? sender, RoutedEventArgs e)
    {
        if (!double.TryParse(TxtA.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double a) ||
            !double.TryParse(TxtB.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double b) ||
            !double.TryParse(TxtX.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double x))
        {
            LblResult.Text = "Ошибка: введите числовые значения";
            LblResult.Foreground = Brushes.Red;
            return;
        }

        double arg = a * x + b;
        var selectedItem = CmbFunc.SelectedItem as ComboBoxItem;
        string funcName = selectedItem?.Content?.ToString() ?? "sin(ax+b)";

        try
        {
            double result = funcName switch
            {
                "sin(ax+b)" => Math.Sin(arg),
                "cos(ax+b)" => Math.Cos(arg),
                "exp(ax+b)" => Math.Exp(arg),
                "ln(ax+b)" => Math.Log(arg),
                _ => double.NaN
            };

            LblResult.Foreground = Brushes.DarkBlue;
            LblResult.Text = $"{funcName} = {result:F6}  (при a={a}, b={b}, x={x})";
        }
        catch (Exception ex)
        {
            LblResult.Text = $"Ошибка вычисления: {ex.Message}";
            LblResult.Foreground = Brushes.Red;
        }
    }
}
