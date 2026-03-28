using System.Globalization;

namespace Lab08_Usability;

public class LanguageDetectPanel : Panel
{
    public LanguageDetectPanel()
    {
        Padding = new Padding(20);

        var layout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            Padding = new Padding(10)
        };

        var culture = CultureInfo.CurrentUICulture;
        string langCode = culture.TwoLetterISOLanguageName;

        string greeting = langCode switch
        {
            "ru" => "Здравствуйте! Обнаружен русский язык.",
            "en" => "Hello! English language detected.",
            "de" => "Hallo! Deutsche Sprache erkannt.",
            "fr" => "Bonjour! Langue fran\u00e7aise d\u00e9tect\u00e9e.",
            "es" => "Hola! Idioma espa\u00f1ol detectado.",
            _ => $"Hello! Detected language: {culture.DisplayName}"
        };

        var greetingLabel = new Label
        {
            Text = greeting,
            Font = new Font("Segoe UI", 18, FontStyle.Bold),
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 25)
        };

        var infoTitle = new Label
        {
            Text = "Информация о системной культуре:",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 10)
        };

        var details = new Label
        {
            Text =
                $"CurrentUICulture: {culture.Name} ({culture.DisplayName})\n" +
                $"CurrentCulture: {CultureInfo.CurrentCulture.Name} ({CultureInfo.CurrentCulture.DisplayName})\n" +
                $"Код языка (ISO 639-1): {langCode}\n" +
                $"Формат даты: {DateTime.Now.ToString("D", culture)}\n" +
                $"Формат числа: {1234567.89.ToString("N2", culture)}\n" +
                $"Формат валюты: {1234.50.ToString("C", culture)}",
            AutoSize = true,
            Font = new Font("Segoe UI", 10)
        };

        layout.Controls.Add(greetingLabel);
        layout.Controls.Add(infoTitle);
        layout.Controls.Add(details);
        Controls.Add(layout);
    }
}
