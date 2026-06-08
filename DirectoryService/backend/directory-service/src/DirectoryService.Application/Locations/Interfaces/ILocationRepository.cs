using Directory.Domain.Entities;


namespace DirectoryService.Application.Locations.Interfaces;

public interface ILocationRepository
{
    Task AddAsync(Location location, CancellationToken ct = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default);
}