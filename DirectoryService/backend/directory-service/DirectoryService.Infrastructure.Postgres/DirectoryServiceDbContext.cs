using Directory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DirectoryService.Infrastructure.Postgres;

public class DirectoryServiceDbContext : DbContext
{
    
    public DirectoryServiceDbContext(DbContextOptions<DirectoryServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(DirectoryServiceDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Position> Positions => Set<Position>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<DepartmentLocation> DepartmentLocations => Set<DepartmentLocation>();
}
