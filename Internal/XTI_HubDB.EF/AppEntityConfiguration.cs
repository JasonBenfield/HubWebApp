using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF;

public sealed class AppEntityConfiguration : IEntityTypeConfiguration<AppEntity>
{
    public void Configure(EntityTypeBuilder<AppEntity> builder)
    {
        builder.HasKey(a => a.ID);
        builder.Property(a => a.ID).ValueGeneratedOnAdd();
        builder.Property(a => a.Name).HasMaxLength(50);
        builder.HasIndex(a => a.Name).IsUnique();
        builder.Property(a => a.Title)
            .HasMaxLength(100)
            .HasDefaultValue("");
        builder.ToTable("Apps");
    }
}