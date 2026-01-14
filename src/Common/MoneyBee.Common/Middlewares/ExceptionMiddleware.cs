namespace MoneyBee.Common.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using MoneyBee.Common.Extensions;
    using MoneyBee.Common.Models.Exception;
    using System;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// Exception Middleware
    /// </summary>
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        private readonly ILogger _logger = logger;

        /// <summary>
        /// Invoke next request
        /// </summary>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception : {Type}", ex.GetType().Name);
                switch (ex)
                {
                    case ApiException apiException:
                        await httpContext.HandleException(apiException.StatusCode, apiException.Error);
                        break;
                    default:
                        await httpContext.HandleException((int)HttpStatusCode.InternalServerError, "An exception occured");
                        break;
                }
            }
        }
    }
}
