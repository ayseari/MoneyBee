namespace MoneyBee.External.Api.Client.Interfaces
{
    using MoneyBee.External.Api.Client.Models.Kyc;
    using Refit;

    /// <summary>
    /// Represents an interface for interacting with the BPN Kyc Verification API.
    /// </summary>
    public interface IKycVerificationApiClient
    {
        /// <summary>
        /// Verify KYC information
        /// </summary>
        /// <param name="request">KYC verification request</param>
        /// <returns>KYC verification response</returns>
        [Post("/api/kyc/verify")]
        Task<ApiResponse<KycVerifyResponse>> VerifyAsync([Body] KycVerifyRequest request);

        /// <summary>
        /// Get KYC verification status by user ID
        /// </summary>
        /// <param name="userId">User unique identifier</param>
        /// <returns>KYC verification status response</returns>
        [Get("/api/kyc/status/{userId}")]
        Task<ApiResponse<KycVerifyStatusResponse>> GetStatusAsync(Guid userId);

        /// <summary>
        /// Get KYC statistics
        /// </summary>
        /// <returns>KYC statistics response</returns>
        [Get("/api/kyc/statistics")]
        Task<ApiResponse<KycVerifyStatisticsResponse>> GetStatisticsAsync();
    }
}
