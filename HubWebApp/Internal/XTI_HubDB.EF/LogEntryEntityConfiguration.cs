﻿namespace XTI_HubDB.EF;

public sealed class LogEntryEntityConfiguration : IEntityTypeConfiguration<LogEntryEntity>
{
    public void Configure(EntityTypeBuilder<LogEntryEntity> builder)
    {
        builder.HasKey(e => e.ID);
        builder.Property(e => e.ID).ValueGeneratedOnAdd();
        builder.Property(e => e.EventKey).HasMaxLength(100);
        builder.HasIndex(e => e.EventKey).IsUnique();
        builder.Property(e => e.Caption).HasMaxLength(1000);
        builder.Property(e => e.Message).HasMaxLength(5000);
        builder.Property(e => e.Detail).HasMaxLength(32000);
        builder.Property(e => e.Category).HasMaxLength(500);
        builder
                .HasOne<AppRequestEntity>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(e => e.RequestID);
        builder.ToTable("LogEntries");
    }
}