using Directory.Domain.Entities;

namespace DirectoryService.Application.Departments.Interfaces;

public interface IDepartmentRepository
{
    Task<Department?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<bool> AllLocationsExistAsync(IEnumerable<Guid> locationsId, CancellationToken ct);
    Task AddAsync(Department department, IEnumerable<DepartmentLocation> locations, CancellationToken ct);
}