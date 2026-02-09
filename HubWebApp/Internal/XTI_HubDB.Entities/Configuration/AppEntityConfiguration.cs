namespace XTI_HubDB.Entities.Configuration;

public sealed class AppEntityConfiguration : IEntityTypeConfiguration<AppEntity>
{
    public void Configure(EntityTypeBuilder<AppEntity> builder)
    {
        builder.HasKey(a => a.ID);
        builder.HasIndex(a => new { a.Name, a.Type }).IsUnique();
        builder.Property(a => a.ID).ValueGeneratedOnAdd();
        builder.Property(a => a.Name).HasMaxLength(50).HasDefaultValue("");
        builder.Property(a => a.DisplayText).HasMaxLength(50).HasDefaultValue("");
        builder.Property(a => a.VersionName).HasMaxLength(100).HasDefaultValue("");
        builder.Property(a => a.SerializedDefaultOptions).HasMaxLength(5000).HasDefaultValue("");
        builder.Property(a => a.RepoOwner).HasMaxLength(100).HasDefaultValue("");
        builder.Property(a => a.RepoName).HasMaxLength(100).HasDefaultValue("");
        builder.Property(a => a.Title).HasMaxLength(100).HasDefaultValue("");
        builder.ToTable("Apps");
    }
}