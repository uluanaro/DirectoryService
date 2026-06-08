using Directory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class DepartmentPositionConfiguration : IEntityTypeConfiguration<DepartmentPosition>
{
    public void Configure(EntityTypeBuilder<DepartmentPosition> builder)
    {
        builder.ToTable("department_positions");

        builder.HasKey(dp => new { dp.DepartmentId, dp.PositionId });

        builder.Property(dp => dp.DepartmentId).HasColumnName("department_id");
        builder.Property(dp => dp.PositionId).HasColumnName("position_id");

        builder.Property(dp => dp.AssignedAt)
            .IsRequired()
            .HasColumnName("assigned_at");
        
        
            builder.HasOne<Position>()
                .WithMany()
                .HasForeignKey(dp => dp.PositionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Department>()
                .WithMany()
                .HasForeignKey(dp => dp.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        
    }
}
