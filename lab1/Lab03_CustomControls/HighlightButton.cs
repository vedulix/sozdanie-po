namespace Lab03_CustomControls;

/// <summary>
/// Упражнение 3: Расширенный элемент управления.
/// Кнопка, меняющая цвет фона при наведении курсора.
/// </summary>
public class HighlightButton : Button
{
    private Color _normalColor = SystemColors.Control;
    private Color _highlightColor = Color.LightSkyBlue;

    public Color HighlightColor
    {
        get => _highlightColor;
        set => _highlightColor = value;
    }

    public HighlightButton()
    {
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 1;
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        _normalColor = BackColor;
        BackColor = _highlightColor;
        base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        BackColor = _normalColor;
        base.OnMouseLeave(e);
    }
}
