namespace Lab04_Dialogs;

/// <summary>
/// Упражнение 4: Немодальный диалог поиска текста.
/// Ищет текст в RichTextBox главной формы и подсвечивает совпадения.
/// </summary>
public class FindDialog : Form
{
    private readonly TextBox _txtSearch;
    private readonly Button _btnFind;
    private readonly RichTextBox _targetRichTextBox;

    public FindDialog(RichTextBox target)
    {
        _targetRichTextBox = target;

        Text = "Поиск";
        Size = new Size(350, 120);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        StartPosition = FormStartPosition.CenterParent;
        TopMost = true;

        _txtSearch = new TextBox { Location = new Point(10, 15), Width = 220 };

        _btnFind = new Button
        {
            Text = "Найти далее",
            Location = new Point(240, 13),
            Size = new Size(90, 27)
        };
        _btnFind.Click += OnFindClick;

        AcceptButton = _btnFind;
        Controls.AddRange(new Control[] { _txtSearch, _btnFind });
    }

    private void OnFindClick(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_txtSearch.Text))
            return;

        // Сбросить выделение
        _targetRichTextBox.SelectAll();
        _targetRichTextBox.SelectionBackColor = _targetRichTextBox.BackColor;
        _targetRichTextBox.DeselectAll();

        // Подсветить все совпадения
        int startIndex = 0;
        int count = 0;
        while (startIndex < _targetRichTextBox.TextLength)
        {
            int idx = _targetRichTextBox.Find(_txtSearch.Text, startIndex, RichTextBoxFinds.None);
            if (idx == -1) break;

            _targetRichTextBox.Select(idx, _txtSearch.Text.Length);
            _targetRichTextBox.SelectionBackColor = Color.Yellow;
            startIndex = idx + _txtSearch.Text.Length;
            count++;
        }

        if (count == 0)
            MessageBox.Show("Текст не найден", "Поиск", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        // Скрываем вместо закрытия, чтобы можно было переоткрыть
        if (e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
            Hide();
        }
        base.OnFormClosing(e);
    }
}
