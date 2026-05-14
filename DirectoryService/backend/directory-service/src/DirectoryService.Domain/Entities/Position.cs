// Entities/Position.cs
namespace Directory.Domain.Entities;

public sealed class Position
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    private Position() { }

    public static Position Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название должности не может быть пустым", nameof(name));

        return new Position
        {
            Id = Guid.NewGuid(),
            Name = name.Trim()
        };
    }
}