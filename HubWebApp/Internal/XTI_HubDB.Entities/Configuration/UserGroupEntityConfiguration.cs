namespace XTI_HubDB.Entities.Configuration;

internal sealed class UserGroupEntityConfiguration : IEntityTypeConfiguration<UserGroupEntity>
{
    public void Configure(EntityTypeBuilder<UserGroupEntity> builder)
    {
        builder.HasKey(ug => ug.ID);
        builder.Property(ug => ug.ID).ValueGeneratedOnAdd();
        builder.Property(ug => ug.GroupName).HasMaxLength(100);
        builder.Property(ug => ug.DisplayText).HasMaxLength(100);
        builder.HasIndex(ug => new { ug.GroupName }).IsUnique();
        builder.ToTable("UserGroups");
    }
}
