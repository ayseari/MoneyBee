namespace MoneyBee.Auth.Infrastructure.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;
    using MoneyBee.Auth.Domain.Interfaces;
    using MoneyBee.Auth.Domain.Repositories;
    using MoneyBee.Auth.Infrastructure.Persistence;
    using MoneyBee.Auth.Infrastructure.Persistence.Contexts;
    using MoneyBee.Auth.Infrastructure.Repositories;

    /// <summary>
    /// Provides extension methods for the IServiceCollection interface.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Add Auth Infrastructure
        /// </summary>
        public static void AddAuthInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthDbContexts(configuration);

            services.AddAuthRepositories();
        }

        /// <summary>
        /// Register auth repositories
        /// </summary>
        private static void AddAuthRepositories(this IServiceCollection services)
        {
            services.TryAddScoped<IAuthUnitOfWork, AuthUnitOfWork>();
            services.TryAddScoped<IApiKeyRepository, ApiKeyRepository>();
        }

        /// <summary>
        /// Register auth db context
        /// </summary>
        private static void AddAuthDbContexts(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<IAuthDbContext, AuthDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("MoneyBeeAuthConnStr"));
            });
        }

        /// <summary>
        /// Apply Migrations
        /// </summary>
        public static async Task ApplyMigrations(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<AuthDbContext>>();

            try
            {
                var context = services.GetRequiredService<AuthDbContext>();

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
