using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF
{
    public sealed class AppUserRoleEntityConfiguration : IEntityTypeConfiguration<AppUserRoleRecord>
    {
        public void Configure(EntityTypeBuilder<AppUserRoleRecord> builder)
        {
            builder.HasKey(ur => ur.ID);
            builder.Property(ur => ur.ID).ValueGeneratedOnAdd();
            builder
                .HasOne<AppUserRecord>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(ur => ur.UserID);
            builder
                .HasOne<ModifierRecord>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(ur => ur.ModifierID);
            builder
                .HasOne<AppRoleRecord>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(ur => ur.RoleID);
            builder.ToTable("UserRoles");
        }
    }
}
