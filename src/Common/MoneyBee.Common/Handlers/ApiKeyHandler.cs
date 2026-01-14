using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBee.Common.Handlers
{
    public class ApiKeyHandler : DelegatingHandler
    {
        private readonly string _apiKey;

        public ApiKeyHandler(IConfiguration configuration)
        {
            _apiKey = configuration["InternalApiSettings:ApiKey"];
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.Headers.Add("X-API-Key", _apiKey);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
