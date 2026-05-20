using Directory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DirectoryService.Infrastructure.Postgres;

public class DirectoryServiceDbContext: DbContext
{
    public readonly string _connectionString;

    public DirectoryServiceDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql();
    }

    public DbSet<Department> Departments => Set<Department>();
}

public class AppDbContext
{
    private readonly string _connectionString;

    public AppDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Task GetDepartments()
    {
        
    }
}





public class DirectoryRepository
{
    private readonly DirectoryServiceDbContext _dbContext;
    
    public DirectoryRepository(DirectoryServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public async Task AddDepartment(Department department)
    {
        // var connectionString = "Server=127.0.0.1;Port=5432;Database=myDataBase;User Id=myUsername;Password=myPassword;PostgreSQL"
        //
        // await using var dataSource = NpgsqlDataSource.Create(connectionString);
        //
        // var sql = "INSERT INTO departments (department_id, name) VALUES (@department_id, @name)";
        //
        // var command = dataSource.CreateCommand(sql);
        // command.Parameters.Add(new NpgsqlParameter("id", department.Id));
        // command.Parameters.Add(new NpgsqlParameter("name", department.Name));
        //
        // await command.ExecuteNonQueryAsync();
        
        
        await _dbContext.AddAsync(department);
    }
    
    public async Task<List<DepartmentDto>> GetDepartment()
    {
        // var connectionString = "Server=127.0.0.1;Port=5432;Database=myDataBase;User Id=myUsername;Password=myPassword;PostgreSQL"
        //
        // await using var dataSource = NpgsqlDataSource.Create(connectionString);
        //
        // var command = dataSource.CreateCommand("SELECT id, name FROM departments");
        //
        // await using var reader = await command.ExecuteReaderAsync();
        //
        // var departments = new List<DepartmentDto>();
        //
        // while (await reader.ReadAsync())
        // {
        //     var departmentDto = new DepartmentDto(reader.GetGuid(0), reader.GetString(1));
        //     
        //     departments.Add(departmentDto);
        // }
        //
        // return departments;
//var departments = await _dbContext.Set<Department>().Select(d =>new DepartmentDto()).ToListAsync()
var departments = await _dbContext.Departments.Select(d =>new DepartmentDto(d.Id, d.Name)).ToListAsync()
return departments;

    }
}

public record DepartmentDto(Guid Id, string Name);