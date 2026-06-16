using Directory.Domain.Entities;
using DirectoryService.Application.Departments.Interfaces;
using DirectoryService.Application.Exceptions;
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

    public async Task UpdateAsync(Department department, CancellationToken ct = default)
    {
        try
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Не удалось обновить департамент.");
            throw;
        }
    }

    public async Task<bool> LinkExistsAsync(Guid departmentId, Guid locationId, CancellationToken ct = default)
    {
        var links = await _context.DepartmentLocations
            .Where(dl => dl.DepartmentId == departmentId)
            .ToListAsync(ct);
        return links.Any(dl => dl.LocationId.Value == locationId);
    }
    public async Task AddLocationLinkAsync(DepartmentLocation link, CancellationToken ct)
    {
        await _context.DepartmentLocations.AddAsync(link, ct);
        try
        {
            await _context.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Не удалось создать связь локации с департаментом.");
            throw;
        }
    }

    public async Task RemoveLocationLinkAsync(Guid departmentId, Guid locationId, CancellationToken ct)
    {
        var links = await _context.DepartmentLocations
            .Where(dl => dl.DepartmentId == departmentId)
            .ToListAsync(ct);
    
        var link = links.FirstOrDefault(dl => dl.LocationId.Value == locationId);
    
        if (link == null)
            throw new DomainException("Связь не найдена.");
    
        try
        {
            _context.DepartmentLocations.Remove(link);
            await _context.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Не удалось удалить связь.");
            throw;
        }
    }

}
