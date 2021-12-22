using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF;

public sealed class ResourceRoleEntityConfiguration : IEntityTypeConfiguration<ResourceRoleEntity>
{
    public void Configure(EntityTypeBuilder<ResourceRoleEntity> builder)
    {
        builder.HasKey(r => r.ID);
        builder.Property(r => r.ID).ValueGeneratedOnAdd();
        builder
            .HasIndex(r => new { r.ResourceID, r.RoleID })
            .IsUnique();
        builder
            .HasOne<ResourceEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(r => r.ResourceID);
        builder
            .HasOne<AppRoleEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(r => r.RoleID);
        builder.ToTable("ResourceRoles");
    }
}