using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF;

public sealed class InstallLocationEntityConfiguration : IEntityTypeConfiguration<InstallLocationEntity>
{
    public void Configure(EntityTypeBuilder<InstallLocationEntity> builder)
    {
        builder.HasKey(l => l.ID);
        builder.Property(l => l.ID).ValueGeneratedOnAdd();
        builder.Property(l => l.QualifiedMachineName).HasMaxLength(100);
        builder.HasIndex(l => l.QualifiedMachineName).IsUnique();
        builder.ToTable("InstallLocations");
    }
}