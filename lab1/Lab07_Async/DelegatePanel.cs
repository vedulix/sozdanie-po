namespace Lab07_Async;

// Определяем делегат для математической операции
public delegate double MathOperation(int n);

public class DelegatePanel : Panel
{
    private readonly TextBox _inputBox;
    private readonly ComboBox _operationBox;
    private readonly Label _resultLabel;

    public DelegatePanel()
    {
        Padding = new Padding(20);

        var layout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Padding = new Padding(10)
        };

        var description = new Label
        {
            Text = "Демонстрация использования делегатов.\n" +
                   "Выберите операцию, введите число и нажмите «Вычислить».",
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 15)
        };

        var inputLabel = new Label { Text = "Число:", AutoSize = true };
        _inputBox = new TextBox { Width = 200, Text = "5" };

        var opLabel = new Label { Text = "Операция:", AutoSize = true, Margin = new Padding(0, 10, 0, 0) };
        _operationBox = new ComboBox
        {
            Width = 200,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        _operationBox.Items.AddRange(new object[] { "Квадрат (n²)", "Куб (n³)", "Факториал (n!)" });
        _operationBox.SelectedIndex = 0;

        var calcBtn = new Button { Text = "Вычислить", Width = 120, Margin = new Padding(0, 10, 0, 0) };

        _resultLabel = new Label
        {
            Text = "Результат: —",
            AutoSize = true,
            Font = new Font("Segoe UI", 14, FontStyle.Bold),
            Margin = new Padding(0, 15, 0, 0)
        };

        layout.Controls.Add(description);
        layout.Controls.Add(inputLabel);
        layout.Controls.Add(_inputBox);
        layout.Controls.Add(opLabel);
        layout.Controls.Add(_operationBox);
        layout.Controls.Add(calcBtn);
        layout.Controls.Add(_resultLabel);
        Controls.Add(layout);

        calcBtn.Click += OnCalculate;
    }

    private void OnCalculate(object? sender, EventArgs e)
    {
        if (!int.TryParse(_inputBox.Text, out int n) || n < 0)
        {
            _resultLabel.Text = "Ошибка: введите целое неотрицательное число";
            return;
        }

        // Выбор делегата в зависимости от операции
        MathOperation operation = _operationBox.SelectedIndex switch
        {
            0 => Square,
            1 => Cube,
            2 => Factorial,
            _ => Square
        };

        double result = operation.Invoke(n);
        _resultLabel.Text = $"Результат: {result:N0}";
    }

    private static double Square(int n) => (double)n * n;
    private static double Cube(int n) => (double)n * n * n;

    private static double Factorial(int n)
    {
        if (n <= 1) return 1;
        double result = 1;
        for (int i = 2; i <= n; i++)
            result *= i;
        return result;
    }
}
