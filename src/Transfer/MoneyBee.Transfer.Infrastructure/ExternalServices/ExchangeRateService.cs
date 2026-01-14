namespace MoneyBee.Transfer.Infrastructure.ExternalServices
{
    using Microsoft.Extensions.Logging;
    using MoneyBee.Common.Models.Result;
    using MoneyBee.External.Api.Client.Interfaces;
    using MoneyBee.External.Api.Client.Models.Exchange;
    using MoneyBee.Transfer.Application.Models;
    using MoneyBee.Transfer.Application.Services.Interfaces.External;
    using Newtonsoft.Json;

    /// <summary>
    /// ExchangeRateService
    /// </summary>
    public class ExchangeRateService(IExchangeRateApiClient exchangeRateApiClient, ILogger<ExchangeRateService> logger) : IExchangeRateService
    {
        /// <summary>
        /// Exchange
        /// </summary>
        public async Task<ServiceResult<(decimal rate, decimal exchangedValue)>> Exchange(ExchangeRequest request)
        {
            try
            {
                var apiResponse = await exchangeRateApiClient.ConvertAsync(request.FromCurreny, request.ToCurreny, request.Amount);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = apiResponse.Content;
                    return ServiceResult.Success((response.Rate, response.Converted));
                }
                else
                {
                    var response = JsonConvert.DeserializeObject<ExchangeConvertErrorResponse>(apiResponse.Error.Content);

                    logger.LogWarning($"Exchange ConvertAsync failed. Status Code: {apiResponse.StatusCode}, Response: {JsonConvert.SerializeObject(response)}", apiResponse.Error);

                    return ServiceResult.Failure<(decimal rate, decimal exchangedValue)>($"Exchange ConvertAsync failed. Reason: {response.Error}, Message: {response.Message}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exchange rate API call failed");
                return ServiceResult.Failure<(decimal rate, decimal exchangedValue)>("Exchange rate API call failed");
            }
        }
    }
}
