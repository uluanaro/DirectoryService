using Directory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class DepartmentLocationConfiguration : IEntityTypeConfiguration<DepartmentLocation>
{
    public void Configure(EntityTypeBuilder<DepartmentLocation> builder)
    {
        builder.ToTable("department_locations");

        builder.HasKey(dl => new { dl.DepartmentId, dl.LocationId });

        builder.Property(dl => dl.DepartmentId).HasColumnName("department_id");

        builder.Property(dl => dl.LocationId)
            .HasConversion(
                id => id.Value,
                value => new LocationId(value)
            )
            .HasColumnName("location_id");

        builder.Property(dl => dl.IsPrimary)
            .IsRequired()
            .HasColumnName("is_primary");

        builder.Property(dl => dl.AssignedAt)
            .IsRequired()
            .HasColumnName("assigned_at");
    }
}
