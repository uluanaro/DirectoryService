using Directory.Domain.Entities;
using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id");


        builder.OwnsOne(d => d.Name, nb =>
        {
            nb.Property(n => n.Prefix)
                .IsRequired()
                .HasMaxLength(LengthConstants.Length50)
                .HasColumnName("name_prefix");

            nb.Property(n => n.Name)
                .IsRequired()
                .HasMaxLength(LengthConstants.Length500)
                .HasColumnName("name");
        });


        builder.Property(d => d.Slug)
            .HasConversion(
                slug => slug.Value,
                value => Slug.Create(value)
            )
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("slug");

        builder.Property(d => d.ParentId)
            .IsRequired(false)
            .HasColumnName("parent_id");


        builder.HasMany(d => d.Locations)
            .WithOne()
            .HasForeignKey(dl => dl.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Positions)
            .WithOne()
            .HasForeignKey(dp => dp.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}