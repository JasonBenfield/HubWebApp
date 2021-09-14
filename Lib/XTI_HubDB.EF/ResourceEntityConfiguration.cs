﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF
{
    public sealed class ResourceEntityConfiguration : IEntityTypeConfiguration<ResourceRecord>
    {
        public void Configure(EntityTypeBuilder<ResourceRecord> builder)
        {
            builder.HasKey(r => r.ID);
            builder.Property(r => r.ID).ValueGeneratedOnAdd();
            builder.Property(r => r.Name).HasMaxLength(100);
            builder
                .HasIndex(r => new { r.GroupID, r.Name })
                .IsUnique();
            builder
                .HasOne<ResourceGroupRecord>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(r => r.GroupID);
            builder.ToTable("Resources");
        }
    }
}