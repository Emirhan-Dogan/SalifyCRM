using Microsoft.EntityFrameworkCore;
using SalifyCRM.InteractionManagement.Domain.Entities;
using SalifyCRM.InteractionManagement.Persistence.EntityFrameworkCore.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Persistence.EntityFrameworkCore.Contexts
{
    public class SalifyDbContext : DbContext
    {
        public SalifyDbContext(DbContextOptions<SalifyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InteractionConfigurations());
            modelBuilder.ApplyConfiguration(new InteractionNoteConfigurations());
            modelBuilder.ApplyConfiguration(new InteractionTypeConfigurations());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<InteractionNote> InteractionNotes { get; set; }
        public DbSet<InteractionType> InteractionTypes { get; set; }
    }
}
