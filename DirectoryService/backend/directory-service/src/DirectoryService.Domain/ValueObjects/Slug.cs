namespace Directory.Domain.ValueObjects;

public record Slug
{
    public string Value { get; }
    
    private Slug(string value)
    {
        Value = value;
    }
    
    public static Slug Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Slug cannot be empty");
            
        var slug = value.ToLower().Trim().Replace(" ", "-");
        return new Slug(slug);
    }
    
    public override string ToString() => Value;
}
