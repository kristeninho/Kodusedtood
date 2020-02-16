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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Training>().ToTable("Training");
            modelBuilder.Entity<Signing>().ToTable("Signing");
            modelBuilder.Entity<Player>().ToTable("Player");
        }
    }
}
