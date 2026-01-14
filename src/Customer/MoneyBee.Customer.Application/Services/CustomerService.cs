namespace MoneyBee.Customer.Application.Services
{
    using Mapster;
    using Microsoft.Extensions.Logging;
    using MoneyBee.Common.Enums;
    using MoneyBee.Common.Models.Customer;
    using MoneyBee.Common.Models.Result;
    using MoneyBee.Common.Utilities;
    using MoneyBee.Common.Validations;
    using MoneyBee.Common.Validations.Rules;
    using MoneyBee.Customer.Application.Interfaces.External;
    using MoneyBee.Customer.Application.Services.Interfaces;
    using MoneyBee.Customer.Application.Validators;
    using MoneyBee.Customer.Domain.Entities;
    using MoneyBee.Customer.Domain.Repositories;
    using Newtonsoft.Json;

    public class CustomerService(ICustomerRepository customerRepository,
    IKycService kycService,
    IValidatorFactory validatorFactory,
    ILogger<CustomerService> logger) : ICustomerService
    {
        /// <summary>
        /// Create Customer
        /// </summary>
        public async Task<ServiceResult<CustomerDto>> CreateCustomerAsync(CreateCustomerRequest request)
        {
            request.PhoneNumber = PhoneNumberNormalizeHelper.Normalize(request.PhoneNumber);

            var validationResult = ValidateCustomer(request);
            if (!validationResult.IsSuccess)
            {
                logger.LogWarning("Customer validation failed: {Error}", validationResult.ErrorMessage);
                return ServiceResult.Failure<CustomerDto>(validationResult.ErrorMessage);
            }

            var customerId = Guid.NewGuid();

            // KYC doğrulaması
            var kycResult = await kycService.VerifyKycAsync(request, customerId);
            if (!kycResult.IsSuccess && !kycResult.Data)
            {
                logger.LogWarning("KYC verification failed for customer: {Error}", kycResult.ErrorMessage);
                return ServiceResult.Failure<CustomerDto>(kycResult.ErrorMessage);
            }

            // Müşteri entity oluştur
            var customer = CreateCustomerEntity(request, customerId);

            await customerRepository.AddAsync(customer);
            await customerRepository.SaveAsync();

            logger.LogInformation("Customer created successfully with Id: {CustomerId}", customer.Id);

            return ServiceResult.Success(customer.Adapt<CustomerDto>());
        }

        /// <summary>
        /// GetCustomerByNationalId
        /// </summary>
        public async Task<ServiceResult<CustomerDto>> GetCustomerByNationalId(string nationalId)
        {
            var customer = await customerRepository.GetByNationalId(nationalId);

            if (customer == null)
            {
                logger.LogWarning("Customer not found with NationalId: {NationalId}", nationalId);
                return ServiceResult.Failure<CustomerDto>("Customer not found ");
            }

            return ServiceResult.Success(customer.Adapt<CustomerDto>());
        }

        /// <summary>
        /// GetCustomerByTaxNumber
        /// </summary>
        public async Task<ServiceResult<CustomerDto>> GetCustomerByTaxNumber(string taxNumber)
        {
            var customer = await customerRepository.GetByTaxNumber(taxNumber);

            if (customer == null)
            {
                logger.LogWarning("Customer not found with TaxNumber: {TaxNumber}", taxNumber);
                return ServiceResult.Failure<CustomerDto>("Customer not found ");
            }

            return ServiceResult.Success(customer.Adapt<CustomerDto>());
        }

        /// <summary>
        /// Update Customer
        /// </summary>
        public async Task<ServiceResult<CustomerDto>> UpdateCustomerAsync(UpdateCustomerRequest request)
        {
            var customer = await customerRepository.GetById(request.CustomerId);

            if (customer == null)
            {
                logger.LogWarning("Customer not found with Id: {CustomerId}", request.CustomerId);
                return ServiceResult.Failure<CustomerDto>("Customer not found");
            }

            request.PhoneNumber = PhoneNumberNormalizeHelper.Normalize(request.PhoneNumber);

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber) &&
                request.PhoneNumber != customer.PhoneNumber)
            {
                var validator = new BusinessRuleValidator();
                validator.AddRule(new PhoneNumberMustBeValidRule(request.PhoneNumber));

                var validationResult = validator.Validate();
                if (!validationResult.IsValid)
                {
                    return ServiceResult.Failure<CustomerDto>(JsonConvert.SerializeObject(validationResult.Errors));
                }
                customer.PhoneNumber = request.PhoneNumber;
            }

            if (!string.IsNullOrWhiteSpace(request.FirstName))
                customer.FirstName = request.FirstName;

            if (!string.IsNullOrWhiteSpace(request.LastName))
                customer.LastName = request.LastName;

            if (request.Type.HasValue && customer.Type != request.Type)
                customer.Type = request.Type.Value;

            if (customer.Type == CustomerType.Corporate &&
                    !string.IsNullOrWhiteSpace(request.CompanyName))
                customer.CompanyName = request.CompanyName;

            if (request.Status.HasValue && customer.Status != request.Status)
                customer.Status = request.Status.Value;

            if (customer.Status == CustomerStatus.Blocked)
            {
                //event publish et
            }

            customer.UpdatedAt = DateTime.UtcNow;

            await customerRepository.UpdateAsync(customer);
            await customerRepository.SaveAsync();

            logger.LogInformation("Customer updated successfully with Id: {CustomerId}", request.CustomerId);

            return ServiceResult.Success(customer.Adapt<CustomerDto>());
        }

        #region Private Methods
        /// <summary>
        /// Validate Customer
        /// </summary>
        private ServiceResult ValidateCustomer(CreateCustomerRequest request)
        {
            var validator = validatorFactory.Create(new CustomerValidator(
                request.NationalId,
                request.TaxNumber,
                request.PhoneNumber,
                request.DateOfBirth,
                request.Type));

            var validationResult = validator.Validate();
            if (!validationResult.IsValid)
            {
                return ServiceResult.Failure(JsonConvert.SerializeObject(validationResult.Errors));
            }

            //TODO: Rule yap
            // Kurumsal müşteri için vergi numarası zorunludur
            if (request.Type == CustomerType.Corporate)
            {

                if (string.IsNullOrWhiteSpace(request.CompanyName))
                {
                    return ServiceResult.Failure("Kurumsal müşteriler için şirket adı zorunludur");
                }
            }

            return ServiceResult.Success();
        }

        /// <summary>
        /// CreateCustomerRequest'ten CustomerEntity oluşturur
        /// </summary>
        private CustomerEntity CreateCustomerEntity(CreateCustomerRequest request, Guid customerId)
        {
            var entity = request.Adapt<CustomerEntity>();

            entity.Id = customerId;
            entity.Status = CustomerStatus.Active;
            entity.CreatedAt = DateTime.UtcNow;

            return entity;
        }
        #endregion
    }
}