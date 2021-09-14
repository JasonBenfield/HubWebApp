﻿// <auto-generated />
using System;
using XTI_HubDB.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace XTI_HubDB.EF.SqlServer
{
    [DbContext(typeof(HubDbContext))]
    [Migration("20210126224236_ResourceGroupChildOfVersion")]
    partial class ResourceGroupChildOfVersion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("XTI_HubDB.Entities.AppEventRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Caption")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Detail")
                        .HasMaxLength(32000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventKey")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Message")
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequestID")
                        .HasColumnType("int");

                    b.Property<int>("Severity")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("TimeOccurred")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("ID");

                    b.HasIndex("EventKey")
                        .IsUnique()
                        .HasFilter("[EventKey] IS NOT NULL");

                    b.HasIndex("RequestID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("XTI_HubDB.Entities.AppRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTimeOffset>("TimeAdded")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Title")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValue("");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Apps");
                });

            modelBuilder.Entity("XTI_HubDB.Entities.AppRequestRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ModifierID")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RequestKey")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ResourceID")
                        .HasColumnType("int");

                    b.Property<int>("SessionID")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("TimeEnded")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("TimeStarted")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("ID");

                    b.HasIndex("ModifierID");

                    b.HasIndex("RequestKey")
                        .IsUnique()
                        .HasFilter("[RequestKey] IS NOT NULL");

                    b.HasIndex("ResourceID");

                    b.HasIndex("SessionID");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("XTI_HubDB.Entities.AppRoleRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AppID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.HasIndex("AppID", "Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("XTI_HubDB.Entities.AppSessionRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("RemoteAddress")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("RequesterKey")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SessionKey")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("TimeEnded")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("TimeStarted")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UserAgent")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SessionKey")
                        .IsUnique()
                        .HasFilter("[SessionKey] IS NOT NULL");

                    b.HasIndex("UserID");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("XTI_HubDB.Entities.AppUserModifierRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ModifierID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ModifierID");

                    b.HasIndex("UserID");

                    b.ToTable("UserModifiers");
                });

            modelBuilder.Entity("XTI_HubDB.Entities.AppUserRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValue("");

                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValue("");

                    b.Property<string>("Password")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("TimeAdded")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UserName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasFilter("[UserName] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("XTI_HubDB.Entities.AppUserRoleRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.HasIndex("UserID");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("XTI_HubDB.Entities.AppVersionRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AppID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Major")
                        .HasColumnType("int");

                    b.Property<int>("Minor")
                        .HasColumnType("int");

                    b.Property<int>("Patch")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("TimeAdded")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("VersionKey")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.HasIndex("AppID");

                    b.HasIndex("VersionKey")
                        .IsUnique()
                        .HasFilter("[VersionKey] IS NOT NULL");

                    b.ToTable("Versions");
                });

            modelBuilder.Entity("XTI_HubDB.Entities.ModifierCategoryAdminRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ModCategoryID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.HasIndex("ModCategoryID", "UserID")
                        .IsUnique();

                    b.ToTable("ModifierCategoryAdmins");
                });

            modelBuilder.Entity("XTI_HubDB.Entities.ModifierCategoryRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AppID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.HasIndex("AppID", "Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("ModifierCategories");
                });

            modelBuilder.Entity("XTI_HubDB.Entities.ModifierRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("DisplayText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModKey")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TargetKey")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.HasIndex("ModKey")
                        .IsUnique()
                        .HasFilter("[ModKey] IS NOT NULL");

                    b.HasIndex("CategoryID", "TargetKey")
                        .IsUnique()
                        .HasFilter("[TargetKey] IS NOT NULL");

                    b.ToTable("Modifiers");
                });

            modelBuilder.Entity("XTI_HubDB.Entities.ResourceGroupRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("IsAnonymousAllowed")
                        .HasColumnType("bit");

                    b.Property<int>("ModCategoryID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("VersionID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ModCategoryID");

                    b.HasIndex("VersionID", "Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("ResourceGroups");
                });

            modelBuilder.Entity("XTI_HubDB.Entities.ResourceGroupRoleRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("GroupID")
                        .HasColumnType("int");

                    b.Property<bool>("IsAllowed")
                        .HasColumnType("bit");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.HasIndex("GroupID", "RoleID")
                        .IsUnique();

                    b.ToTable("ResourceGroupRoles");
                });

            modelBuilder.Entity("XTI_HubDB.Entities.ResourceRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("GroupID")
                        .HasColumnType("int");

                    b.Property<bool>("IsAnonymousAllowed")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ResultType")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("GroupID", "Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("XTI_HubDB.Entities.ResourceRoleRecord", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("IsAllowed")
                        .HasColumnType("bit");

                    b.Property<int>("ResourceID")
                        .HasColumnType("int");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.HasIndex("ResourceID", "RoleID")
                        .IsUnique();

                    b.ToTable("ResourceRoles");
                });

            modelBuilder.Entity("XTI_HubDB.Entities.AppEventRecord", b =>
                {
                    b.HasOne("XTI_HubDB.Entities.AppRequestRecord", null)
                        .WithMany()
                        .HasForeignKey("RequestID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("XTI_HubDB.Entities.AppRequestRecord", b =>
                {
                    b.HasOne("XTI_HubDB.Entities.ModifierRecord", null)
                        .WithMany()
                        .HasForeignKey("ModifierID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("XTI_HubDB.Entities.ResourceRecord", null)
                        .WithMany()
                        .HasForeignKey("ResourceID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("XTI_HubDB.Entities.AppSessionRecord", null)
                        .WithMany()
                        .HasForeignKey("SessionID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("XTI_HubDB.Entities.AppRoleRecord", b =>
                {
                    b.HasOne("XTI_HubDB.Entities.AppRecord", null)
                        .WithMany()
                        .HasForeignKey("AppID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("XTI_HubDB.Entities.AppSessionRecord", b =>
                {
                    b.HasOne("XTI_HubDB.Entities.AppUserRecord", null)
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("XTI_HubDB.Entities.AppUserModifierRecord", b =>
                {
                    b.HasOne("XTI_HubDB.Entities.ModifierRecord", null)
                        .WithMany()
                        .HasForeignKey("ModifierID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("XTI_HubDB.Entities.AppUserRecord", null)
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("XTI_HubDB.Entities.AppUserRoleRecord", b =>
                {
                    b.HasOne("XTI_HubDB.Entities.AppRoleRecord", null)
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("XTI_HubDB.Entities.AppUserRecord", null)
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("XTI_HubDB.Entities.AppVersionRecord", b =>
                {
                    b.HasOne("XTI_HubDB.Entities.AppRecord", null)
                        .WithMany()
                        .HasForeignKey("AppID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("XTI_HubDB.Entities.ModifierCategoryAdminRecord", b =>
                {
                    b.HasOne("XTI_HubDB.Entities.ModifierCategoryRecord", null)
                        .WithMany()
                        .HasForeignKey("ModCategoryID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("XTI_HubDB.Entities.AppUserRecord", null)
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("XTI_HubDB.Entities.ModifierCategoryRecord", b =>
                {
                    b.HasOne("XTI_HubDB.Entities.AppRecord", null)
                        .WithMany()
                        .HasForeignKey("AppID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("XTI_HubDB.Entities.ModifierRecord", b =>
                {
                    b.HasOne("XTI_HubDB.Entities.ModifierCategoryRecord", null)
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("XTI_HubDB.Entities.ResourceGroupRecord", b =>
                {
                    b.HasOne("XTI_HubDB.Entities.ModifierCategoryRecord", null)
                        .WithMany()
                        .HasForeignKey("ModCategoryID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("XTI_HubDB.Entities.AppVersionRecord", null)
                        .WithMany()
                        .HasForeignKey("VersionID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("XTI_HubDB.Entities.ResourceGroupRoleRecord", b =>
                {
                    b.HasOne("XTI_HubDB.Entities.ResourceGroupRecord", null)
                        .WithMany()
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("XTI_HubDB.Entities.AppRoleRecord", null)
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("XTI_HubDB.Entities.ResourceRecord", b =>
                {
                    b.HasOne("XTI_HubDB.Entities.ResourceGroupRecord", null)
                        .WithMany()
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("XTI_HubDB.Entities.ResourceRoleRecord", b =>
                {
                    b.HasOne("XTI_HubDB.Entities.ResourceRecord", null)
                        .WithMany()
                        .HasForeignKey("ResourceID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("XTI_HubDB.Entities.AppRoleRecord", null)
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
