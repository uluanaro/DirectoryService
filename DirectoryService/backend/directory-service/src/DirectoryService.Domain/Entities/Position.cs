namespace Directory.Domain.Entities;

public class Position
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    
    private Position() { }
    
    public static Position Create(string name, string description = "")
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Имя не может быть пустым.");
        return new Position
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description ?? string.Empty
        };
    }
}