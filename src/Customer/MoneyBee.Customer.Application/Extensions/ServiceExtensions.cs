namespace MoneyBee.Customer.Application.Extensions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using MoneyBee.Common.Extensions;
    using MoneyBee.Customer.Application.Interfaces.External;
    using MoneyBee.Customer.Application.Services;
    using MoneyBee.Customer.Application.Services.Interfaces;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Provides extension methods for the IServiceCollection interface.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        /// <summary>
        /// Add customer application
        /// </summary>
        public static void AddCustomerApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomerServices();
            services.AddCommonServices();
        }

        /// <summary>
        /// Register customer services
        /// </summary>
        private static void AddCustomerServices(this IServiceCollection services)
        {
            #region "Scoped Services"
            services.TryAddScoped<ICustomerService, CustomerService>();
            #endregion
        }
    }
}
