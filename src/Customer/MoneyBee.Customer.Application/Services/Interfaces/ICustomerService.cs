namespace MoneyBee.Customer.Application.Services.Interfaces
{
    using MoneyBee.Common.Models.Customer;
    using MoneyBee.Common.Models.Result;
    using System;
    using System.Threading.Tasks;

    public interface ICustomerService
    {
        /// <summary>
        /// Create Customer
        /// </summary>
        Task<ServiceResult<CustomerDto>> CreateCustomerAsync(CreateCustomerRequest request);

        /// <summary>
        /// GetCustomerByNationalId
        /// </summary>
        Task<ServiceResult<CustomerDto>> GetCustomerByNationalId(string nationalId);

        /// <summary>
        /// GetCustomerByTaxNumber
        /// </summary>
        Task<ServiceResult<CustomerDto>> GetCustomerByTaxNumber(string taxNumber);

        /// <summary>
        /// Update Customer
        /// </summary>
        Task<ServiceResult<CustomerDto>> UpdateCustomerAsync(UpdateCustomerRequest request);

    }
}
