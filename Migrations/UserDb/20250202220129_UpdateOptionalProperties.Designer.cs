﻿// <auto-generated />
using System;
using DTH.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DTH.API.Migrations.UserDb
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20250202220129_UpdateOptionalProperties")]
    partial class UpdateOptionalProperties
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("DTH.API.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DTH.API.Models.UserClaim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim");
                });

            modelBuilder.Entity("DTH.API.Models.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("DTH.API.Models.UserClaim", b =>
                {
                    b.HasOne("DTH.API.Models.User", null)
                        .WithMany("Claims")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DTH.API.Models.UserRole", b =>
                {
                    b.HasOne("DTH.API.Models.User", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DTH.API.Models.User", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
