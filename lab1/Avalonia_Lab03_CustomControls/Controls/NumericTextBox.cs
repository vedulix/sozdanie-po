using System.Globalization;
using Avalonia.Controls;
using Avalonia.Input;

namespace Avalonia_Lab03_CustomControls.Controls;

/// <summary>
/// Упражнение 2: Специализированный элемент управления.
/// TextBox, принимающий только цифры и десятичную точку/запятую.
/// </summary>
public class NumericTextBox : TextBox
{
    /// <summary>
    /// Числовое значение из текстового поля (0 если текст некорректен).
    /// </summary>
    public double Value
    {
        get => double.TryParse(Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var v) ? v : 0;
        set => Text = value.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Фильтрация ввода: разрешаем только цифры, точку и запятую (одну).
    /// </summary>
    protected override void OnTextInput(TextInputEventArgs e)
    {
        if (e.Text == null)
        {
            base.OnTextInput(e);
            return;
        }

        foreach (char c in e.Text)
        {
            // Разрешаем цифры
            if (char.IsDigit(c))
                continue;

            // Разрешаем одну точку или запятую
            if ((c == '.' || c == ',') && Text != null && !Text.Contains('.') && !Text.Contains(','))
                continue;

            // Блокируем все остальные символы
            e.Handled = true;
            return;
        }

        base.OnTextInput(e);
    }
}
