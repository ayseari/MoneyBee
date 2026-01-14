namespace MoneyBee.Customer.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using MoneyBee.Customer.Domain.Entities;
    using MoneyBee.Customer.Domain.Interfaces;
    using MoneyBee.Customer.Domain.Repositories;
    using MoneyBee.Customer.Infrastructure.Persistence.Contexts;

    /// <summary>
    /// Customer Repository
    /// </summary>
    /// <param name="customerDbContext"></param>
    public class CustomerRepository(ICustomerDbContext customerDbContext, ICustomerUnitOfWork customerUnitOfWork) : ICustomerRepository
    {
        private readonly CustomerDbContext _customerDbContext = (CustomerDbContext)customerDbContext;
        private readonly DbSet<CustomerEntity> _dbSet = customerDbContext.Customers;

        /// <summary>
        /// Get by id
        /// </summary>
        public async Task<CustomerEntity?> GetById(Guid id) => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        /// <summary>
        /// Get by national id
        /// </summary>
        public async Task<CustomerEntity?> GetByNationalId(string nationalId) => await _dbSet.FirstOrDefaultAsync(x => x.NationalId == nationalId);

        /// <summary>
        /// Get by tax number
        /// </summary>
        public async Task<CustomerEntity?> GetByTaxNumber(string taxNumber) => await _dbSet.FirstOrDefaultAsync(x => x.TaxNumber == taxNumber);

        /// <summary>
        /// Add Apikey async
        /// </summary>
        public async Task AddAsync(CustomerEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        /// <summary>
        /// Update API key
        /// </summary>
        public async Task UpdateAsync(CustomerEntity entity)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Save
        /// </summary>
        public async Task SaveAsync()
        {
            await customerUnitOfWork.SaveAsync();
        }
    }
}
