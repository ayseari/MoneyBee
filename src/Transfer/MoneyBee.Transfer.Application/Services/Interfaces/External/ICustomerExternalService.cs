namespace MoneyBee.Transfer.Application.Services.Interfaces.External
{
    using MoneyBee.Common.Models.Customer;
    using MoneyBee.Common.Models.Result;
    using MoneyBee.Transfer.Application.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// Customer External Service
    /// </summary>
    public interface ICustomerExternalService
    {
        /// <summary>
        /// GetOrCreateCustomerAsync
        /// </summary>
        Task<ServiceResult<CustomerDto>> GetOrCreateCustomerAsync(CustomerInfo customerInfo);

        /// <summary>
        /// GetCustomerAsync
        /// </summary>
        Task<ServiceResult<CustomerDto>> GetCustomerAsync(CustomerInfo customerInfo);
    }
}
