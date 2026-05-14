// Entities/DepartmentPosition.cs — соединительная сущность
namespace Directory.Domain.Entities;

public sealed class DepartmentPosition
{
    public Guid DepartmentId { get; private set; }
    public Guid PositionId { get; private set; }
    public DateTime AssignedAt { get; private set; }

    private DepartmentPosition() { }

    public static DepartmentPosition Create(Department department, Position position)
    {
        ArgumentNullException.ThrowIfNull(department);
        ArgumentNullException.ThrowIfNull(position);

        return new DepartmentPosition
        {
            DepartmentId = department.Id,
            PositionId = position.Id,
            AssignedAt = DateTime.UtcNow
        };
    }
}