namespace XTI_HubDB.EF;

internal class AuthenticatorEntityConfiguration : IEntityTypeConfiguration<AuthenticatorEntity>
{
    public void Configure(EntityTypeBuilder<AuthenticatorEntity> builder)
    {
        builder.HasKey(a => a.ID);
        builder.Property(a => a.ID).ValueGeneratedOnAdd();
        builder.HasIndex(a => a.AuthenticatorKey).IsUnique();
        builder.Property(a => a.AuthenticatorKey).HasMaxLength(100);
        builder.Property(a => a.AuthenticatorName).HasMaxLength(100);
        builder.ToTable("Authenticators");
    }
}
