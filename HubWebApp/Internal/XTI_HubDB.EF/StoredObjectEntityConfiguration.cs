using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF;

internal sealed class StoredObjectEntityConfiguration : IEntityTypeConfiguration<StoredObjectEntity>
{
    public void Configure(EntityTypeBuilder<StoredObjectEntity> builder)
    {
        builder.HasKey(a => a.ID);
        builder.Property(a => a.ID).ValueGeneratedOnAdd();
        builder.Property(a => a.StorageName).HasMaxLength(100);
        builder.Property(a => a.StorageKey).HasMaxLength(100);
        builder.HasIndex(a => new { a.StorageName, a.StorageKey }).IsUnique();
        builder.ToTable("StoredObjects");
    }
}
