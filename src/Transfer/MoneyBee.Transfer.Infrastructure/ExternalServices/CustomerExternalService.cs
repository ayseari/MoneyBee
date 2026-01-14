namespace MoneyBee.Transfer.Infrastructure.ExternalServices
{
    using Mapster;
    using MoneyBee.Common.Models.Customer;
    using MoneyBee.Common.Models.Result;
    using MoneyBee.Customer.Api.Client.Interfaces;
    using MoneyBee.Transfer.Application.Models;
    using MoneyBee.Transfer.Application.Services.Interfaces.External;

    /// <summary>
    /// Customer Service
    /// </summary>
    public class CustomerExternalService(ICustomerApiClient customerApiClient) : ICustomerExternalService
    {
        /// <summary>
        /// GetOrCreateCustomerAsync
        /// </summary>
        public async Task<ServiceResult<CustomerDto>> GetOrCreateCustomerAsync(CustomerInfo customerInfo)
        {
            var customerResult = customerInfo.Type == Common.Enums.CustomerType.Individual ?
                await customerApiClient.GetCustomerByNationalId(customerInfo.NationalId) :
                await customerApiClient.GetCustomerByTaxNumber(customerInfo.TaxNumber);

            if (!customerResult.IsSuccess)
            {
                if(customerResult.ErrorMessage =="Customer not found")
                {
                    return await customerApiClient.Create(customerInfo.Adapt<CreateCustomerRequest>());
                }
                return ServiceResult.Failure<CustomerDto>(customerResult.ErrorMessage);
            }

            return ServiceResult.Success(customerResult.Data);
        }

        /// <summary>
        /// GetCustomerAsync
        /// </summary>
        public async Task<ServiceResult<CustomerDto>> GetCustomerAsync(CustomerInfo customerInfo)
        {
            var customerResult = customerInfo.Type == Common.Enums.CustomerType.Individual ?
                await customerApiClient.GetCustomerByNationalId(customerInfo.NationalId) :
                await customerApiClient.GetCustomerByTaxNumber(customerInfo.TaxNumber);

            if (!customerResult.IsSuccess)
            {
                return ServiceResult.Failure<CustomerDto>(customerResult.ErrorMessage);
            }

            return ServiceResult.Success(customerResult.Data);
        }
    }
}
