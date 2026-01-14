namespace MoneyBee.Customer.Api.Client.Interfaces
{
    using MoneyBee.Common.Models.Customer;
    using MoneyBee.Common.Models.Result;
    using Refit;

    /// <summary>
    /// Customer API Client
    /// </summary>
    public interface ICustomerApiClient
    {
        /// <summary>
        /// Create customer
        /// </summary>
        [Post("/customer")]
        Task<ServiceResult<CustomerDto>> Create([Body] CreateCustomerRequest request);

        [Get("/customer/by-national-id/{nationalId}")]
        Task<ServiceResult<CustomerDto>> GetCustomerByNationalId(string nationalId);

        [Get("/customer/by-tax-number/{taxNumber}")]
        Task<ServiceResult<CustomerDto>> GetCustomerByTaxNumber(string taxNumber);
    }
}
