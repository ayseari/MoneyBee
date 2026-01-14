namespace MoneyBee.Customer.Application.Interfaces.External
{
    using MoneyBee.Common.Models.Customer;
    using MoneyBee.Common.Models.Result;
    using System.Threading.Tasks;

    public interface IKycService
    {
        /// <summary>
        /// VerifyKycAsync
        /// </summary>
        Task<ServiceResult<bool>> VerifyKycAsync(CreateCustomerRequest request, Guid customerId);
    }
}
