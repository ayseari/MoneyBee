namespace MoneyBee.External.Api.Client.Interfaces
{
    using MoneyBee.External.Api.Client.Models.Exchange;
    using Refit;

    /// <summary>
    /// Exchange Rate API Client
    /// </summary>
    public interface IExchangeRateApiClient
    {
        /// <summary>
        /// Get exchange rate between two currencies
        /// </summary>
        /// <param name="from">From currency code</param>
        /// <param name="to">To currency code</param>
        /// <returns>Exchange rate response</returns>
        [Get("/api/exchange/rate/{from}/{to}")]
        Task<ApiResponse<ExchangeRateResponse>> GetRateAsync(string from, string to);

        /// <summary>
        /// Convert amount from one currency to another
        /// </summary>
        /// <param name="from">From currency code</param>
        /// <param name="to">To currency code</param>
        /// <param name="amount">Amount to convert</param>
        /// <returns>Exchange convert response</returns>
        [Get("/api/exchange/convert")]
        Task<ApiResponse<ExchangeConvertResponse>> ConvertAsync(
            [AliasAs("from")] string from,
            [AliasAs("to")] string to,
            [AliasAs("amount")] decimal amount);

        /// <summary>
        /// Get list of supported currencies
        /// </summary>
        /// <returns>Supported currencies response</returns>
        [Get("/api/exchange/currencies")]
        Task<ApiResponse<ExchangeCurrenciesResponse>> GetCurrenciesAsync();
    }
}
