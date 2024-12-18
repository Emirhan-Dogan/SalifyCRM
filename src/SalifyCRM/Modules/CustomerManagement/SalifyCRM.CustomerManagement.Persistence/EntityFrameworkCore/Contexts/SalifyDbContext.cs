using Microsoft.EntityFrameworkCore;
using SalifyCRM.CustomerManagement.Domain.Entities;
using SalifyCRM.CustomerManagement.Persistence.EntityFrameworkCore.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Persistence.EntityFrameworkCore.Contexts
{
    public class SalifyDbContext : DbContext
    {
        public SalifyDbContext(DbContextOptions<SalifyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressConfigurations());
            modelBuilder.ApplyConfiguration(new AddressTypeConfigurations());
            modelBuilder.ApplyConfiguration(new CityConfigurations());
            modelBuilder.ApplyConfiguration(new CountryConfigurations());
            modelBuilder.ApplyConfiguration(new CustomerConfigurations());
            modelBuilder.ApplyConfiguration(new CustomerAddressConfigurations());
            modelBuilder.ApplyConfiguration(new CustomerNoteConfigurations());
            modelBuilder.ApplyConfiguration(new CustomerPermissionConfigurations());
            modelBuilder.ApplyConfiguration(new CustomerSocialConfigurations());
            modelBuilder.ApplyConfiguration(new CustomerTagConfigurations());
            modelBuilder.ApplyConfiguration(new CustomerTagMappingConfigurations());
            modelBuilder.ApplyConfiguration(new CustomerTypeConfigurations());
            modelBuilder.ApplyConfiguration(new DistrictConfigurations());
            modelBuilder.ApplyConfiguration(new MartialStatusConfigurations());
            modelBuilder.ApplyConfiguration(new OccupationConfigurations());
            modelBuilder.ApplyConfiguration(new PermissionConfigurations());
            modelBuilder.ApplyConfiguration(new SocialMediaConfigurations());
            modelBuilder.ApplyConfiguration(new CustomerCategoryConfigurations());

            modelBuilder.ApplyConfiguration(new UserConfigurations());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<CustomerNote> CustomerNotes { get; set; }
        public DbSet<CustomerPermission> CustomerPermissions { get; set; }
        public DbSet<CustomerSocial> CustomerSocials { get; set; }
        public DbSet<CustomerTag> CustomerTags { get; set; }
        public DbSet<CustomerTagMapping> CustomerTagMappings { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<MartialStatus> MartialStatuses { get; set; }
        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<CustomerCategory> CustomerCategories { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
