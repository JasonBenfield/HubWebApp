using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF;

public sealed class XtiVersionEntityConfiguration : IEntityTypeConfiguration<XtiVersionEntity>
{
    public void Configure(EntityTypeBuilder<XtiVersionEntity> builder)
    {
        builder.HasKey(v => v.ID);
        builder.Property(v => v.ID).ValueGeneratedOnAdd();
        builder.Property(v => v.GroupName).HasMaxLength(100);
        builder.Property(v => v.VersionKey).HasMaxLength(50);
        builder.HasIndex(v => new { v.GroupName, v.VersionKey }).IsUnique();
        builder.ToTable("XtiVersions");
    }
}
