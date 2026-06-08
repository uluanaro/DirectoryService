using Directory.Domain.Entities;
using DirectoryService.Application.Locations.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DirectoryService.Infrastructure.Postgres;

public class LocationRepository : ILocationRepository
{
    private readonly DirectoryServiceDbContext _context;

    public LocationRepository(DirectoryServiceDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Location location, CancellationToken ct = default)
    {
        await _context.Locations.AddAsync(location, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default)
    {
        return await _context.Locations.AnyAsync(l => l.Name == name, ct);
    }
}