namespace XTI_HubDB.EF;

internal sealed class InstallConfigurationTemplateEntityConfiguration : IEntityTypeConfiguration<InstallConfigurationTemplateEntity>
{
    public void Configure(EntityTypeBuilder<InstallConfigurationTemplateEntity> builder)
    {
        builder.HasKey(c => c.ID);
        builder.HasIndex(c => c.TemplateName).IsUnique();
        builder.Property(c => c.TemplateName).HasMaxLength(50);
        builder.Property(c => c.DestinationMachineName).HasMaxLength(100);
        builder.Property(c => c.SiteName).HasMaxLength(100);
        builder.Property(c => c.Domain).HasMaxLength(100);
        builder.ToTable("InstallConfigurationTemplates");
    }
}
