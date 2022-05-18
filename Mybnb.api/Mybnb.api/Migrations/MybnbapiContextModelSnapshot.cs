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

                    b.Property<int>("TypeOfEstablishment")
                        .HasColumnType("int");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("BNBs");
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

                    b.Property<int?>("BNBID")
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
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
                        .HasForeignKey("BNBID");
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
