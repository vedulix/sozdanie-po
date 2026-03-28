namespace Avalonia_Lab08_Usability;

/// <summary>
/// Модель контакта для сериализации в JSON.
/// </summary>
[Serializable]
public class Contact
{
    public string Name { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";
}
