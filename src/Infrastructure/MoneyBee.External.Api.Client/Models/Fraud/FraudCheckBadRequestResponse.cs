namespace MoneyBee.External.Api.Client.Models.Fraud
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Fraud check bad request response (400)
    /// </summary>
    public class FraudCheckBadRequestResponse
    {
        /// <summary>
        /// Success flag (always false for error responses)
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; } = string.Empty;

        /// <summary>
        /// Error details
        /// </summary>
        [JsonProperty("details")]
        public List<object> Details { get; set; } = new();
    }
}
