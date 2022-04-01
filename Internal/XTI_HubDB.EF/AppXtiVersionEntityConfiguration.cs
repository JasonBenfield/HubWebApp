using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF;

public sealed class AppXtiVersionEntityConfiguration : IEntityTypeConfiguration<AppXtiVersionEntity>
{
    public void Configure(EntityTypeBuilder<AppXtiVersionEntity> builder)
    {
        builder.HasKey(av => av.ID);
        builder.Property(av => av.ID).ValueGeneratedOnAdd();
        builder.Property(av => av.Domain).HasMaxLength(100);
        builder.HasIndex(av => new { av.AppID, av.VersionID }).IsUnique();
        builder
            .HasOne<AppEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(v => v.AppID);
        builder
            .HasOne<XtiVersionEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(v => v.VersionID);
        builder.ToTable("AppXtiVersions");
    }
}