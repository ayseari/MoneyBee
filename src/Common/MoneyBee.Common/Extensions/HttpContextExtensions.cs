namespace MoneyBee.Common.Extensions
{
    using Microsoft.AspNetCore.Http;
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

            return context.Response.WriteAsync(errorMessage);
        }
    }
}
