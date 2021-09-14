using XTI_HubDB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace XTI_HubDB.EF
{
    public sealed class AppRoleEntityConfiguration : IEntityTypeConfiguration<AppRoleRecord>
    {
        public void Configure(EntityTypeBuilder<AppRoleRecord> builder)
        {
            builder.HasKey(r => r.ID);
            builder.Property(r => r.ID).ValueGeneratedOnAdd();
            builder.Property(r => r.Name).HasMaxLength(100);
            builder.Property(r => r.TimeDeactivated).HasDefaultValue(DateTimeOffset.MaxValue);
            builder
                .HasIndex(r => new { r.AppID, r.Name })
                .IsUnique();
            builder
                .HasOne<AppRecord>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(r => r.AppID);
            builder.ToTable("Roles");
        }
    }
}
