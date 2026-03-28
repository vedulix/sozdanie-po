using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia_Lab08_Usability;

/// <summary>
/// Упр.5: Определение системного языка.
/// Показывает приветствие на языке системы и информацию о культуре.
/// </summary>
public class LanguageDetectPanel : UserControl
{
    public LanguageDetectPanel()
    {
        var culture = CultureInfo.CurrentUICulture;
        string langCode = culture.TwoLetterISOLanguageName;

        // Приветствие на языке системы
        string greeting = langCode switch
        {
            "ru" => "Здравствуйте! Обнаружен русский язык.",
            "en" => "Hello! English language detected.",
            "de" => "Hallo! Deutsche Sprache erkannt.",
            "fr" => "Bonjour! Langue française détectée.",
            "es" => "¡Hola! Idioma español detectado.",
            _ => $"Hello! Detected language: {culture.DisplayName}"
        };

        var greetingLabel = new TextBlock
        {
            Text = greeting,
            FontSize = 22,
            FontWeight = FontWeight.Bold,
            Margin = new Thickness(0, 0, 0, 25)
        };

        var infoTitle = new TextBlock
        {
            Text = "Информация о системной культуре:",
            FontSize = 14,
            FontWeight = FontWeight.Bold,
            Margin = new Thickness(0, 0, 0, 10)
        };

        // Детали о текущей культуре системы
        var details = new TextBlock
        {
            Text =
                $"CurrentUICulture: {culture.Name} ({culture.DisplayName})\n" +
                $"CurrentCulture: {CultureInfo.CurrentCulture.Name} ({CultureInfo.CurrentCulture.DisplayName})\n" +
                $"Код языка (ISO 639-1): {langCode}\n" +
                $"Формат даты: {DateTime.Now.ToString("D", culture)}\n" +
                $"Формат числа: {1234567.89.ToString("N2", culture)}\n" +
                $"Формат валюты: {1234.50.ToString("C", culture)}",
            FontSize = 13,
            TextWrapping = TextWrapping.Wrap
        };

        var layout = new StackPanel { Margin = new Thickness(20), Spacing = 4 };
        layout.Children.Add(greetingLabel);
        layout.Children.Add(infoTitle);
        layout.Children.Add(details);

        Content = layout;
    }
}
