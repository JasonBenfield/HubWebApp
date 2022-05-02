using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF;

internal class AuthenticatorEntityConfiguration : IEntityTypeConfiguration<AuthenticatorEntity>
{
    public void Configure(EntityTypeBuilder<AuthenticatorEntity> builder)
    {
        builder.HasKey(a => a.ID);
        builder.Property(a => a.ID).ValueGeneratedOnAdd();
        builder.HasIndex(a => a.AppID).IsUnique();
        builder
            .HasOne<AppEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(r => r.AppID);
        builder.ToTable("Authenticators");
    }
}
