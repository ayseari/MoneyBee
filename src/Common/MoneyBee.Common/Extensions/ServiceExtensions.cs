namespace MoneyBee.Common.Extensions
{
    using Mapster;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using MoneyBee.Common.Validations;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Service Extensions
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        /// <summary>
        /// Register common services
        /// </summary>
        public static void AddCommonServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IBusinessRuleValidator, BusinessRuleValidator>();
            services.TryAddSingleton<IValidatorFactory, ValidatorFactory>();
            services.AddMapster();

        }
    }
}