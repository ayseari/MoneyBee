using Newtonsoft.Json;

namespace MoneyBee.External.Api.Client.Models.Exchange
{
    /// <summary>
    /// Exchange rate response (200 - Success)
    /// </summary>
    public class ExchangeRateResponse
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
        /// Exchange rate
        /// </summary>
        [JsonProperty("rate")]
        public decimal Rate { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        /// <summary>
        /// Valid for seconds
        /// </summary>
        [JsonProperty("validForSeconds")]
        public int ValidForSeconds { get; set; }
    }
}
