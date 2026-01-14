namespace MoneyBee.Auth.Api.Extensions
{
    using Microsoft.OpenApi.Models;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Reflection;
    using MoneyBee.Auth.Api.Filters;

    /// <summary>
    /// Service Extensions
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        /// <summary>
        /// Register Http and Controller Dependencies
        /// </summary>
        public static IServiceCollection AddHttp(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    var enumConverter = new JsonStringEnumConverter();
                    options.JsonSerializerOptions.Converters.Add(enumConverter);
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MoneyBee Auth API",
                    Description = "Authentication and API Key management service for MoneyBee money transfer system. " +
                      "This service handles API key generation, validation, and rate limiting for all MoneyBee microservices."
                });

                // XML Dokümantasyonu (method açıklamaları için)
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
                options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
                {
                    Description = "Basic auth added to authorization header",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "basic",
                    Type = SecuritySchemeType.Http,
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Basic" }
                    },
                    new List<string>()
                }});

                // For response examples
                options.EnableAnnotations();

                // Enum'ları string olarak göster
                options.SchemaFilter<EnumSchemaFilter>();

                // Örnek değerler için
                options.SchemaFilter<ExampleSchemaFilter>();
            });

            return services;
        }
    }
}
