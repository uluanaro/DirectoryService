// ValueObjects/Slug.cs
namespace Directory.Domain.ValueObjects;

using System.Text.RegularExpressions;

public sealed class Slug
{
    private static readonly Regex ValidSlug = new(@"^[a-z0-9]+(-[a-z0-9]+)*$", RegexOptions.Compiled);

    public string Value { get; }

    private Slug(string value) => Value = value;

    public static Slug Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Slug не может быть пустым", nameof(value));

        value = value.Trim().ToLowerInvariant();

        if (!ValidSlug.IsMatch(value))
            throw new ArgumentException(
                "Slug может содержать только строчные буквы, цифры и дефисы (не в начале/конце)",
                nameof(value));

        if (value.Length > 100)
            throw new ArgumentException("Slug слишком длинный", nameof(value));

        return new Slug(value);
    }

    public override bool Equals(object? obj) =>
        obj is Slug other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;
}