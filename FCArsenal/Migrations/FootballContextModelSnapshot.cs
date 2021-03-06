﻿// <auto-generated />
using System;
using FCArsenal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FCArsenal.Migrations
{
    [DbContext(typeof(FootballContext))]
    partial class FootballContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FCArsenal.Models.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Budget")
                        .HasColumnType("money");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int?>("StaffID");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("DepartmentID");

                    b.HasIndex("StaffID");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("FCArsenal.Models.OfficeAssignment", b =>
                {
                    b.Property<int>("StaffID");

                    b.Property<string>("Location")
                        .HasMaxLength(50);

                    b.HasKey("StaffID");

                    b.ToTable("OfficeAssignment");
                });

            modelBuilder.Entity("FCArsenal.Models.Player", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasColumnName("FirstName")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("SigningDate");

                    b.HasKey("ID");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("FCArsenal.Models.Signing", b =>
                {
                    b.Property<int>("SigningID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Form");

                    b.Property<int>("PlayerID");

                    b.Property<int>("TrainingID");

                    b.HasKey("SigningID");

                    b.HasIndex("PlayerID");

                    b.HasIndex("TrainingID");

                    b.ToTable("Signing");
                });

            modelBuilder.Entity("FCArsenal.Models.Staff", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasColumnName("FirstName")
                        .HasMaxLength(50);

                    b.Property<DateTime>("HireDate");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("FCArsenal.Models.Training", b =>
                {
                    b.Property<int>("TrainingID");

                    b.Property<int>("Credits");

                    b.Property<int>("DepartmentID");

                    b.Property<string>("Title")
                        .HasMaxLength(50);

                    b.HasKey("TrainingID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Training");
                });

            modelBuilder.Entity("FCArsenal.Models.TrainingAssignment", b =>
                {
                    b.Property<int>("TrainingID");

                    b.Property<int>("StaffID");

                    b.HasKey("TrainingID", "StaffID");

                    b.HasIndex("StaffID");

                    b.ToTable("TrainingAssignment");
                });

            modelBuilder.Entity("FCArsenal.Models.Department", b =>
                {
                    b.HasOne("FCArsenal.Models.Staff", "Administrator")
                        .WithMany()
                        .HasForeignKey("StaffID");
                });

            modelBuilder.Entity("FCArsenal.Models.OfficeAssignment", b =>
                {
                    b.HasOne("FCArsenal.Models.Staff", "Staff")
                        .WithOne("OfficeAssignment")
                        .HasForeignKey("FCArsenal.Models.OfficeAssignment", "StaffID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FCArsenal.Models.Signing", b =>
                {
                    b.HasOne("FCArsenal.Models.Player", "Player")
                        .WithMany("Signings")
                        .HasForeignKey("PlayerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FCArsenal.Models.Training", "Training")
                        .WithMany("Signings")
                        .HasForeignKey("TrainingID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FCArsenal.Models.Training", b =>
                {
                    b.HasOne("FCArsenal.Models.Department", "Department")
                        .WithMany("Trainings")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FCArsenal.Models.TrainingAssignment", b =>
                {
                    b.HasOne("FCArsenal.Models.Staff", "Staff")
                        .WithMany("TrainingAssignments")
                        .HasForeignKey("StaffID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FCArsenal.Models.Training", "Training")
                        .WithMany("TrainingAssignments")
                        .HasForeignKey("TrainingID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
