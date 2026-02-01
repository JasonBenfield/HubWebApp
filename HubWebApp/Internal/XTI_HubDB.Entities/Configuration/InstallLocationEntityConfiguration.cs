namespace XTI_HubDB.Entities.Configuration;

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