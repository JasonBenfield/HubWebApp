namespace XTI_HubDB.EF;

public sealed class AppEntityConfiguration : IEntityTypeConfiguration<AppEntity>
{
    public void Configure(EntityTypeBuilder<AppEntity> builder)
    {
        builder.HasKey(a => a.ID);
        builder.Property(a => a.ID).ValueGeneratedOnAdd();
        builder.Property(a => a.Name).HasMaxLength(50);
        builder.Property(a => a.DisplayText).HasMaxLength(50);
        builder.Property(a => a.VersionName).HasMaxLength(100);
        builder.Property(a => a.SerializedDefaultOptions).HasMaxLength(5000);
        builder.HasIndex(a => new { a.Name, a.Type }).IsUnique();
        builder.Property(a => a.Title)
            .HasMaxLength(100)
            .HasDefaultValue("");
        builder.ToTable("Apps");
    }
}