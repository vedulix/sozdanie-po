using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using CustomControls = Avalonia_Lab03_CustomControls.Controls;

namespace Avalonia_Lab03_CustomControls;

/// <summary>
/// Главное окно ЛР 3: демонстрация пользовательских элементов управления.
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Подписка на событие изменения цвета в ColorPicker (Вкладка 1)
        var picker = this.FindControl<CustomControls.ColorPicker>("MyColorPicker")!;
        var lblInfo = this.FindControl<TextBlock>("LblColorInfo")!;
        picker.ColorChanged += (_, _) =>
            lblInfo.Text = $"Выбранный цвет: #{picker.SelectedColor.R:X2}{picker.SelectedColor.G:X2}{picker.SelectedColor.B:X2}";

        // Подписка на кнопку валидации (Вкладка 3)
        var btnValidate = this.FindControl<CustomControls.HighlightButton>("BtnValidate")!;
        btnValidate.Click += OnValidateClick;
    }

    /// <summary>
    /// Обработчик нажатия кнопки «Проверить» — валидация данных формы.
    /// </summary>
    private void OnValidateClick(object? sender, RoutedEventArgs e)
    {
        var txtName = this.FindControl<TextBox>("TxtName")!;
        var txtEmail = this.FindControl<TextBox>("TxtEmail")!;
        var txtPhone = this.FindControl<TextBox>("TxtPhone")!;
        var txtAge = this.FindControl<CustomControls.NumericTextBox>("TxtAge")!;

        var errName = this.FindControl<TextBlock>("ErrName")!;
        var errEmail = this.FindControl<TextBlock>("ErrEmail")!;
        var errPhone = this.FindControl<TextBlock>("ErrPhone")!;
        var errAge = this.FindControl<TextBlock>("ErrAge")!;
        var lblResult = this.FindControl<TextBlock>("LblResult")!;

        bool valid = true;

        // Очищаем предыдущие ошибки
        errName.Text = errEmail.Text = errPhone.Text = errAge.Text = "";

        // Проверка имени — не должно быть пустым
        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            errName.Text = "Введите имя";
            valid = false;
        }

        // Проверка email — должен содержать @ и точку
        if (txtEmail.Text == null || !txtEmail.Text.Contains('@') || !txtEmail.Text.Contains('.'))
        {
            errEmail.Text = "Некорректный email";
            valid = false;
        }

        // Проверка телефона — минимум 6 символов
        if (txtPhone.Text == null || txtPhone.Text.Length < 6)
        {
            errPhone.Text = "Телефон слишком короткий";
            valid = false;
        }

        // Проверка возраста — от 1 до 150
        if (txtAge.Value < 1 || txtAge.Value > 150)
        {
            errAge.Text = "Возраст от 1 до 150";
            valid = false;
        }

        // Вывод результата валидации
        if (valid)
        {
            lblResult.Text = $"Данные корректны: {txtName.Text}, {txtEmail.Text}, {txtPhone.Text}, возраст {txtAge.Value}";
            lblResult.Foreground = new SolidColorBrush(Colors.DarkGreen);
        }
        else
        {
            lblResult.Text = "Исправьте ошибки и повторите";
            lblResult.Foreground = new SolidColorBrush(Colors.Red);
        }
    }
}
