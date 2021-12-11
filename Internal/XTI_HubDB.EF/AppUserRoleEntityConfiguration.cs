using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF
{
    public sealed class AppUserRoleEntityConfiguration : IEntityTypeConfiguration<AppUserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<AppUserRoleEntity> builder)
        {
            builder.HasKey(ur => ur.ID);
            builder.Property(ur => ur.ID).ValueGeneratedOnAdd();
            builder
                .HasOne<AppUserEntity>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(ur => ur.UserID);
            builder
                .HasOne<ModifierEntity>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(ur => ur.ModifierID);
            builder
                .HasOne<AppRoleEntity>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(ur => ur.RoleID);
            builder.ToTable("UserRoles");
        }
    }
}
