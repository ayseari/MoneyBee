namespace MoneyBee.Common.Extensions
{
    using Microsoft.AspNetCore.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// HttpContextExtensions
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// HandleException
        /// </summary>
        public static Task HandleException(this HttpContext context, int statusCode, string errorMessage)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = statusCode;

            var errorResponse = new
            {
                status = statusCode,
                error = errorMessage
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
