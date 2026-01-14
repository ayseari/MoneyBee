namespace MoneyBee.Auth.Api.Extensions
{
    using System.Diagnostics.CodeAnalysis;
    using MoneyBee.Auth.Application.Extensions;
    using MoneyBee.Auth.Infrastructure.Extensions;

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
            services.AddAuthApplication(configurationManager);
            services.AddAuthInfrastructure(configurationManager);
        }
    }
}