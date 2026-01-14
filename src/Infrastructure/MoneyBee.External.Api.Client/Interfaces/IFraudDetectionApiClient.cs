namespace MoneyBee.External.Api.Client.Interfaces
{
    using MoneyBee.Common.Models.Fraud;
    using MoneyBee.External.Api.Client.Models.Fraud;
    using Refit;

    /// <summary>
    /// Fraud Detection API Client
    /// </summary>
    public interface IFraudDetectionApiClient
    {
        /// <summary>
        /// Check transaction for fraud
        /// </summary>
        /// <param name="request">Fraud check request</param>
        /// <returns>Fraud check response</returns>
        [Post("/api/fraud/check")]
        Task<ApiResponse<FraudCheckResponse>> CheckAsync([Body] FraudCheckRequest request);

        /// <summary>
        /// Get fraud statistics
        /// </summary>
        /// <returns>Fraud statistics response</returns>
        [Get("/api/fraud/statistics")]
        Task<ApiResponse<FraudStatisticsResponse>> GetStatisticsAsync();
    }
}
