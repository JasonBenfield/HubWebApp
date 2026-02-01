namespace XTI_HubDB.Entities.Configuration;

internal sealed class ExpandedRequestEntityConfiguration : IEntityTypeConfiguration<ExpandedRequest>
{
    public void Configure(EntityTypeBuilder<ExpandedRequest> builder)
    {
        builder.HasKey(b => b.RequestID);
        builder.ToView("ExpandedRequests");
    }
}
