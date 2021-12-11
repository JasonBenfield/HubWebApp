using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF
{
    public sealed class ModifierCategoryEntityConfiguration : IEntityTypeConfiguration<ModifierCategoryEntity>
    {
        public void Configure(EntityTypeBuilder<ModifierCategoryEntity> builder)
        {
            builder.HasKey(c => c.ID);
            builder.Property(c => c.ID).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).HasMaxLength(50);
            builder.HasIndex(c => new { c.AppID, c.Name }).IsUnique();
            builder
                .HasOne<AppEntity>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(c => c.AppID);
            builder.ToTable("ModifierCategories");
        }
    }
}
