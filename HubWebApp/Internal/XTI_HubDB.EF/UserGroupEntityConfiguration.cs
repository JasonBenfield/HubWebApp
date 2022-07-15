using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF;

internal sealed class UserGroupEntityConfiguration : IEntityTypeConfiguration<UserGroupEntity>
{
    public void Configure(EntityTypeBuilder<UserGroupEntity> builder)
    {
        builder.HasKey(ug => ug.ID);
        builder.Property(ug => ug.ID).ValueGeneratedOnAdd();
        builder.Property(ug => ug.GroupName).HasMaxLength(50);
        builder.Property(ug => ug.DisplayText).HasMaxLength(100);
        builder.Property(ug => ug.PublicKey).HasMaxLength(100);
        builder.HasIndex(ug => new { ug.GroupName }).IsUnique();
        builder.HasIndex(ug => new { ug.PublicKey }).IsUnique();
        builder.ToTable("UserGroups");
    }
}
