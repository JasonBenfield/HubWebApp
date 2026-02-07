namespace XTI_HubDB.Entities.Configuration;

internal sealed class AppCommandEntityConfiguration : IEntityTypeConfiguration<AppCommandEntity>
{
    public void Configure(EntityTypeBuilder<AppCommandEntity> builder)
    {
        builder.HasKey(ri => ri.ID);
        builder.Property(c => c.CommandName).HasMaxLength(100).HasDefaultValue("");
        builder.Property(c => c.SerializedRequest).HasMaxLength(1000).HasDefaultValue("");
        builder.Property(ri => ri.TimeAdded).HasDefaultValue(DateTimeOffset.MaxValue);
        builder.Property(ri => ri.TimeStarted).HasDefaultValue(DateTimeOffset.MaxValue);
        builder.Property(ri => ri.TimeEnded).HasDefaultValue(DateTimeOffset.MaxValue);
        builder
            .HasOne<AppEntity>()
            .WithMany()
            .HasForeignKey(ri => ri.AppID)
            .OnDelete(DeleteBehavior.Restrict);
        builder.ToTable("AppCommands");
    }
}
