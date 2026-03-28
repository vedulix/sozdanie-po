using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace Avalonia_Lab08_Usability;

/// <summary>
/// Упр.6: Локализация интерфейса.
/// Переключение языка через ComboBox (русский/английский).
/// Вместо .resx используем словарь строк — кроссплатформенная альтернатива.
/// </summary>
public class LocalizationPanel : UserControl
{
    // Словари локализованных строк (замена .resx)
    private static readonly Dictionary<string, Dictionary<string, string>> Strings = new()
    {
        ["ru"] = new()
        {
            ["Greeting"] = "Добро пожаловать!",
            ["NameLabel"] = "Имя:",
            ["EmailLabel"] = "Электронная почта:",
            ["LanguageLabel"] = "Язык:",
            ["SaveButton"] = "Сохранить",
            ["CancelButton"] = "Отмена",
            ["StatusReady"] = "Готово",
            ["SavedMessage"] = "Данные сохранены!"
        },
        ["en"] = new()
        {
            ["Greeting"] = "Welcome!",
            ["NameLabel"] = "Name:",
            ["EmailLabel"] = "Email:",
            ["LanguageLabel"] = "Language:",
            ["SaveButton"] = "Save",
            ["CancelButton"] = "Cancel",
            ["StatusReady"] = "Ready",
            ["SavedMessage"] = "Data saved!"
        }
    };

    private readonly TextBlock _greetingLabel;
    private readonly TextBlock _nameLabel;
    private readonly TextBlock _emailLabel;
    private readonly TextBlock _langLabel;
    private readonly TextBlock _statusLabel;
    private readonly Button _saveBtn;
    private readonly Button _cancelBtn;
    private readonly TextBox _nameBox;
    private readonly TextBox _emailBox;

    public LocalizationPanel()
    {
        var description = new TextBlock
        {
            Text = "Переключение языка интерфейса.\nВыберите язык в ComboBox ниже — все подписи обновятся.",
            Margin = new Thickness(0, 0, 0, 15),
            TextWrapping = TextWrapping.Wrap
        };

        // Выбор языка
        _langLabel = new TextBlock { VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 5, 0) };
        var langCombo = new ComboBox { Width = 200 };
        langCombo.Items.Add("Русский (по умолчанию)");
        langCombo.Items.Add("English");
        langCombo.SelectedIndex = 0;

        var langRow = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 5,
            Margin = new Thickness(0, 0, 0, 15)
        };
        langRow.Children.Add(_langLabel);
        langRow.Children.Add(langCombo);

        _greetingLabel = new TextBlock
        {
            FontSize = 20,
            FontWeight = FontWeight.Bold,
            Margin = new Thickness(0, 0, 0, 15)
        };

        _nameLabel = new TextBlock();
        _nameBox = new TextBox { Width = 300 };

        _emailLabel = new TextBlock { Margin = new Thickness(0, 10, 0, 0) };
        _emailBox = new TextBox { Width = 300 };

        _saveBtn = new Button { Width = 120 };
        _cancelBtn = new Button { Width = 120 };
        var btnRow = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 10,
            Margin = new Thickness(0, 15, 0, 0)
        };
        btnRow.Children.Add(_saveBtn);
        btnRow.Children.Add(_cancelBtn);

        _statusLabel = new TextBlock
        {
            Foreground = Brushes.Green,
            Margin = new Thickness(0, 10, 0, 0)
        };

        var layout = new StackPanel { Margin = new Thickness(20), Spacing = 4 };
        layout.Children.Add(description);
        layout.Children.Add(langRow);
        layout.Children.Add(_greetingLabel);
        layout.Children.Add(_nameLabel);
        layout.Children.Add(_nameBox);
        layout.Children.Add(_emailLabel);
        layout.Children.Add(_emailBox);
        layout.Children.Add(btnRow);
        layout.Children.Add(_statusLabel);

        Content = layout;

        // Применяем начальную локаль
        ApplyLocale("ru");

        langCombo.SelectionChanged += (_, _) =>
        {
            string lang = langCombo.SelectedIndex == 1 ? "en" : "ru";
            ApplyLocale(lang);
        };

        _saveBtn.Click += (_, _) =>
        {
            string lang = langCombo.SelectedIndex == 1 ? "en" : "ru";
            _statusLabel.Text = GetString(lang, "SavedMessage");
        };

        _cancelBtn.Click += (_, _) =>
        {
            _nameBox.Text = "";
            _emailBox.Text = "";
            _statusLabel.Text = "";
        };
    }

    /// <summary>Получить строку из словаря по ключу и языку.</summary>
    private static string GetString(string lang, string key)
    {
        if (Strings.TryGetValue(lang, out var dict) && dict.TryGetValue(key, out var val))
            return val;
        return key;
    }

    /// <summary>Применить выбранную локаль ко всем элементам.</summary>
    private void ApplyLocale(string lang)
    {
        _greetingLabel.Text = GetString(lang, "Greeting");
        _nameLabel.Text = GetString(lang, "NameLabel");
        _emailLabel.Text = GetString(lang, "EmailLabel");
        _langLabel.Text = GetString(lang, "LanguageLabel");
        _saveBtn.Content = GetString(lang, "SaveButton");
        _cancelBtn.Content = GetString(lang, "CancelButton");
        _statusLabel.Text = GetString(lang, "StatusReady");
    }
}
