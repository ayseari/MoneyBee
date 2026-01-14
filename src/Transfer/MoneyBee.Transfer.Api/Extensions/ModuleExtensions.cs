namespace MoneyBee.Transfer.Api.Extensions
{
    using MoneyBee.Transfer.Application.Extensions;
    using MoneyBee.Transfer.Infrastructure.Extensions;
    using MoneyBee.External.Api.Client.Extensions;
    using MoneyBee.Customer.Api.Client.Extensions;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Module extensions
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ModuleExtensions
    {
        /// <summary>
        /// Register Domain Modules
        /// </summary>
        public static void AddModules(this IServiceCollection services, IConfiguration configurationManager)
        {
            services.AddTransferApplication(configurationManager);
            services.AddTransferInfrastructure(configurationManager);
        }

        /// <summary>
        /// Register Api Clients
        /// </summary>
        public static void AddApiClients(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddExchangeRateApiClient(configurationManager);
            services.AddFraudDetectionApiClient(configurationManager);
            services.AddCustomerApiClient(configurationManager);
        }
    }
}