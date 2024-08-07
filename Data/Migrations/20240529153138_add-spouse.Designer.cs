﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(RentingContext))]
    [Migration("20240529153138_add-spouse")]
    partial class addspouse
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Data.TestEntity.Dependent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Birthdate")
                        .HasColumnType("date");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Dependent");
                });

            modelBuilder.Entity("Data.TestEntity.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Birthdate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SpouseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SpouseId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Data.TestEntity.Spouse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Birthdate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Spouse");
                });

            modelBuilder.Entity("Data.TestEntity.Dependent", b =>
                {
                    b.HasOne("Data.TestEntity.Employee", null)
                        .WithMany("Dependents")
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("Data.TestEntity.Employee", b =>
                {
                    b.HasOne("Data.TestEntity.Spouse", "Spouse")
                        .WithMany()
                        .HasForeignKey("SpouseId");

                    b.Navigation("Spouse");
                });

            modelBuilder.Entity("Data.TestEntity.Employee", b =>
                {
                    b.Navigation("Dependents");
                });
#pragma warning restore 612, 618
        }
    }
}
