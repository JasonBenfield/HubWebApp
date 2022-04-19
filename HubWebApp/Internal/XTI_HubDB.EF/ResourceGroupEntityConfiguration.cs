using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF;

public sealed class ResourceGroupEntityConfiguration : IEntityTypeConfiguration<ResourceGroupEntity>
{
    public void Configure(EntityTypeBuilder<ResourceGroupEntity> builder)
    {
        builder.HasKey(g => g.ID);
        builder.Property(g => g.ID).ValueGeneratedOnAdd();
        builder.Property(g => g.Name).HasMaxLength(100);
        builder
            .HasIndex(g => new { g.AppVersionID, g.Name })
            .IsUnique();
        builder
            .HasOne<AppXtiVersionEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(g => g.AppVersionID);
        builder
            .HasOne<ModifierCategoryEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(g => g.ModCategoryID);
        builder.ToTable("ResourceGroups");
    }
}