namespace MoneyBee.Customer.Infrastructure.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;
    using MoneyBee.Customer.Application.Interfaces.External;
    using MoneyBee.Customer.Application.Services;
    using MoneyBee.Customer.Application.Services.Interfaces;
    using MoneyBee.Customer.Domain.Interfaces;
    using MoneyBee.Customer.Domain.Repositories;
    using MoneyBee.Customer.Infrastructure.ExternalServices;
    using MoneyBee.Customer.Infrastructure.Persistence;
    using MoneyBee.Customer.Infrastructure.Persistence.Contexts;
    using MoneyBee.Customer.Infrastructure.Repositories;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides extension methods for the IServiceCollection interface.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Add Customer Infrastructure
        /// </summary>
        public static void AddCustomerInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomerDbContexts(configuration);

            services.AddCustomerRepositories();
            services.AddExternalServices();
        }

        /// <summary>
        /// Register customer repositories
        /// </summary>
        private static void AddCustomerRepositories(this IServiceCollection services)
        {
            services.TryAddScoped<ICustomerUnitOfWork, CustomerUnitOfWork>();
            services.TryAddScoped<ICustomerRepository, CustomerRepository>();
        }

        /// <summary>
        /// Register customer db context
        /// </summary>
        private static void AddCustomerDbContexts(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ICustomerDbContext, CustomerDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("MoneyBeeCustomerConnStr"));
            });
        }

        /// <summary>
        /// Register external services
        /// </summary>
        private static void AddExternalServices(this IServiceCollection services)
        {
            #region "Scoped Services"
            services.TryAddScoped<IKycService, KycService>();
            #endregion
        }

        /// <summary>
        /// Apply Migrations
        /// </summary>
        public static async Task ApplyMigrations(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<CustomerDbContext>>();

            try
            {
                var context = services.GetRequiredService<CustomerDbContext>();

                await context.Database.EnsureCreatedAsync();

                var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

                if (pendingMigrations.Any())
                {
                    logger.LogInformation(
                        "Applying {Count} pending migrations: {Migrations}",
                        pendingMigrations.Count(),
                        string.Join(", ", pendingMigrations)
                    );

                    await context.Database.MigrateAsync();

                    logger.LogInformation("Migrations applied successfully");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database");
            }
        }
    }
}
