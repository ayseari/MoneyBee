namespace MoneyBee.Transfer.Application.Services.Interfaces.External
{
    using MoneyBee.Common.Enums;
    using MoneyBee.Common.Models.Fraud;
    using MoneyBee.Common.Models.Result;
    using System.Threading.Tasks;

    /// <summary>
    /// Fraud Detection Service
    /// </summary>
    public interface IFraudDetectionService
    {
        /// <summary>
        /// FraudCheck
        /// </summary>
        Task<ServiceResult<(RiskLevel riskLevel, int riskScore)>> FraudCheck(FraudCheckRequest request);
    }
}
