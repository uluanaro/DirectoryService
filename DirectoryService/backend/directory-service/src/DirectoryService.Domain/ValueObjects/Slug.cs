using System.Text.RegularExpressions;

namespace DirectoryService.Domain.ValueObjects;

public record Slug
{
    private static readonly Regex ValidSlug = new(@"^[a-z0-9]+(-[a-z0-9]+)*$", RegexOptions.Compiled);

    public string Value { get; }

    private Slug(string value) => Value = value;

    public static Slug Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Slug не может быть пустым.");

        value = value.Trim().ToLowerInvariant().Replace(" ", "-");

        if (!ValidSlug.IsMatch(value))
            throw new ArgumentException(
                "Slug может содержать только строчные буквы, цифры и дефисы.");

        return new Slug(value);
    }

    public override string ToString() => Value;
}

