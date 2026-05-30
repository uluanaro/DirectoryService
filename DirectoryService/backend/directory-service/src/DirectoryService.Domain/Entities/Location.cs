namespace Directory.Domain.Entities;

public record LocationId(Guid Value);

public class Location
{
    public LocationId Id { get; private set; } = null!;
    public string Name { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    
    private Location() { }
    
    public static Location Create(string name, string address = "")
    {
        return new Location
        {
            Id = new LocationId(Guid.NewGuid()),
            Name = name,
            Address = address ?? string.Empty
        };
    }
}
