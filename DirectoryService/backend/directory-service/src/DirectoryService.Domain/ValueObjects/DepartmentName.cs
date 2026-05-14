// ValueObjects/DepartmentName.cs
namespace Directory.Domain.ValueObjects;

public sealed class DepartmentName
{
    public string Value { get; }

    private DepartmentName(string value) => Value = value;

    public static DepartmentName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Название не может быть пустым", nameof(value));

        value = value.Trim();

        if (value.Length < 2)
            throw new ArgumentException("Название слишком короткое (минимум 2 символа)", nameof(value));

        if (value.Length > 100)
            throw new ArgumentException("Название слишком длинное (максимум 100 символов)", nameof(value));

        return new DepartmentName(value);
    }
    
    public override bool Equals(object? obj) =>
        obj is DepartmentName other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;
}