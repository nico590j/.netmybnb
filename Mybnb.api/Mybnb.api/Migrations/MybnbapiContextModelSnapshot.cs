﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mybnb.api.Data;

#nullable disable

namespace Mybnb.api.Migrations
{
    [DbContext(typeof(MybnbapiContext))]
    partial class MybnbapiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Mybnb.api.Models.BNB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerUserID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeOfEstablishment")
                        .HasColumnType("int");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("OwnerUserID");

                    b.ToTable("BNBs");

                    b.HasData(
                        new
                        {
                            ID = -1,
                            Address = "test",
                            Country = "DK",
                            Description = "test data beskrivelse",
                            OwnerUserID = -1,
                            Title = "test",
                            TypeOfEstablishment = 0,
                            ZipCode = "1234"
                        });
                });

            modelBuilder.Entity("Mybnb.api.Models.BnbImage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("BNBID")
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("ID");

                    b.HasIndex("BNBID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Mybnb.api.Models.PossibleRentingPeriod", b =>
                {
                    b.Property<int>("PossibleRentingPeriodID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PossibleRentingPeriodID"), 1L, 1);

                    b.Property<int>("BNBID")
                        .HasColumnType("int");

                    b.Property<double>("DailyPrice")
                        .HasColumnType("float");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MinimumRentingDays")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PossibleRentingPeriodID");

                    b.HasIndex("BNBID");

                    b.ToTable("PossibleRentingPeriods");

                    b.HasData(
                        new
                        {
                            PossibleRentingPeriodID = -1,
                            BNBID = -1,
                            DailyPrice = 123456.0,
                            EndDate = new DateTime(2022, 6, 24, 16, 48, 10, 468, DateTimeKind.Local).AddTicks(7021),
                            MinimumRentingDays = 12,
                            StartDate = new DateTime(2022, 5, 31, 16, 48, 10, 468, DateTimeKind.Local).AddTicks(7000)
                        });
                });

            modelBuilder.Entity("Mybnb.api.Models.TenantPeriod", b =>
                {
                    b.Property<int>("TenantPeriodID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TenantPeriodID"), 1L, 1);

                    b.Property<int?>("BNBID")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TenantUserID")
                        .HasColumnType("int");

                    b.HasKey("TenantPeriodID");

                    b.HasIndex("BNBID");

                    b.HasIndex("TenantUserID");

                    b.ToTable("TenantPeriods");
                });

            modelBuilder.Entity("Mybnb.api.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserID = -1,
                            Email = "test@edu.ucl.dk",
                            FullName = "test",
                            Password = "9F86D081884C7D659A2FEAA0C55AD015A3BF4F1B2B0B822CD15D6C15B0F00A08"
                        });
                });

            modelBuilder.Entity("Mybnb.api.Models.BNB", b =>
                {
                    b.HasOne("Mybnb.api.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Mybnb.api.Models.BnbImage", b =>
                {
                    b.HasOne("Mybnb.api.Models.BNB", null)
                        .WithMany("Images")
                        .HasForeignKey("BNBID");
                });

            modelBuilder.Entity("Mybnb.api.Models.PossibleRentingPeriod", b =>
                {
                    b.HasOne("Mybnb.api.Models.BNB", null)
                        .WithMany("RentingPeriods")
                        .HasForeignKey("BNBID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mybnb.api.Models.TenantPeriod", b =>
                {
                    b.HasOne("Mybnb.api.Models.BNB", null)
                        .WithMany("TenantPeriods")
                        .HasForeignKey("BNBID");

                    b.HasOne("Mybnb.api.Models.User", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("Mybnb.api.Models.BNB", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("RentingPeriods");

                    b.Navigation("TenantPeriods");
                });
#pragma warning restore 612, 618
        }
    }
}
