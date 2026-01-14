namespace MoneyBee.Auth.Api.Filters
{
    using Microsoft.OpenApi.Any;
    using Microsoft.OpenApi.Models;
    using MoneyBee.Auth.Application.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    /// Example Schema Filter
    /// </summary>
    public class ExampleSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(CreateApiKeyRequest))
            {
                schema.Example = new OpenApiObject
                {
                    ["clientName"] = new OpenApiString("Test Client"),
                    ["description"] = new OpenApiString("API key for testing purposes"),
                    ["rateLimit"] = new OpenApiInteger(100),
                    ["createdBy"] = new OpenApiString("admin")
                };
            }

            //if (context.Type == typeof(ValidateApiKeyRequest))
            //{
            //    schema.Example = new OpenApiObject
            //    {
            //        ["apiKey"] = new OpenApiString("mb_xK9mP2nQ5rT8vW3yZ4aB6cD7eF9gH0jK1lM2")
            //    };
            //}
        }
    }
}