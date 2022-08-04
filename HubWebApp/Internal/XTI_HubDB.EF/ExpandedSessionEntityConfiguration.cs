namespace XTI_HubDB.EF;

internal sealed class ExpandedSessionEntityConfiguration : IEntityTypeConfiguration<ExpandedSession>
{
    public void Configure(EntityTypeBuilder<ExpandedSession> builder)
    {
        builder.HasKey(b => b.SessionID);
        builder.ToView("ExpandedSessions");
    }
}
