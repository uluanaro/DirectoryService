using System.Data;
using Dapper;
using Directory.Domain.Entities;
using DirectoryService.Application.Locations.Interfaces;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Infrastructure.Postgres;


    
public class DapperLocationRepository: ILocationRepository
{
    private readonly IDbConnection _connection;
    private readonly ILogger<DapperLocationRepository> _logger;
    
    public DapperLocationRepository (IDbConnection connection, ILogger<DapperLocationRepository> logger) {
        _logger = logger;
        _connection = connection;
    }
    public async Task AddAsync(Location location, CancellationToken ct = default)
    {
        
        var sql = "INSERT INTO locations (id, name, address) VALUES(@Id, @Name, @Address)";
        var parameters = new { Id =location.Id.Value, location.Name, location.Address };

        try
        {
            await _connection.ExecuteAsync(sql, parameters);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Не удалось добавить локацию.");
            throw;
        }
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default)
    {
        var sql = "SELECT COUNT(1) FROM locations WHERE name = @Name";

        try
        {
            var count = await _connection.ExecuteScalarAsync<int>(sql, new { Name = name });
            return count > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Не удалось найти локацию.");
            throw;
        }
    }
    
    public async Task<Location?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var sql = "SELECT id, name, address FROM locations WHERE id = @Id";

        try
        {
            var result = await _connection.QueryFirstOrDefaultAsync(sql, new { Id = id });
            if (result == null) return null;

            var location = Location.Create(result.name, result.address);
            return location;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Не удалось найти локацию.");
            throw;
        }
    }

    public async Task UpdateAsync(Location location, CancellationToken ct)
    {
        var sql = "UPDATE locations SET name = @Name, address = @Address WHERE id = @Id";
        try
        {
            await _connection.ExecuteAsync(sql, new { location.Name, location.Address, Id = location.Id.Value });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Не удалось обновить локацию.");
            throw;
        }
    }
}