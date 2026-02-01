namespace XTI_HubDB.Entities.Configuration;

internal sealed class ExpandedUserRoleEntityConfiguration : IEntityTypeConfiguration<ExpandedUserRole>
{
    public void Configure(EntityTypeBuilder<ExpandedUserRole> builder)
    {
        builder.HasKey(b => b.UserRoleID);
        builder.ToView("ExpandedUserRoles");
    }
}
