namespace MoneyBee.External.Api.Client.Models.Kyc
{
    /// <summary>
    /// Base KYC Verification Error Response
    /// </summary>
    public class KycVerifyErrorResponse
    {
        /// <summary>
        /// Success flag (always false for error responses)
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Verification result (always false for error responses)
        /// </summary>
        public bool Verified { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Error reason
        /// </summary>
        public string Reason { get; set; } = string.Empty;
    }
}
