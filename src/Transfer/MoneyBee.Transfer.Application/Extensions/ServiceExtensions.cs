namespace MoneyBee.Transfer.Application.Extensions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using MoneyBee.Common.Extensions;
    using MoneyBee.Transfer.Application.Models.Settings;
    using MoneyBee.Transfer.Application.Services;
    using MoneyBee.Transfer.Application.Services.Interfaces;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Provides extension methods for the IServiceCollection interface.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        /// <summary>
        /// Add transfer application
        /// </summary>
        public static void AddTransferApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransferServices();
            services.AddCommonServices(configuration);
        }

        /// <summary>
        /// Register transfer services
        /// </summary>
        private static void AddTransferServices(this IServiceCollection services)
        {
            #region "Scoped Services"
            services.TryAddScoped<ITransactionService, TransactionService>();
            #endregion
        }
    }
}