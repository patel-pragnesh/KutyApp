﻿// <auto-generated />
using System;
using GeoAPI.Geometries;
using KutyApp.Services.Environment.Bll.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KutyApp.Services.Environment.Api.Migrations
{
    [DbContext(typeof(KutyAppServiceDbContext))]
    partial class KutyAppServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KutyApp.Services.Environment.Bll.Entities.Model.Advert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdvertiserId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description");

                    b.Property<IPoint>("Location");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("AdvertiserId");

                    b.ToTable("Adverts");
                });

            modelBuilder.Entity("KutyApp.Services.Environment.Bll.Entities.Model.DbVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("DbVerisons");
                });

            modelBuilder.Entity("KutyApp.Services.Environment.Bll.Entities.Model.Habit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<string>("Description");

                    b.Property<TimeSpan?>("EndTime");

                    b.Property<int>("PetId");

                    b.Property<TimeSpan?>("StartTime");

                    b.Property<string>("Title");

                    b.Property<string>("Unit");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.ToTable("Habits");
                });

            modelBuilder.Entity("KutyApp.Services.Environment.Bll.Entities.Model.MedicalTreatment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Currency");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("PetId");

                    b.Property<string>("Place");

                    b.Property<double>("Price");

                    b.Property<string>("Tender");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.ToTable("MedicalTreatments");
                });

            modelBuilder.Entity("KutyApp.Services.Environment.Bll.Entities.Model.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("ChipNumber");

                    b.Property<string>("Color");

                    b.Property<int>("Gender");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Kind");

                    b.Property<string>("Name");

                    b.Property<string>("OwnerId");

                    b.Property<double?>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("KutyApp.Services.Environment.Bll.Entities.Model.PetSitting", b =>
                {
                    b.Property<string>("SitterId");

                    b.Property<int>("PetId");

                    b.HasKey("SitterId", "PetId");

                    b.HasIndex("PetId");

                    b.ToTable("PetSittings");
                });

            modelBuilder.Entity("KutyApp.Services.Environment.Bll.Entities.Model.Poi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<TimeSpan?>("CloseTime");

                    b.Property<int>("EnvironmentTypes");

                    b.Property<string>("ImageUrl");

                    b.Property<IPoint>("Location")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<TimeSpan?>("OpeningTime");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Pois");
                });

            modelBuilder.Entity("KutyApp.Services.Environment.Bll.Entities.Model.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("KutyApp.Services.Environment.Bll.Entities.Model.UserFriendship", b =>
                {
                    b.Property<string>("User1Id");

                    b.Property<string>("User2Id");

                    b.Property<string>("UserId");

                    b.HasKey("User1Id", "User2Id");

                    b.HasIndex("User2Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserFriendships");
                });

            modelBuilder.Entity("KutyApp.Services.Environment.Bll.Entities.Model.UserPoi", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("PoiId");

                    b.HasKey("UserId", "PoiId");

                    b.HasIndex("PoiId");

                    b.ToTable("UserPois");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("KutyApp.Services.Environment.Bll.Entities.Model.Advert", b =>
                {
                    b.HasOne("KutyApp.Services.Environment.Bll.Entities.Model.User", "Advertiser")
                        .WithMany("Adverts")
                        .HasForeignKey("AdvertiserId");
                });

            modelBuilder.Entity("KutyApp.Services.Environment.Bll.Entities.Model.Habit", b =>
                {
                    b.HasOne("KutyApp.Services.Environment.Bll.Entities.Model.Pet", "Pet")
                        .WithMany("Habits")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KutyApp.Services.Environment.Bll.Entities.Model.MedicalTreatment", b =>
                {
                    b.HasOne("KutyApp.Services.Environment.Bll.Entities.Model.Pet", "Pet")
                        .WithMany("MedicalTreatments")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KutyApp.Services.Environment.Bll.Entities.Model.Pet", b =>
                {
                    b.HasOne("KutyApp.Services.Environment.Bll.Entities.Model.User", "Owner")
                        .WithMany("Pets")
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("KutyApp.Services.Environment.Bll.Entities.Model.PetSitting", b =>
                {
                    b.HasOne("KutyApp.Services.Environment.Bll.Entities.Model.Pet", "Pet")
                        .WithMany("PetSittings")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KutyApp.Services.Environment.Bll.Entities.Model.User", "Sitter")
                        .WithMany("PetSittings")
                        .HasForeignKey("SitterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KutyApp.Services.Environment.Bll.Entities.Model.UserFriendship", b =>
                {
                    b.HasOne("KutyApp.Services.Environment.Bll.Entities.Model.User", "User1")
                        .WithMany()
                        .HasForeignKey("User1Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("KutyApp.Services.Environment.Bll.Entities.Model.User", "User2")
                        .WithMany()
                        .HasForeignKey("User2Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("KutyApp.Services.Environment.Bll.Entities.Model.User")
                        .WithMany("Friendships")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("KutyApp.Services.Environment.Bll.Entities.Model.UserPoi", b =>
                {
                    b.HasOne("KutyApp.Services.Environment.Bll.Entities.Model.Poi", "Poi")
                        .WithMany("FavouredByUsers")
                        .HasForeignKey("PoiId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KutyApp.Services.Environment.Bll.Entities.Model.User", "User")
                        .WithMany("FavoritePlaces")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("KutyApp.Services.Environment.Bll.Entities.Model.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("KutyApp.Services.Environment.Bll.Entities.Model.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KutyApp.Services.Environment.Bll.Entities.Model.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("KutyApp.Services.Environment.Bll.Entities.Model.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
