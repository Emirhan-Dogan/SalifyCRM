using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Persistence.EntityFrameworkCore.Contexts;
using SalifyCRM.CustomerManagement.Persistence.EntityFrameworkCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Persistence.EntityFrameworkCore
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Ortamı alalım ve veritabanı sağlayıcısını kontrol edelim
            var databaseProvider = configuration.GetSection("DatabaseProvider").Value;

            if (databaseProvider == "InMemory")
            {
                // Development ortamında InMemory veritabanı kullan
                services.AddDbContext<SalifyDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb"));
            }
            else if (databaseProvider == "PostgreSQL")
            {
                // Production ve Staging ortamlarında PostgreSQL kullan
                var connectionString = configuration.GetConnectionString("PgConnection");
                services.AddDbContext<SalifyDbContext>(options =>
                    options.UseNpgsql(connectionString));
            }

            services.AddScoped<IDistrictRepository, EFDistrictRepository>();
            services.AddScoped<ICityRepository, EFCityRepository>();
            services.AddScoped<ICountryRepository, EFCountryRepository>();
            services.AddScoped<IAddressTypeRepository, EFAddressTypeRepository>();
            services.AddScoped<IAddressRepository, EFAddressRepository>();
            services.AddScoped<IMartialStatusRepository, EFMartialStatusRepository>();
            services.AddScoped<IOccupationRepository, EFOccupationRepository>();
            services.AddScoped<ISocialMediaRepository, EFSocialMediaRepository>();
            services.AddScoped<IPermissionRepository, EFPermissionRepository>();
            services.AddScoped<ICustomerCategoryRepository, EFCustomerCategoryRepository>();
            services.AddScoped<ICustomerTagRepository, EFCustomerTagRepository>();
            services.AddScoped<ICustomerTypeRepository, EFCustomerTypeRepository>();
            services.AddScoped<ICustomerRepository, EFCustomerRepository>();
            services.AddScoped<ICustomerAddressRepository, EFCustomerAddressRepository>();
            services.AddScoped<ICustomerNoteRepository, EFCustomerNoteRepository>();
            services.AddScoped<ICustomerPermissionRepository, EFCustomerPermissionRepository>();
            services.AddScoped<ICustomerSocialRepository, EFCustomerSocialRepository>();
            services.AddScoped<ICustomerTagMappingRepository, EFCustomerTagMappingRepository>();

            return services;
        }
    }
}
