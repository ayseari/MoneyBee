namespace MoneyBee.Transfer.Infrastructure.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;
    using MoneyBee.Common.Models.Settings;
    using MoneyBee.Transfer.Application.Models.Settings;
    using MoneyBee.Transfer.Application.Services.Interfaces.External;
    using MoneyBee.Transfer.Domain.Interfaces;
    using MoneyBee.Transfer.Domain.Repositories;
    using MoneyBee.Transfer.Infrastructure.ExternalServices;
    using MoneyBee.Transfer.Infrastructure.Persistence;
    using MoneyBee.Transfer.Infrastructure.Persistence.Contexts;
    using MoneyBee.Transfer.Infrastructure.Repositories;

    /// <summary>
    /// Provides extension methods for the IServiceCollection interface.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Add Transfer Infrastructure
        /// </summary>
        public static void AddTransferInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransferDbContexts(configuration);

            services.AddTransferRepositories();

            services.AddExternalServices();

            services.AddSettings(configuration);
        }

        /// <summary>
        /// Register transfer repositories
        /// </summary>
        private static void AddTransferRepositories(this IServiceCollection services)
        {
            services.TryAddScoped<ITransferUnitOfWork, TransferUnitOfWork>();
            services.TryAddScoped<ITransactionRepository, TransactionRepository>();
        }

        /// <summary>
        /// Register transfer db context
        /// </summary>
        private static void AddTransferDbContexts(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ITransferDbContext, TransferDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("MoneyBeeTransferConnStr"));
            });
        }

        /// <summary>
        /// Register external services
        /// </summary>
        private static void AddExternalServices(this IServiceCollection services)
        {
            #region "Scoped Services"
            services.TryAddScoped<IFraudDetectionService, FraudDetectionService>();
            services.TryAddScoped<IExchangeRateService, ExchangeRateService>();
            services.TryAddScoped<ICustomerExternalService, CustomerExternalService>();
            #endregion
        }

        /// <summary>
        /// Register settings
        /// </summary>
        private static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MoneyTransferSettings>(
                configuration.GetSection(MoneyTransferSettings.SectionName));
        }

        /// <summary>
        /// Apply Migrations
        /// </summary>
        public static async Task ApplyMigrations(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TransferDbContext>>();

            try
            {
                var context = services.GetRequiredService<TransferDbContext>();

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
