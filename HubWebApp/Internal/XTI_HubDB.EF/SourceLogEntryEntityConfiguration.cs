namespace XTI_HubDB.EF;

internal sealed class SourceLogEntryEntityConfiguration : IEntityTypeConfiguration<SourceLogEntryEntity>
{
    public void Configure(EntityTypeBuilder<SourceLogEntryEntity> builder)
    {
        builder.HasKey(e => e.ID);
        builder.Property(e => e.ID).ValueGeneratedOnAdd();
        builder.HasIndex(r => new { r.SourceID, r.TargetID }).IsUnique();
        builder
            .HasOne<LogEntryEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(e => e.SourceID);
        builder
            .HasOne<LogEntryEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(e => e.TargetID);
        builder.ToTable("SourceLogEntries");
    }
}
