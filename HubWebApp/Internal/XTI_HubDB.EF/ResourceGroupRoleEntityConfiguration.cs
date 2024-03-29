﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF;

public sealed class ResourceGroupRoleEntityConfiguration : IEntityTypeConfiguration<ResourceGroupRoleEntity>
{
    public void Configure(EntityTypeBuilder<ResourceGroupRoleEntity> builder)
    {
        builder.HasKey(g => g.ID);
        builder.Property(g => g.ID).ValueGeneratedOnAdd();
        builder
            .HasIndex(g => new { g.GroupID, g.RoleID })
            .IsUnique();
        builder
            .HasOne<ResourceGroupEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(g => g.GroupID);
        builder
            .HasOne<AppRoleEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(g => g.RoleID);
        builder.ToTable("ResourceGroupRoles");
    }
}