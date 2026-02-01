namespace XTI_HubDB.Entities.Configuration;

public sealed class AppXtiVersionEntityConfiguration : IEntityTypeConfiguration<AppXtiVersionEntity>
{
    public void Configure(EntityTypeBuilder<AppXtiVersionEntity> builder)
    {
        builder.HasKey(av => av.ID);
        builder.Property(av => av.ID).ValueGeneratedOnAdd();
        builder.HasIndex(av => new { av.AppID, av.VersionID }).IsUnique();
        builder
            .HasOne<AppEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(v => v.AppID);
        builder
            .HasOne<XtiVersionEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(v => v.VersionID);
        builder.ToTable("AppXtiVersions");
    }
}