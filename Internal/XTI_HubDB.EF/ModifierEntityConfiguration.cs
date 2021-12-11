using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF
{
    public sealed class ModifierEntityConfiguration : IEntityTypeConfiguration<ModifierEntity>
    {
        public void Configure(EntityTypeBuilder<ModifierEntity> builder)
        {
            builder.HasKey(m => m.ID);
            builder.Property(m => m.ID).ValueGeneratedOnAdd();
            builder.Property(m => m.ModKey).HasMaxLength(100);
            builder.HasIndex(m => new { m.CategoryID, m.ModKey }).IsUnique();
            builder.Property(m => m.TargetKey).HasMaxLength(100);
            builder.HasIndex(m => new { m.CategoryID, m.TargetKey }).IsUnique();
            builder
                .HasOne<ModifierCategoryEntity>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(m => m.CategoryID);
            builder.ToTable("Modifiers");
        }
    }
}
