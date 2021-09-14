using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF
{
    public sealed class AppEventEntityConfiguration : IEntityTypeConfiguration<AppEventRecord>
    {
        public void Configure(EntityTypeBuilder<AppEventRecord> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.ID).ValueGeneratedOnAdd();
            builder.Property(e => e.EventKey).HasMaxLength(100);
            builder.HasIndex(e => e.EventKey).IsUnique();
            builder.Property(e => e.Caption).HasMaxLength(1000);
            builder.Property(e => e.Message).HasMaxLength(5000);
            builder.Property(e => e.Detail).HasMaxLength(32000);
            builder
                .HasOne<AppRequestRecord>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(e => e.RequestID);
            builder.ToTable("Events");
        }
    }
}
