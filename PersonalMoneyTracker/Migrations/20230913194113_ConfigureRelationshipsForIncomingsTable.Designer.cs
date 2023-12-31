﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonalMoneyTracker.Persistence;

#nullable disable

namespace PersonalMoneyTracker.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230913194113_ConfigureRelationshipsForIncomingsTable")]
    partial class ConfigureRelationshipsForIncomingsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PersonalMoneyTracker.Models.IncomeCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("IncomeCategories");
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.Incoming", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IncomeCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IncomeCategoryId");

                    b.HasIndex("UserId");

                    b.HasIndex("WalletId");

                    b.ToTable("Incomings");
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PaymentCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaymentCategoryId");

                    b.HasIndex("UserId");

                    b.HasIndex("WalletId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.PaymentCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PaymentCategories");
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.IncomeCategory", b =>
                {
                    b.HasOne("PersonalMoneyTracker.Models.User", "User")
                        .WithMany("IncomeCategories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.Incoming", b =>
                {
                    b.HasOne("PersonalMoneyTracker.Models.IncomeCategory", "IncomeCategory")
                        .WithMany("Incomings")
                        .HasForeignKey("IncomeCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalMoneyTracker.Models.User", "User")
                        .WithMany("Incomings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalMoneyTracker.Models.Wallet", "Wallet")
                        .WithMany("Incomings")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IncomeCategory");

                    b.Navigation("User");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.Payment", b =>
                {
                    b.HasOne("PersonalMoneyTracker.Models.PaymentCategory", "PaymentCategory")
                        .WithMany("Payments")
                        .HasForeignKey("PaymentCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalMoneyTracker.Models.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalMoneyTracker.Models.Wallet", "Wallet")
                        .WithMany("Payments")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentCategory");

                    b.Navigation("User");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.PaymentCategory", b =>
                {
                    b.HasOne("PersonalMoneyTracker.Models.User", "User")
                        .WithMany("PaymentCategories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.Wallet", b =>
                {
                    b.HasOne("PersonalMoneyTracker.Models.User", "User")
                        .WithMany("Wallets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.IncomeCategory", b =>
                {
                    b.Navigation("Incomings");
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.PaymentCategory", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.User", b =>
                {
                    b.Navigation("IncomeCategories");

                    b.Navigation("Incomings");

                    b.Navigation("PaymentCategories");

                    b.Navigation("Payments");

                    b.Navigation("Wallets");
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.Wallet", b =>
                {
                    b.Navigation("Incomings");

                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
