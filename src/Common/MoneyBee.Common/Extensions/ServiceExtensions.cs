namespace MoneyBee.Common.Extensions
{
    using Mapster;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using MoneyBee.Common.Caching;
    using MoneyBee.Common.Models.Settings;
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
        public static void AddCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton<IBusinessRuleValidator, BusinessRuleValidator>();
            services.TryAddSingleton<IValidatorFactory, ValidatorFactory>();
            services.AddMapster();
            services.AddRedis(configuration);
        }

        private static void AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RedisSettings>(
                    configuration.GetSection(RedisSettings.SectionName));

            services.AddStackExchangeRedisCache(options =>
            {
                var redisSettings = configuration
                    .GetSection(RedisSettings.SectionName)
                    .Get<RedisSettings>();

                options.Configuration = redisSettings.ConnectionString;
                options.InstanceName = redisSettings.InstanceName;
            });

            services.TryAddSingleton<IRedisDistributedCacheService, RedisDistributedCacheService>();            
        }
    }
}