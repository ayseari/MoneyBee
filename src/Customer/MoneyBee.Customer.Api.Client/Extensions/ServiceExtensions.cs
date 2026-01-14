namespace MoneyBee.Customer.Api.Client.Extensions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MoneyBee.Common.Models.Settings;
    using MoneyBee.Customer.Api.Client.Interfaces;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Refit;
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Provides extension methods for the IServiceCollection interface.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        /// <summary>
        /// AddCustomerApiClient method adds the ICustomerApiClient to the IServiceCollection with the specified apiUrl.
        /// </summary>
        public static void AddCustomerApiClient(this IServiceCollection services, IConfiguration configurationManager)
        {
            var appSettings = configurationManager.GetSection(InternalApiSettings.SectionName).Get<InternalApiSettings>();

            if (string.IsNullOrEmpty(appSettings?.CustomerApiBaseUrl))
            {
                throw new Exception("Customer api base url is not found. Path:InternalApiSettings");
            }

            var refitSettings = new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings(new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }))
            };

            services
                .AddRefitClient<ICustomerApiClient>(refitSettings)
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri(appSettings.CustomerApiBaseUrl);
                    client.Timeout = TimeSpan.FromSeconds(30);
                });
        }
    }
}
