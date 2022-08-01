namespace XTI_HubDB.EF;

internal sealed class AppSessionEntityConfiguration : IEntityTypeConfiguration<AppSessionEntity>
{
    public void Configure(EntityTypeBuilder<AppSessionEntity> builder)
    {
        builder.HasKey(s => s.ID);
        builder.Property(s => s.ID).ValueGeneratedOnAdd();
        builder.Property(s => s.SessionKey).HasMaxLength(100);
        builder.HasIndex(s => s.SessionKey).IsUnique();
        builder.Property(s => s.RequesterKey).HasMaxLength(100);
        builder.Property(s => s.RemoteAddress).HasMaxLength(20);
        builder.Property(s => s.UserAgent).HasMaxLength(1000);
        builder
            .HasOne<AppUserEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(s => s.UserID);
        builder.ToTable("Sessions");
    }
}