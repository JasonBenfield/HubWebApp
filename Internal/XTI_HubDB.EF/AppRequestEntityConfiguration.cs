using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF
{
    public sealed class AppRequestEntityConfiguration : IEntityTypeConfiguration<AppRequestEntity>
    {
        public void Configure(EntityTypeBuilder<AppRequestEntity> builder)
        {
            builder.HasKey(r => r.ID);
            builder.Property(r => r.ID).ValueGeneratedOnAdd();
            builder.Property(r => r.Path).HasMaxLength(100);
            builder.Property(r => r.RequestKey).HasMaxLength(100);
            builder.HasIndex(r => r.RequestKey).IsUnique();
            builder
                .HasOne<AppSessionEntity>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(r => r.SessionID);
            builder
                .HasOne<ResourceEntity>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(r => r.ResourceID);
            builder
                .HasOne<ModifierEntity>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(r => r.ModifierID);
            builder.ToTable("Requests");
        }
    }
}
