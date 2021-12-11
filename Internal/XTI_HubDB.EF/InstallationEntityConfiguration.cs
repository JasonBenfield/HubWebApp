using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF
{
    public sealed class InstallationEntityConfiguration : IEntityTypeConfiguration<InstallationEntity>
    {
        public void Configure(EntityTypeBuilder<InstallationEntity> builder)
        {
            builder.HasKey(inst => inst.ID);
            builder.Property(inst => inst.ID).ValueGeneratedOnAdd();
            builder.HasIndex(inst => new { inst.LocationID, inst.VersionID, inst.IsCurrent }).IsUnique();
            builder
                .HasOne<InstallLocationEntity>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(inst => inst.LocationID);
            builder
                .HasOne<AppVersionEntity>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(inst => inst.VersionID);
            builder.ToTable("Installations");
        }
    }
}
