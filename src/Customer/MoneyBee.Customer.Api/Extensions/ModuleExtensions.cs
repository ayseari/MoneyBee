namespace MoneyBee.Customer.Api.Extensions
{
    using MoneyBee.Customer.Application.Extensions;
    using MoneyBee.Customer.Infrastructure.Extensions;
    using MoneyBee.External.Api.Client.Extensions;
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
            services.AddCustomerApplication(configurationManager);
            services.AddCustomerInfrastructure(configurationManager);
        }

        /// <summary>
        /// Register Api Clients
        /// </summary>
        public static void AddApiClients(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddKycVerificationApiClient(configurationManager);
        }
    }
}