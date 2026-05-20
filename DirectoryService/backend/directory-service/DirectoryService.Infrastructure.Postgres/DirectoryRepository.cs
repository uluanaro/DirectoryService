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
        await _dbContext.AddAsync(department);
    }
    
    public async Task<List<DepartmentDto>> GetDepartment()
    {

        var departments = await _dbContext.Departments.Select(d => new DepartmentDto(d.Id, d.Name)).ToListAsync();
        return departments;

    }
}