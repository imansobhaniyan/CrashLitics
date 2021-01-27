﻿// <auto-generated />
using System;
using Ighan.CrashLitics.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ighan.CrashLitics.DataAccessLayer.Migrations
{
    [DbContext(typeof(CrashLiticsDbContext))]
    partial class CrashLiticsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Ighan.CrashLitics.StorageModels.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Ighan.CrashLitics.StorageModels.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("DeviceUniqueIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<string>("Release")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SDK")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Ighan.CrashLitics.StorageModels.ExceptionLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeviceId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackTrace")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("ExceptionLogs");
                });

            modelBuilder.Entity("Ighan.CrashLitics.StorageModels.InnerExceptionLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<int>("ExceptionLogId")
                        .HasColumnType("int");

                    b.Property<long?>("ExceptionLogId1")
                        .HasColumnType("bigint");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackTrace")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExceptionLogId1");

                    b.ToTable("InnerExceptionLogs");
                });

            modelBuilder.Entity("Ighan.CrashLitics.StorageModels.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("Ighan.CrashLitics.StorageModels.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("Ighan.CrashLitics.StorageModels.Brand", b =>
                {
                    b.HasOne("Ighan.CrashLitics.StorageModels.Manufacturer", "Manufacturer")
                        .WithMany("Brands")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("Ighan.CrashLitics.StorageModels.Device", b =>
                {
                    b.HasOne("Ighan.CrashLitics.StorageModels.Model", "Model")
                        .WithMany("Devices")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");
                });

            modelBuilder.Entity("Ighan.CrashLitics.StorageModels.ExceptionLog", b =>
                {
                    b.HasOne("Ighan.CrashLitics.StorageModels.Device", "Device")
                        .WithMany("ExceptionLogs")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("Ighan.CrashLitics.StorageModels.InnerExceptionLog", b =>
                {
                    b.HasOne("Ighan.CrashLitics.StorageModels.ExceptionLog", "ExceptionLog")
                        .WithMany("InnerExceptionLogs")
                        .HasForeignKey("ExceptionLogId1");

                    b.Navigation("ExceptionLog");
                });

            modelBuilder.Entity("Ighan.CrashLitics.StorageModels.Model", b =>
                {
                    b.HasOne("Ighan.CrashLitics.StorageModels.Brand", "Brand")
                        .WithMany("Models")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("Ighan.CrashLitics.StorageModels.Brand", b =>
                {
                    b.Navigation("Models");
                });

            modelBuilder.Entity("Ighan.CrashLitics.StorageModels.Device", b =>
                {
                    b.Navigation("ExceptionLogs");
                });

            modelBuilder.Entity("Ighan.CrashLitics.StorageModels.ExceptionLog", b =>
                {
                    b.Navigation("InnerExceptionLogs");
                });

            modelBuilder.Entity("Ighan.CrashLitics.StorageModels.Manufacturer", b =>
                {
                    b.Navigation("Brands");
                });

            modelBuilder.Entity("Ighan.CrashLitics.StorageModels.Model", b =>
                {
                    b.Navigation("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}
