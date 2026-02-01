namespace XTI_HubDB.Entities.Configuration;

internal sealed class ExpandedSessionEntityConfiguration : IEntityTypeConfiguration<ExpandedSession>
{
    public void Configure(EntityTypeBuilder<ExpandedSession> builder)
    {
        builder.HasKey(b => b.SessionID);
        builder.ToView("ExpandedSessions");
    }
}
