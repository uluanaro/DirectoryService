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

    public Task GetDepartments()
    {
        
    }
}


public record DepartmentDto(Guid Id, string Name);