using Newtonsoft.Json;

namespace MoneyBee.External.Api.Client.Models.Exchange
{
    /// <summary>
    /// Exchange convert error response (400)
    /// </summary>
    public class ExchangeConvertErrorResponse
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
    }
}
