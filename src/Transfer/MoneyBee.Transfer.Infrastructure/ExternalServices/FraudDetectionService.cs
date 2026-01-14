namespace MoneyBee.Transfer.Infrastructure.ExternalServices
{
    using Microsoft.Extensions.Logging;
    using MoneyBee.Common.Enums;
    using MoneyBee.Common.Models.Fraud;
    using MoneyBee.Common.Models.Result;
    using MoneyBee.External.Api.Client.Interfaces;
    using MoneyBee.External.Api.Client.Models.Fraud;
    using MoneyBee.Transfer.Application.Services.Interfaces.External;
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// FraudDetectionService
    /// </summary>
    public class FraudDetectionService(IFraudDetectionApiClient fraudDetectionApiClient, ILogger<FraudDetectionService> logger) : IFraudDetectionService
    {
        /// <summary>
        /// FraudCheck
        /// </summary>
        public async Task<ServiceResult<(RiskLevel riskLevel, int riskScore)>> FraudCheck(FraudCheckRequest request)
        {
            try
            {
                var apiResponse = await fraudDetectionApiClient.CheckAsync(request);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = apiResponse.Content;
                    if (!response.Success)
                    {
                        return ServiceResult.Failure<(RiskLevel riskLevel, int riskScore)>($"Fraud check failed. Response: {JsonConvert.SerializeObject(response)}");
                    }
                    return ServiceResult.Success((response.Data.RiskLevel, response.Data.RiskScore));
                }
                else
                {
                    var response = JsonConvert.DeserializeObject<FraudCheckBadRequestResponse>(apiResponse.Error.Content);

                    logger.LogWarning($"Fraud check failed. Error: {apiResponse.StatusCode}, Response: {JsonConvert.SerializeObject(response)}");

                    return ServiceResult.Failure<(RiskLevel riskLevel, int riskScore)>($"Fraud check failed. Reason: {response.Error}, Message: {JsonConvert.SerializeObject(response.Details)}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fraud Detection API call failed");
                return ServiceResult.Failure<(RiskLevel riskLevel, int riskScore)>("Fraud Detection API call failed");
            }
        }
    }
}
