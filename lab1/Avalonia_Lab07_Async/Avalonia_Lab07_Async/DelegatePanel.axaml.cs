using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia_Lab07_Async;

// Определяем делегат для математической операции
public delegate double MathOperation(int n);

/// <summary>
/// Упр.2: Демонстрация делегатов.
/// Делегат MathOperation указывает на одну из трёх функций.
/// </summary>
public partial class DelegatePanel : UserControl
{
    public DelegatePanel()
    {
        InitializeComponent();
        CalcBtn.Click += OnCalculate;
    }

    private void OnCalculate(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(InputBox.Text, out int n) || n < 0)
        {
            ResultLabel.Text = "Ошибка: введите целое неотрицательное число";
            return;
        }

        // Выбор делегата в зависимости от операции
        MathOperation operation = OperationBox.SelectedIndex switch
        {
            0 => Square,
            1 => Cube,
            2 => Factorial,
            _ => Square
        };

        // Вызов операции через делегат
        double result = operation.Invoke(n);
        ResultLabel.Text = $"Результат: {result:N0}";
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
