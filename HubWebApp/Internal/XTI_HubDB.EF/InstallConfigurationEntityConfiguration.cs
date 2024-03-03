namespace XTI_HubDB.EF;

internal sealed class InstallConfigurationEntityConfiguration : IEntityTypeConfiguration<InstallConfigurationEntity>
{
    public void Configure(EntityTypeBuilder<InstallConfigurationEntity> builder)
    {
        builder.HasKey(c => c.ID);
        builder.HasIndex(c => new { c.RepoOwner, c.RepoName, c.ConfigurationName, c.AppName, c.AppType }).IsUnique();
        builder.Property(c => c.RepoOwner).HasMaxLength(100);
        builder.Property(c => c.RepoName).HasMaxLength(100);
        builder.Property(c => c.AppName).HasMaxLength(50);
        builder.Property(c => c.ConfigurationName).HasMaxLength(50);
        builder.HasOne<InstallConfigurationTemplateEntity>()
            .WithMany()
            .HasForeignKey(c => c.TemplateID)
            .OnDelete(DeleteBehavior.Restrict);
        builder.ToTable("InstallConfigurations");
    }
}
