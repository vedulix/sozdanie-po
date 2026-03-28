using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia_Lab04_Dialogs;

/// <summary>
/// Упражнение 4: НЕМОДАЛЬНЫЙ диалог поиска текста.
/// Открывается через Show (а не ShowDialog) — главное окно остаётся активным.
/// Ищет текст в TextBox главной формы и выделяет совпадения.
/// </summary>
public partial class FindDialog : Window
{
    // Ссылка на текстовое поле главной формы, в котором ищем
    private TextBox _targetTextBox = null!;

    public FindDialog()
    {
        InitializeComponent();
    }

    public FindDialog(TextBox target) : this()
    {
        _targetTextBox = target;
        BtnFindNext.Click += OnFindClick;

        // При закрытии — скрываем окно вместо уничтожения (можно переоткрыть)
        Closing += (_, args) =>
        {
            args.Cancel = true;
            Hide();
        };
    }

    /// <summary>
    /// Поиск и выделение текста.
    /// В Avalonia TextBox нет подсветки фона выделения как в RichTextBox,
    /// поэтому используем SelectionStart/SelectionEnd для выделения найденного фрагмента.
    /// </summary>
    private void OnFindClick(object? sender, RoutedEventArgs e)
    {
        var searchText = TxtSearch.Text;
        if (string.IsNullOrEmpty(searchText))
            return;

        var fullText = _targetTextBox.Text ?? "";

        // Начинаем поиск после текущей позиции каретки
        int startPos = _targetTextBox.CaretIndex;
        int idx = fullText.IndexOf(searchText, startPos, StringComparison.OrdinalIgnoreCase);

        // Если не нашли — ищем с начала
        if (idx == -1 && startPos > 0)
        {
            idx = fullText.IndexOf(searchText, 0, StringComparison.OrdinalIgnoreCase);
        }

        if (idx >= 0)
        {
            // Выделяем найденный текст
            _targetTextBox.Focus();
            _targetTextBox.SelectionStart = idx;
            _targetTextBox.SelectionEnd = idx + searchText.Length;
            _targetTextBox.CaretIndex = idx + searchText.Length;

            // Подсчитываем общее количество совпадений
            int count = 0;
            int pos = 0;
            while ((pos = fullText.IndexOf(searchText, pos, StringComparison.OrdinalIgnoreCase)) >= 0)
            {
                count++;
                pos += searchText.Length;
            }
            LblStatus.Text = $"Найдено совпадений: {count}";
        }
        else
        {
            LblStatus.Text = "Текст не найден";
            _targetTextBox.SelectionStart = 0;
            _targetTextBox.SelectionEnd = 0;
        }
    }
}
