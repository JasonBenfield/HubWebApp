namespace XTI_HubDB.Entities.Configuration;

internal sealed class AppCommandInstallationEntityConfiguration : IEntityTypeConfiguration<AppCommandInstallationEntity>
{
    public void Configure(EntityTypeBuilder<AppCommandInstallationEntity> builder)
    {
        builder.HasKey(ci => new { ci.CommandID, ci.InstallationID });
        builder
            .HasOne<AppCommandEntity>()
            .WithMany()
            .HasForeignKey(ci => ci.CommandID)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne<InstallationEntity>()
            .WithMany()
            .HasForeignKey(ci => ci.InstallationID)
            .OnDelete(DeleteBehavior.Restrict);
        builder.ToTable("AppCommandInstallations");
    }
}
