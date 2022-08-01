namespace XTI_HubDB.EF;

internal sealed class ExpandedLogEntryEntityConfiguration : IEntityTypeConfiguration<ExpandedLogEntry>
{
    public void Configure(EntityTypeBuilder<ExpandedLogEntry> builder)
    {
        builder.HasKey(b => b.EventID);
        builder.ToView("ExpandedLogEntries");
    }
}
