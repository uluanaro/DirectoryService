using Directory.Domain.Entities;
using DirectoryService.Application.Locations.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Infrastructure.Postgres;

public class EfLocationRepository : ILocationRepository
{
    private readonly DirectoryServiceDbContext _context;
    private readonly ILogger<EfLocationRepository> _logger;
    
    public EfLocationRepository (ILogger<EfLocationRepository>logger, DirectoryServiceDbContext context) {
        _logger = logger;
        _context = context;
    }

    public async Task AddAsync(Location location, CancellationToken ct = default)
    {
        await _context.Locations.AddAsync(location, ct);
        try
        {
            await _context.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Не удалось создать локацию.");
            throw;
        }
    }
    
    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default)
    {
        return await _context.Locations.AnyAsync(l => l.Name == name, ct);
    }
    
    public async Task<Location?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var allLocations = await _context.Locations.ToListAsync(ct);
        return allLocations.FirstOrDefault(l => l.Id.Value == id);
    }

    public async Task UpdateAsync(Location location, CancellationToken ct)
    {
            try
            {
                _context.Locations.Update(location);
                await _context.SaveChangesAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Не удалось обновить локацию.");
                throw;
            }
    }
}