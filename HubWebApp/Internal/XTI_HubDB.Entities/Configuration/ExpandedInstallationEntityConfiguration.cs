namespace XTI_HubDB.Entities.Configuration;

internal sealed class ExpandedInstallationEntityConfiguration : IEntityTypeConfiguration<ExpandedInstallation>
{
    public void Configure(EntityTypeBuilder<ExpandedInstallation> builder)
    {
        builder.HasKey(b => b.InstallationID);
        builder.ToView("ExpandedInstallations");
    }
}
