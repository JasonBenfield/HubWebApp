namespace XTI_HubDB.Entities.Configuration;

internal sealed class ExpandedUserEntityConfiguration : IEntityTypeConfiguration<ExpandedUser>
{
    public void Configure(EntityTypeBuilder<ExpandedUser> builder)
    {
        builder.HasKey(u => u.UserID);
        builder.ToView("ExpandedUsers");
    }
}
