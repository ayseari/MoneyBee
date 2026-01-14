using Newtonsoft.Json;
using System.Collections.Generic;

namespace MoneyBee.External.Api.Client.Models.Exchange
{
    /// <summary>
    /// Exchange currencies response (200 - Success)
    /// </summary>
    public class ExchangeCurrenciesResponse
    {
        /// <summary>
        /// List of supported currencies
        /// </summary>
        [JsonProperty("currencies")]
        public List<string> Currencies { get; set; } = new();

        /// <summary>
        /// Total count of currencies
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}
