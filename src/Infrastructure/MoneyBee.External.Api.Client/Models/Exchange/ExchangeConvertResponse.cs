using Newtonsoft.Json;

namespace MoneyBee.External.Api.Client.Models.Exchange
{
    /// <summary>
    /// Exchange convert response (200 - Success)
    /// </summary>
    public class ExchangeConvertResponse
    {
        /// <summary>
        /// From currency
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; } = string.Empty;

        /// <summary>
        /// To currency
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; } = string.Empty;

        /// <summary>
        /// Original amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Converted amount
        /// </summary>
        [JsonProperty("converted")]
        public decimal Converted { get; set; }

        /// <summary>
        /// Exchange rate used
        /// </summary>
        [JsonProperty("rate")]
        public decimal Rate { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}
