namespace MoneyBee.External.Api.Client.Models.Kyc
{
    using Newtonsoft.Json;

    /// <summary>
    /// KYC Verification Status Not Found Response (404)
    /// </summary>
    public class KycVerifyStatusNotFoundResponse
    {
        /// <summary>
        /// Success flag (always false for error responses)
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;
    }
}
