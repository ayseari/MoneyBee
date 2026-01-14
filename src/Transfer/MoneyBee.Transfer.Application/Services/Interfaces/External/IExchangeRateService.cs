namespace MoneyBee.Transfer.Application.Services.Interfaces.External
{
    using MoneyBee.Common.Models.Result;
    using MoneyBee.Transfer.Application.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// Exchange Rate Service
    /// </summary>
    public interface IExchangeRateService
    {
        /// <summary>
        /// Exchange
        /// </summary>
        Task<ServiceResult<(decimal rate, decimal exchangedValue)>> Exchange(ExchangeRequest request);
    }
}