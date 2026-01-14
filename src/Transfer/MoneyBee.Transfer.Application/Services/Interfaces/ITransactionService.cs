namespace MoneyBee.Transfer.Application.Services.Interfaces
{
    using MoneyBee.Common.Models.Result;
    using MoneyBee.Transfer.Application.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// Transaction Service
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// SendMoneyAsync
        /// </summary>
        Task<ServiceResult<SendMoneyResponse>> SendMoneyAsync(SendMoneyRequest request);

        /// <summary>
        /// IsTransactionCodeExist
        /// </summary>
        Task<bool> IsTransactionCodeExist(string transactionCode);

        /// <summary>
        /// ReceiveMoneyAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> ReceiveMoneyAsync(ReceiveMoneyRequest request);

        /// <summary>
        /// Cancel Transaction
        /// </summary>
        /// <param name="transactionCode"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> CancelTransactionAsync(string transactionCode);
    }
}
