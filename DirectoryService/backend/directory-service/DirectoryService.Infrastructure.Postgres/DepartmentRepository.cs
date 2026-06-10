using Directory.Domain.Entities;
using DirectoryService.Application.Departments.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Infrastructure.Postgres;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly DirectoryServiceDbContext _context;
    private readonly ILogger<DepartmentRepository> _logger;
     
     public DepartmentRepository(ILogger<DepartmentRepository>logger, DirectoryServiceDbContext context) {
        _logger = logger;
        _context = context;
    }
    
    public async Task<Department?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Departments
            .FirstOrDefaultAsync(d => d.Id == id, ct);
    }

    public async Task<bool> AllLocationsExistAsync(IEnumerable<Guid> locationsId, CancellationToken ct)
    {
        var locationsList = locationsId.ToList(); 
        // TODO: загружает все локации в память — оптимизировать через WHERE id IN (...) при росте данных
        var existingIds = await _context.Locations
            .Select(l => l.Id.Value)
            .ToListAsync(ct);
        return locationsList.All(id => existingIds.Contains(id));
    }

    public async Task AddAsync(Department department, IEnumerable<DepartmentLocation> locations, CancellationToken ct)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(ct);
        try
        {
            // 1. добавить department
            // 2. добавить locations
            // 3. сохранить
            // 4. commit
            await _context.Departments.AddAsync(department, ct);
            await _context.DepartmentLocations.AddRangeAsync(locations, ct);
            await _context.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(ct);
            _logger.LogError(ex, "Не удалось создать департамент.");
            throw;
        }
        
    }
}
