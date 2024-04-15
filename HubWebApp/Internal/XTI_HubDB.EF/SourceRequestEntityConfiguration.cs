namespace XTI_HubDB.EF;

internal sealed class SourceRequestEntityConfiguration : IEntityTypeConfiguration<SourceRequestEntity>
{
    public void Configure(EntityTypeBuilder<SourceRequestEntity> builder)
    {
        builder.HasKey(e => e.ID);
        builder.Property(e => e.ID).ValueGeneratedOnAdd();
        builder.HasIndex(r => new { r.SourceID, r.TargetID }).IsUnique();
        builder
            .HasOne<AppRequestEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(e => e.SourceID);
        builder
            .HasOne<AppRequestEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(e => e.TargetID);
        builder.ToTable("SourceRequests");
    }
}
