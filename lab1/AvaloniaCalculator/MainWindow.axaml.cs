using Avalonia.Controls;
using Avalonia.Interactivity;
using Calculator;

namespace AvaloniaCalculator;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        new CalcEngine();
        CalcEngine.CalcReset();
    }

    // Нажатие цифры — вызывает CalcEngine.CalcNumber()
    private void OnNumber(object? sender, RoutedEventArgs e)
    {
        var btn = (Button)sender!;
        var result = CalcEngine.CalcNumber(btn.Content!.ToString()!);
        Display.Text = result;
    }

    // Оператор (+, −, ×, ÷, ^) — вызывает CalcEngine.CalcOperation()
    private void OnOperator(object? sender, RoutedEventArgs e)
    {
        var btn = (Button)sender!;
        var op = btn.Content!.ToString() switch
        {
            "+" => CalcEngine.Operator.eAdd,
            "−" => CalcEngine.Operator.eSubtract,
            "×" => CalcEngine.Operator.eMultiply,
            "÷" => CalcEngine.Operator.eDivide,
            "^" => CalcEngine.Operator.ePower,
            _ => CalcEngine.Operator.eUnknown
        };
        CalcEngine.CalcOperation(op);
    }

    // Равно — CalcEngine.CalcEqual()
    private void OnEqual(object? sender, RoutedEventArgs e)
    {
        Display.Text = CalcEngine.CalcEqual();
    }

    // Сброс — CalcEngine.CalcReset()
    private void OnClear(object? sender, RoutedEventArgs e)
    {
        CalcEngine.CalcReset();
        Display.Text = "0";
    }

    // Десятичная точка
    private void OnDecimal(object? sender, RoutedEventArgs e)
    {
        Display.Text = CalcEngine.CalcDecimal();
    }

    // Смена знака ±
    private void OnSign(object? sender, RoutedEventArgs e)
    {
        Display.Text = CalcEngine.CalcSign();
    }

    // Квадратный корень √
    private void OnSqrt(object? sender, RoutedEventArgs e)
    {
        Display.Text = CalcEngine.CalcSqrt();
    }

    // Квадрат числа x²
    private void OnSquare(object? sender, RoutedEventArgs e)
    {
        Display.Text = CalcEngine.CalcSquare();
    }

    // Обратное число 1/x
    private void OnReciprocal(object? sender, RoutedEventArgs e)
    {
        Display.Text = CalcEngine.CalcReciprocal();
    }

    // Факториал n!
    private void OnFactorial(object? sender, RoutedEventArgs e)
    {
        Display.Text = CalcEngine.CalcFactorial();
    }

    // Кубический корень ³√
    private void OnCubeRoot(object? sender, RoutedEventArgs e)
    {
        Display.Text = CalcEngine.CalcCubeRoot();
    }
}
