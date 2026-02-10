namespace XTI_HubDB.Entities.Configuration;

internal sealed class AppCommandStepEntityConfiguration : IEntityTypeConfiguration<AppCommandStepEntity>
{
    public void Configure(EntityTypeBuilder<AppCommandStepEntity> builder)
    {
        builder.HasKey(s => s.ID);
        builder.HasIndex(s => new { s.CommandID, s.TimeStarted });
        builder.Property(s => s.Activity).HasMaxLength(1000).HasDefaultValue("");
        builder.Property(s => s.TimeStarted).HasDefaultValue(DateTimeOffset.MaxValue);
        builder.Property(s => s.TimeEnded).HasDefaultValue(DateTimeOffset.MaxValue);
        builder.Property(s => s.ErrorMessage).HasMaxLength(5000).HasDefaultValue("");
        builder
            .HasOne<AppCommandEntity>()
            .WithMany()
            .HasForeignKey(s => s.CommandID)
            .OnDelete(DeleteBehavior.Restrict);
        builder.ToTable("AppCommandSteps");
    }
}
