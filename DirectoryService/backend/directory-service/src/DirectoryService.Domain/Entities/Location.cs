// Entities/Location.cs
namespace Directory.Domain.Entities;

public sealed class Location
{
    public Guid Id { get; private set; }
    public LocationName Name { get; private set; }
    public Address Address { get; private set; }

    private Location() { }

    public static Location Create(LocationName name, Address address)
    {

        return new Location
        {
            Id = Guid.NewGuid(),
            Name = name,
            Address = address
        };
    }
}
    
    public sealed partial record LocationName
    {
        public string Value { get; }

        public LocationName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Место не может не принимать значение");

            Value = value.Trim();
        }

        public static implicit operator string(LocationName name) => name.Value;
        public static explicit operator LocationName(string value) => new(value);

        public override string ToString() => Value;
        public override int GetHashCode() => Value.GetHashCode();
    }

    public sealed record Address
    {
        public string Value { get; }

        public Address(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Адрес не может быть пустым.");

            Value = value.Trim();
        }

        public static implicit operator string(Address address) => address.Value;
        public static explicit operator Address(string value) => new(value);

        public override string ToString() => Value;
        public override int GetHashCode() => Value.GetHashCode();
    }
