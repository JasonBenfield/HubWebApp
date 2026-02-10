namespace XTI_HubDB.Entities.Configuration;

internal sealed class AppCommandEntityConfiguration : IEntityTypeConfiguration<AppCommandEntity>
{
    public void Configure(EntityTypeBuilder<AppCommandEntity> builder)
    {
        builder.HasKey(c => c.ID);
        builder.HasIndex(c => new { c.LocationID, c.CommandName, c.Status });
        builder.Property(c => c.CommandName).HasMaxLength(100).HasDefaultValue("");
        builder.Property(c => c.Status).HasDefaultValue(0);
        builder.Property(c => c.SerializedRequest).HasMaxLength(1000).HasDefaultValue("");
        builder.Property(c => c.TimeAdded).HasDefaultValueSql("getdate()");
        builder.Property(c => c.TimeStarted).HasDefaultValue(DateTimeOffset.MaxValue);
        builder.Property(c => c.TimeEnded).HasDefaultValue(DateTimeOffset.MaxValue);
        builder
            .HasOne<AppEntity>()
            .WithMany()
            .HasForeignKey(c => c.AppID)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne<InstallLocationEntity>()
            .WithMany()
            .HasForeignKey(c => c.LocationID)
            .OnDelete(DeleteBehavior.Restrict);
        builder.ToTable("AppCommands");
    }
}
