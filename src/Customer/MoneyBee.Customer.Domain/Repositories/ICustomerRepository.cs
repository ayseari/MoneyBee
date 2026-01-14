namespace MoneyBee.Customer.Domain.Repositories
{
    using System;
    using System.Threading.Tasks;
    using MoneyBee.Customer.Domain.Entities;

    /// <summary>
    /// Customer repository
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Get by id
        /// </summary>
        Task<CustomerEntity> GetById(Guid id);

        /// <summary>
        /// Get by national id
        /// </summary>
        Task<CustomerEntity> GetByNationalId(string nationalId);

        /// <summary>
        /// Get by tax number
        /// </summary>
        Task<CustomerEntity> GetByTaxNumber(string taxNumber);

        /// <summary>
        /// Add Customer
        /// </summary>
        Task AddAsync(CustomerEntity entity);

        /// <summary>
        /// Update Customer
        /// </summary>
        Task UpdateAsync(CustomerEntity entity);

        /// <summary>
        /// Save
        /// </summary>
        Task SaveAsync();
    }
}