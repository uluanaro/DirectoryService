namespace Directory.Domain.Entities;

public class Position
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    
    private Position() { }
    
    public static Position Create(string name, string description = "")
    {
        return new Position
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description ?? string.Empty
        };
    }
}