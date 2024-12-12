using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Persistence.EntityFrameworkCore.Contexts;
using SalifyCRM.IdentityServer.Persistence.EntityFrameworkCore.Repositories;

namespace SalifyCRM.IdentityServer.Persistence.EntityFrameworkCore
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

            // Repository'leri kaydedelim
            services.AddScoped<IUserRepository, EFUserRepository>();
            services.AddScoped<IOperationClaimRepository, EFOperationClaimRepository>();
            services.AddScoped<IUserOperationClaimRepository, EFUserOperationClaimRepository>();
            services.AddScoped<IGroupRepository, EFGroupRepository>();
            services.AddScoped<IGroupClaimRepository, EFGroupClaimRepository>();
            services.AddScoped<IUserGroupRepository, EFUserGroupRepository>();

            return services;
        }
    }
}
