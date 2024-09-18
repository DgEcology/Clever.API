using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Clever.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Clever.Domain
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<OrganiserApplication> OrganiserApplications{ get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Event>()
            .HasOne(e => e.Tag)
            .WithMany(t => t.Events)
            .HasForeignKey(e => e.TagId);

            modelBuilder.Entity<Event>()
            .HasOne(e => e.User)
            .WithMany(u => u.Events)
            .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Reaction>()
            .HasOne(r => r.Event)
            .WithMany(e => e.Reactions)
            .HasForeignKey(r => r.EventId);

            modelBuilder.Entity<Reaction>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reactions)
            .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Attendance>()
            .HasOne(a => a.Event)
            .WithMany(e => e.Attendances)
            .HasForeignKey(a => a.EventId);

            modelBuilder.Entity<Attendance>()
            .HasOne(a => a.User)
            .WithMany(u => u.Attendances)
            .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<OrganiserApplication>()
            .HasOne(o => o.User)
            .WithMany(u => u.OrganiserApplications)
            .HasForeignKey(o => o.UserId);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("dbsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();
            optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        }
    }
}