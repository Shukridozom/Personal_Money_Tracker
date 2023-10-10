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
    [Migration("20230914173638_SeedingTransactionTypesTable")]
    partial class SeedingTransactionTypesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PersonalMoneyTracker.Models.Transaction", b =>
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

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaymentCategoryId");

                    b.HasIndex("TransactionTypeId");

                    b.HasIndex("UserId");

                    b.HasIndex("WalletId");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.TransactionCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("TransactionCategories", (string)null);
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.TransactionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.ToTable("TransactionTypes");
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

            modelBuilder.Entity("PersonalMoneyTracker.Models.Transaction", b =>
                {
                    b.HasOne("PersonalMoneyTracker.Models.TransactionCategory", "PaymentCategory")
                        .WithMany("Payments")
                        .HasForeignKey("PaymentCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalMoneyTracker.Models.TransactionType", "TransactionType")
                        .WithMany("Transactions")
                        .HasForeignKey("TransactionTypeId")
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

                    b.Navigation("TransactionType");

                    b.Navigation("User");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.TransactionCategory", b =>
                {
                    b.HasOne("PersonalMoneyTracker.Models.TransactionType", "TransactionType")
                        .WithMany("TransactionCategories")
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalMoneyTracker.Models.User", "User")
                        .WithMany("PaymentCategories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransactionType");

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

            modelBuilder.Entity("PersonalMoneyTracker.Models.TransactionCategory", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.TransactionType", b =>
                {
                    b.Navigation("TransactionCategories");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.User", b =>
                {
                    b.Navigation("PaymentCategories");

                    b.Navigation("Payments");

                    b.Navigation("Wallets");
                });

            modelBuilder.Entity("PersonalMoneyTracker.Models.Wallet", b =>
                {
                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
