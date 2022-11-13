using Application.Admin.Common.Interfaces;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IClientApplicationDbContext = Application.Client.Common.Interfaces.IApplicationDbContext;

namespace Infrastructure.Persistence
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddAdminPersistenceInfrastructureLayer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AdminApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AdminApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<AdminApplicationDbContext>());
            services.AddTransient<ISeeder, Seeder>();
            return services;
        }

        public static IServiceCollection AddClientPersistenceInfrastructureLayer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ClientApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging();
            });

            services.AddScoped<IClientApplicationDbContext>(provider =>
                provider.GetService<ClientApplicationDbContext>());

            return services;
        }
    }
}