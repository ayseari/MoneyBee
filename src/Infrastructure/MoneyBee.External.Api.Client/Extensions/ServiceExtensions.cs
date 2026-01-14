namespace MoneyBee.External.Api.Client.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MoneyBee.Common.Models.Settings;
    using MoneyBee.External.Api.Client.Interfaces;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Refit;

    /// <summary>
    /// Provides extension methods for the IServiceCollection interface.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        /// <summary>
        /// AddKycVerificationApiClient method adds the IKycVerificationApiClient to the IServiceCollection with the specified apiUrl.
        /// </summary>
        public static void AddKycVerificationApiClient(this IServiceCollection services, IConfiguration configurationManager)
        {
            var appSettings = configurationManager.GetSection(ExternalApiSettings.SectionName).Get<ExternalApiSettings>();

            if (string.IsNullOrEmpty(appSettings?.KycApiBaseUrl))
            {
                throw new Exception("Kyc verification api base url is not found. Path:ExternalApiSettings");
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
                .AddRefitClient<IKycVerificationApiClient>(refitSettings)
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri(appSettings.KycApiBaseUrl);
                    client.Timeout = TimeSpan.FromSeconds(30);
                });
        }

        /// <summary>
        /// AddFraudDetectionApiClient method adds the IFraudDetectionApiClient to the IServiceCollection with the specified apiUrl.
        /// </summary>
        public static void AddFraudDetectionApiClient(this IServiceCollection services, IConfiguration configurationManager)
        {
            var appSettings = configurationManager.GetSection(ExternalApiSettings.SectionName).Get<ExternalApiSettings>();

            if (string.IsNullOrEmpty(appSettings?.FraudDetectionApiBaseUrl))
            {
                throw new Exception("Fraud Detection api base url is not found. Path:ExternalApiSettings");
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
                .AddRefitClient<IFraudDetectionApiClient>(refitSettings)
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri(appSettings.FraudDetectionApiBaseUrl);
                    client.Timeout = TimeSpan.FromSeconds(30);
                });
        }

        /// <summary>
        /// AddExchangeRateApiClient method adds the IExchangeRateApiClient to the IServiceCollection with the specified apiUrl.
        /// </summary>
        public static void AddExchangeRateApiClient(this IServiceCollection services, IConfiguration configurationManager)
        {
            var appSettings = configurationManager.GetSection(ExternalApiSettings.SectionName).Get<ExternalApiSettings>();

            if (string.IsNullOrEmpty(appSettings?.ExchangeRateApiBaseUrl))
            {
                throw new Exception("Exchange Rate api base url is not found. Path:ExternalApiSettings");
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
                .AddRefitClient<IExchangeRateApiClient>(refitSettings)
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri(appSettings.ExchangeRateApiBaseUrl);
                    client.Timeout = TimeSpan.FromSeconds(30);
                });
        }
    }
}