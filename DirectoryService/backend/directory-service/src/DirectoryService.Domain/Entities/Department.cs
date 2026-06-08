using Directory.Domain.Entities;
using DirectoryService.Domain.ValueObjects;

namespace Directory.Domain.Entities;

public sealed class Department
{
    public Guid Id { get; private set; }
    public DepartmentName Name { get; private set; } = null!;
    public Slug Slug { get; private set; } = null!;
    public Guid? ParentId { get; private set; }
    
    private readonly List<DepartmentLocation> _locations = new();
    public IReadOnlyList<DepartmentLocation> Locations => _locations.AsReadOnly();
    
    private readonly List<DepartmentPosition> _positions = new();
    public IReadOnlyList<DepartmentPosition> Positions => _positions.AsReadOnly();
    
    private Department() { }
    
    public static Department Create(DepartmentName name, Slug slug, Guid? parentId = null)
    {
        return new Department
        {
            Id = Guid.NewGuid(),
            Name = name,
            Slug = slug,
            ParentId = parentId
        };
    }
    
    public void Rename(DepartmentName newName)
    {
        Name = newName;
    }
    
    public void AddLocation(Location location, bool isPrimary = false)
    {
        ArgumentNullException.ThrowIfNull(location);
        if (_locations.Any(l => l.LocationId == location.Id))
            throw new InvalidOperationException("Эта локация уже привязана к подразделению");
        _locations.Add(DepartmentLocation.Create(this, location, isPrimary));
    }
    
    public void AddPosition(Position position)
    {
        ArgumentNullException.ThrowIfNull(position);
        if (_positions.Any(p => p.PositionId == position.Id))
            throw new InvalidOperationException("Эта должность уже привязана к подразделению");
        _positions.Add(DepartmentPosition.Create(this, position));
    }
}
