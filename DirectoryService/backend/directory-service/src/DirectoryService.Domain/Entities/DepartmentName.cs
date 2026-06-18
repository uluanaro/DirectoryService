using DirectoryService.Domain;

namespace Directory.Domain.Entities;

public record DepartmentName
{
    public string Prefix { get; }
    public string Name { get; }
    
    private DepartmentName() { }

    private DepartmentName(string prefix, string name)
    {
        Prefix = prefix;
        Name = name;
    }

    public override string ToString() => $"{Prefix}-{Name}";
   
    public static DepartmentName Create(string prefix, string name)
    {
        if (string.IsNullOrWhiteSpace(prefix) || string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Department name cannot be empty or whitespace");
        }

        if (prefix.Length > 50 || name.Length > 50)
        {
            throw new ArgumentException("Department name is too long");
        }

        return new DepartmentName(prefix, name);
    }
}
