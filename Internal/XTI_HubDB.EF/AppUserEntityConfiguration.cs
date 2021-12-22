using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF;

public sealed class AppUserEntityConfiguration : IEntityTypeConfiguration<AppUserEntity>
{
    public void Configure(EntityTypeBuilder<AppUserEntity> builder)
    {
        builder.HasKey(u => u.ID);
        builder
            .Property(u => u.ID)
            .ValueGeneratedOnAdd();
        builder
            .HasIndex(u => u.UserName)
            .IsUnique();
        builder
            .Property(u => u.UserName)
            .HasMaxLength(100);
        builder
            .Property(u => u.Password)
            .HasMaxLength(100);
        builder
            .Property(u => u.Name)
            .HasMaxLength(100);
        builder
            .Property(u => u.Email)
            .HasMaxLength(100);
        builder.ToTable("Users");
    }
}