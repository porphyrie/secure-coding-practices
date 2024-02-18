﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineBankingApp.Data;

#nullable disable

namespace OnlineBankingApp.Migrations
{
    [DbContext(typeof(OnlineBankingAppContext))]
    [Migration("20240131182420_AddBackups")]
    partial class AddBackups
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("OnlineBankingApp.Models.Account", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountType")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("CustomerID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("OnlineBankingApp.Models.Backup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BackupDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Backup");
                });

            modelBuilder.Entity("OnlineBankingApp.Models.Customer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("OnlineBankingApp.Models.FundTransfer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountFrom")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountTo")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("CustomerID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("FundTransfer");
                });

            modelBuilder.Entity("OnlineBankingApp.Models.Account", b =>
                {
                    b.HasOne("OnlineBankingApp.Models.Customer", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("OnlineBankingApp.Models.Customer", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}