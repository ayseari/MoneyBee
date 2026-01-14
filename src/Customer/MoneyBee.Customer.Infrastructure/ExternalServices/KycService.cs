namespace MoneyBee.Customer.Infrastructure.ExternalServices
{
    using Microsoft.Extensions.Logging;
    using MoneyBee.Common.Models.Customer;
    using MoneyBee.Common.Models.Result;
    using MoneyBee.Customer.Application.Interfaces.External;
    using MoneyBee.External.Api.Client.Interfaces;
    using MoneyBee.External.Api.Client.Models.Kyc;
    using Newtonsoft.Json;

    public class KycService(IKycVerificationApiClient kycVerificationApiClient,
        ILogger<KycService> logger) : IKycService
    {
        /// <summary>
        /// VerifyKycAsync
        /// </summary>
        public async Task<ServiceResult<bool>> VerifyKycAsync(CreateCustomerRequest request, Guid customerId)
        {
            try
            {
                var kycRequest = new KycVerifyRequest
                {
                    Name = request.FirstName,
                    Surname = request.LastName,
                    TcNo = request.NationalId,
                    BirthYear = request.DateOfBirth.Year,
                    UserId = customerId
                };

                var apiResponse = await kycVerificationApiClient.VerifyAsync(kycRequest);
                
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = apiResponse.Content;
                    return ServiceResult.Success(response?.Verified ?? false);
                }
                else if(apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var response = JsonConvert.DeserializeObject<KycVerifyBadRequestResponse>(apiResponse.Error.Content);

                    logger.LogWarning($"VerifyKycAsync failed. Status Code: {apiResponse.StatusCode}, Response: {JsonConvert.SerializeObject(response)}", apiResponse.Error);
                    
                    return ServiceResult.Failure<bool>($"KYC doğrulaması başarısız. Reason: {response.Reason}, Message: {response.Message}");

                }
                else if (apiResponse.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    var response = JsonConvert.DeserializeObject<KycVerifyForbiddenResponse>(apiResponse.Error.Content);

                    logger.LogWarning($"VerifyKycAsync failed. Status Code: {apiResponse.StatusCode}, Response: {JsonConvert.SerializeObject(response)}", apiResponse.Error);
                    
                    return ServiceResult.Failure<bool>($"VerifyKycAsync failed. Reason: {response.Reason}, Message: {response.Message}");
                }
                else
                {
                    var response = JsonConvert.DeserializeObject<KycVerifyErrorResponse>(apiResponse.Error.Content);

                    logger.LogWarning($"VerifyKycAsync failed. Status Code: {apiResponse.StatusCode}, Response: {JsonConvert.SerializeObject(response)}", apiResponse.Error);

                    return ServiceResult.Failure<bool>($"KYC verification failed. Reason: {response.Reason}, Message: {response.Message}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "KYC verification API call failed");
                return ServiceResult.Failure<bool>("KYC verification API call failed");
            }
        }
    }
}
