namespace Lab03_CustomControls;

/// <summary>
/// Упражнение 2: Специализированный элемент управления.
/// TextBox, принимающий только цифры и десятичную точку.
/// </summary>
public class NumericTextBox : TextBox
{
    public double Value
    {
        get => double.TryParse(Text, System.Globalization.NumberStyles.Float,
            System.Globalization.CultureInfo.InvariantCulture, out var v) ? v : 0;
        set => Text = value.ToString(System.Globalization.CultureInfo.InvariantCulture);
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        base.OnKeyPress(e);

        // Разрешаем: цифры, точку (одну), запятую (одну), Backspace
        if (char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
            return;

        if ((e.KeyChar == '.' || e.KeyChar == ',') && !Text.Contains('.') && !Text.Contains(','))
            return;

        e.Handled = true; // блокируем все остальные символы
    }
}
