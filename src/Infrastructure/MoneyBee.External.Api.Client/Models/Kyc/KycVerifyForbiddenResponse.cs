namespace MoneyBee.External.Api.Client.Models.Kyc
{
    /// <summary>
    /// KYC Verification Forbidden Response (403)
    /// Forbidden - Age restriction
    /// </summary>
    public class KycVerifyForbiddenResponse : KycVerifyErrorResponse
    {
        /// <summary>
        /// User age
        /// </summary>
        public int Age { get; set; }        
    }
}