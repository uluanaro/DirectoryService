// Entities/Location.cs
namespace Directory.Domain.Entities;

public sealed class Location
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    
    private Location() { }

    public static Location Create(string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название локации не может быть пустым", nameof(name));

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Адрес не может быть пустым", nameof(address));

        return new Location
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Address = address.Trim()
        };
    }
}