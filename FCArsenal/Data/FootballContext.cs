using FCArsenal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCArsenal.Data
{
	public class FootballContext : DbContext
	{
        public FootballContext(DbContextOptions<FootballContext> options) : base(options)
        {
        }

        public DbSet<Training> Trainings { get; set; }
        public DbSet<Signing> Signings { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<TrainingAssignment> TrainingAssignments { get; set; }
        public DbSet<Person> People { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Training>().ToTable("Training");
            modelBuilder.Entity<Signing>().ToTable("Signing");
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Staff>().ToTable("Staff");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<TrainingAssignment>().ToTable("TrainingAssignment");
            modelBuilder.Entity<Person>().ToTable("Person");

            modelBuilder.Entity<TrainingAssignment>()
                .HasKey(c => new { c.TrainingID, c.StaffID });
        }
    }
}
