using Newtonsoft.Json;
using System.Collections.Generic;

namespace MoneyBee.External.Api.Client.Models.Exchange
{
    /// <summary>
    /// Exchange rate error response (400)
    /// </summary>
    public class ExchangeRateErrorResponse
    {
        /// <summary>
        /// Error code
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; } = string.Empty;

        /// <summary>
        /// Error message
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Supported currencies (optional, for unsupported_currency error)
        /// </summary>
        [JsonProperty("supported")]
        public List<string>? Supported { get; set; }
    }
}
