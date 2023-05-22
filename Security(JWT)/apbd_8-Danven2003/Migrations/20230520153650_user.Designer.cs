﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zadanie8.Models;

#nullable disable

namespace Zadanie8.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20230520153650_user")]
    partial class user
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Prescription_Medicament", b =>
                {
                    b.Property<int>("IdPrescription")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("IdMedicament")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Dose")
                        .HasColumnType("int");

                    b.HasKey("IdPrescription", "IdMedicament");

                    b.HasIndex("IdMedicament");

                    b.ToTable("Prescription_Medicaments");

                    b.HasData(
                        new
                        {
                            IdPrescription = 1,
                            IdMedicament = 1,
                            Details = "Details for Medicament 1",
                            Dose = 1
                        },
                        new
                        {
                            IdPrescription = 2,
                            IdMedicament = 2,
                            Details = "Details for Medicament 2",
                            Dose = 2
                        });
                });

            modelBuilder.Entity("Zadanie8.Models.Doctor", b =>
                {
                    b.Property<int>("IdDoctor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDoctor"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdDoctor");

                    b.ToTable("Doctors");

                    b.HasData(
                        new
                        {
                            IdDoctor = 1,
                            Email = "johndoe@example.com",
                            FirstName = "John",
                            LastName = "Doe"
                        },
                        new
                        {
                            IdDoctor = 2,
                            Email = "janesmith@example.com",
                            FirstName = "Jane",
                            LastName = "Smith"
                        });
                });

            modelBuilder.Entity("Zadanie8.Models.Medicament", b =>
                {
                    b.Property<int>("IdMedicament")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMedicament"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdMedicament");

                    b.ToTable("Medications");

                    b.HasData(
                        new
                        {
                            IdMedicament = 1,
                            Description = "Description for Medicament 1",
                            Name = "Medicament 1",
                            Type = "Type 1"
                        },
                        new
                        {
                            IdMedicament = 2,
                            Description = "Description for Medicament 2",
                            Name = "Medicament 2",
                            Type = "Type 2"
                        });
                });

            modelBuilder.Entity("Zadanie8.Models.Patient", b =>
                {
                    b.Property<int>("IdPatient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPatient"));

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdPatient");

                    b.ToTable("Patients");

                    b.HasData(
                        new
                        {
                            IdPatient = 1,
                            Birthdate = new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Alice",
                            LastName = "Anderson"
                        },
                        new
                        {
                            IdPatient = 2,
                            Birthdate = new DateTime(1995, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Bob",
                            LastName = "Brown"
                        });
                });

            modelBuilder.Entity("Zadanie8.Models.Prescription", b =>
                {
                    b.Property<int>("IdPrescription")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPrescription"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdDoctor")
                        .HasColumnType("int");

                    b.Property<int>("IdPatient")
                        .HasColumnType("int");

                    b.HasKey("IdPrescription");

                    b.HasIndex("IdDoctor");

                    b.HasIndex("IdPatient");

                    b.ToTable("Prescriptions");

                    b.HasData(
                        new
                        {
                            IdPrescription = 1,
                            Date = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DueDate = new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdDoctor = 1,
                            IdPatient = 1
                        },
                        new
                        {
                            IdPrescription = 2,
                            Date = new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DueDate = new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdDoctor = 2,
                            IdPatient = 2
                        });
                });

            modelBuilder.Entity("Zadanie8.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUser"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("IdUser");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Prescription_Medicament", b =>
                {
                    b.HasOne("Zadanie8.Models.Medicament", "Medicament")
                        .WithMany("Prescription_Medicaments")
                        .HasForeignKey("IdMedicament")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Zadanie8.Models.Prescription", "Prescription")
                        .WithMany("Prescription_Medicaments")
                        .HasForeignKey("IdPrescription")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicament");

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("Zadanie8.Models.Prescription", b =>
                {
                    b.HasOne("Zadanie8.Models.Doctor", "Doctor")
                        .WithMany("Prescriptions")
                        .HasForeignKey("IdDoctor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Zadanie8.Models.Patient", "Patient")
                        .WithMany("Prescriptions")
                        .HasForeignKey("IdPatient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Zadanie8.Models.Doctor", b =>
                {
                    b.Navigation("Prescriptions");
                });

            modelBuilder.Entity("Zadanie8.Models.Medicament", b =>
                {
                    b.Navigation("Prescription_Medicaments");
                });

            modelBuilder.Entity("Zadanie8.Models.Patient", b =>
                {
                    b.Navigation("Prescriptions");
                });

            modelBuilder.Entity("Zadanie8.Models.Prescription", b =>
                {
                    b.Navigation("Prescription_Medicaments");
                });
#pragma warning restore 612, 618
        }
    }
}
