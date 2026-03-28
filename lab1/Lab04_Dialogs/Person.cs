namespace Lab04_Dialogs;

/// <summary>
/// Упражнение 3: Класс Person для передачи данных между формами.
/// </summary>
public class Person
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }

    public override string ToString() => $"{Name}, возраст: {Age}";
}
