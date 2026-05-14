// Entities/DepartmentLocation.cs — соединительная сущность
namespace Directory.Domain.Entities;

public sealed class DepartmentLocation
{
    public Guid DepartmentId { get; private set; }
    public Guid LocationId { get; private set; }
    public bool IsPrimary { get; private set; } // 1 - arribute of connection
    public DateTime AssignedAt { get; private set; }// 2 - attribute of connection

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