using Microsoft.EntityFrameworkCore;
using SalifyCRM.ActivityManagement.Domain.Entities;
using SalifyCRM.ActivityManagement.Persistence.EntityFrameworkCore.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ActivityManagement.Persistence.EntityFrameworkCore.Contexts
{
    public class SalifyDbContext : DbContext
    {
        public SalifyDbContext(DbContextOptions<SalifyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ActivityConfigurations());
            modelBuilder.ApplyConfiguration(new ActivityNoteConfigurations());
            modelBuilder.ApplyConfiguration(new ActivityTypeConfigurations());
            modelBuilder.ApplyConfiguration(new ActivityStatusConfigurations());
            modelBuilder.ApplyConfiguration(new ActivityUserConfigurations());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityNote> ActivityNotes { get; set; }
        public DbSet<ActivityUser> ActivityUsers { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<ActivityStatus> ActivityStatuses { get; set; }
    }
}
