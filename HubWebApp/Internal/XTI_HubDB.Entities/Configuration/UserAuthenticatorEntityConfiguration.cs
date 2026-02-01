namespace XTI_HubDB.Entities.Configuration;

internal sealed class UserAuthenticatorEntityConfiguration : IEntityTypeConfiguration<UserAuthenticatorEntity>
{
    public void Configure(EntityTypeBuilder<UserAuthenticatorEntity> builder)
    {
        builder.HasKey(ua => ua.ID);
        builder.Property(ua => ua.ID).ValueGeneratedOnAdd();
        builder.HasIndex(ua => new { ua.UserID, ua.AuthenticatorID }).IsUnique();
        builder.Property(ua => ua.ExternalUserKey).HasMaxLength(100);
        builder.HasIndex(ua => new { ua.ExternalUserKey, ua.AuthenticatorID }).IsUnique();
        builder
            .HasOne<AppUserEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(ua => ua.UserID);
        builder
            .HasOne<AuthenticatorEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(ua => ua.AuthenticatorID);
        builder.ToTable("UserAuthenticators");
    }
}