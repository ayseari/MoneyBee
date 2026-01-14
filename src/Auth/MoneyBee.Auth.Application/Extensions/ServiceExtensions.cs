namespace MoneyBee.Auth.Application.Extensions
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using MoneyBee.Auth.Application.Services;
    using MoneyBee.Auth.Application.Services.Interfaces;

    /// <summary>
    /// Provides extension methods for the IServiceCollection interface.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        /// <summary>
        /// Add auth application
        /// </summary>
        public static void AddAuthApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthServices();
        }

        /// <summary>
        /// Register auth services
        /// </summary>
        private static void AddAuthServices(this IServiceCollection services)
        {
            #region "Scoped Services"
            services.TryAddScoped<IApiKeyService, ApiKeyService>();
            #endregion
        }
    }
}
