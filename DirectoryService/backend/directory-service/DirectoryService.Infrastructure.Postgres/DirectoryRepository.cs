using Microsoft.EntityFrameworkCore;
using Directory.Domain.Entities;

namespace DirectoryService.Infrastructure.Postgres;

public class DirectoryRepository
{
    private readonly DirectoryServiceDbContext _dbContext;

    public DirectoryRepository(DirectoryServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddDepartment(Department department)
    {
        await _dbContext.Departments.AddAsync(department);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Department>> GetDepartments()
    {
        return await _dbContext.Departments.ToListAsync();
    }
}
