namespace XTI_HubDB.EF;

internal sealed class ExpandedRequestEntityConfiguration : IEntityTypeConfiguration<ExpandedRequest>
{
    public void Configure(EntityTypeBuilder<ExpandedRequest> builder)
    {
        builder.HasKey(b => b.RequestID);
        builder.ToView("ExpandedRequests");
    }
}
