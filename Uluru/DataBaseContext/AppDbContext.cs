using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.Models;

namespace Uluru.DataBaseContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Position> Positions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkingGroup> WorkingGroups {get; set;}
        public DbSet<WorkingGroupSchedule> WorkingGroupSchedules { get; set; }
        public DbSet<WorkingDay> WorkingDays { get; set; }
        public DbSet<WorkEntry> WorkEntries { get; set; }
        public DbSet<WorkingAvailability> WorkingAvailabilities { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<User>().ToTable("User").HasAlternateKey(u => u.Email);
            model.Entity<WorkingGroup>().ToTable("WorkingGroup");
            model.Entity<WorkingGroupSchedule>().ToTable("WorkingGroupSchedule");
            model.Entity<WorkingDay>().ToTable("WorkingDay");
            model.Entity<WorkEntry>().ToTable("WorkEntry");
            model.Entity<WorkingAvailability>().ToTable("WorkingAvailability");
            model.Entity<Position>().ToTable("Position");

            model.Entity<User>().HasMany(u => u.WorkingAvailabilities).WithOne(w => w.User);
            model.Entity<User>().HasOne(u => u.Position).WithMany().OnDelete(DeleteBehavior.SetNull);
            model.Entity<WorkingGroup>().HasMany(w => w.Users).WithOne(u => u.WorkingGroup);
            model.Entity<WorkingGroup>().HasMany(w => w.Positions).WithOne().OnDelete(DeleteBehavior.NoAction);
            model.Entity<WorkingGroupSchedule>().HasOne(w => w.WorkingGroup).WithMany(w => w.WorkingGroupSchedules);
            model.Entity<WorkingGroupSchedule>().HasMany(w => w.WorkingDays).WithOne(w => w.WorkingGroupSchedule);
            model.Entity<WorkingDay>().HasMany(w => w.WorkEntries).WithOne(w => w.WorkingDay);
            model.Entity<WorkingAvailability>().HasOne(w => w.WorkEntry).WithOne(w => w.WorkingAvailability)
                .HasForeignKey<WorkEntry>(w => w.WorkingAvailabilityId);
            model.Entity<WorkEntry>().HasOne(w => w.Position).WithMany();

            model.Entity<User>()
                .Property(u => u.UserRole)
                .HasConversion(
                    v => v.ToString(),
                    v => (UserRole)Enum.Parse(typeof(UserRole), v));
        }
    }
}
