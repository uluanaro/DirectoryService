// Infrastructure/Persistence/AppDbContext.cs

using Microsoft.EntityFrameworkCore;

namespace DirectoryService.Infrastructure;

public class AppDbContext : DbContext 
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)                        
    {
    }

}