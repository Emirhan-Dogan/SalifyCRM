using Microsoft.EntityFrameworkCore;
using SalifyCRM.IdentityServer.Domain.Entities;
using SalifyCRM.IdentityServer.Persistence.EntityFrameworkCore.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Persistence.EntityFrameworkCore.Contexts
{
    public class SalifyDbContext : DbContext
    {
        public SalifyDbContext(DbContextOptions<SalifyDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfigurations());
            modelBuilder.ApplyConfiguration(new OperationClaimConfigurations());
            modelBuilder.ApplyConfiguration(new UserOperationClaimConfigurations());
            modelBuilder.ApplyConfiguration(new GroupConfigurations());
            modelBuilder.ApplyConfiguration(new GroupClaimConfigurations());
            modelBuilder.ApplyConfiguration(new UserGroupConfigurations());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<GroupClaim> GroupClaims { get; set; }
    }
}
