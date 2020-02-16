﻿// <auto-generated />
using System;
using FCArsenal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FCArsenal.Migrations
{
    [DbContext(typeof(FootballContext))]
    [Migration("20200216191014_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FCArsenal.Models.Player", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstMidName");

                    b.Property<string>("LastName");

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

            modelBuilder.Entity("FCArsenal.Models.Training", b =>
                {
                    b.Property<int>("TrainingID");

                    b.Property<int>("Credits");

                    b.Property<string>("Title");

                    b.HasKey("TrainingID");

                    b.ToTable("Training");
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
#pragma warning restore 612, 618
        }
    }
}
