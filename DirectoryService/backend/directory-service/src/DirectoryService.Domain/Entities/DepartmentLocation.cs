namespace Directory.Domain.Entities;

public sealed class DepartmentLocation
{
    public Guid DepartmentId { get; private set; }
    public LocationId LocationId { get; private set; } = null!;
    public bool IsPrimary { get; private set; }
    public DateTime AssignedAt { get; private set; }
    
    private DepartmentLocation() { }
    
    public static DepartmentLocation Create(Department department, Location location, bool isPrimary = false)
    {
        ArgumentNullException.ThrowIfNull(department);
        ArgumentNullException.ThrowIfNull(location);
        return new DepartmentLocation
        {
            DepartmentId = department.Id,
            LocationId = location.Id,
            IsPrimary = isPrimary,
            AssignedAt = DateTime.UtcNow
        };
    }
}
