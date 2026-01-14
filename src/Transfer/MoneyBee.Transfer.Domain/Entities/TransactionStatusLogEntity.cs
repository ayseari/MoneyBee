namespace MoneyBee.Transfer.Domain.Entities
{
    using MoneyBee.Common.DomainModels;
    using MoneyBee.Transfer.Domain.Enums;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Transaction Status Log Entity
    /// </summary>
    public class TransactionStatusLogEntity : Entity<long>
    {
        private TransactionStatusLogEntity()
        {
        }
        public TransactionStatusLogEntity(string changedBy, TransactionStatus status, string changeReason)
        {
            ChangedBy = changedBy;
            Status = status;
            ChangeReason = changeReason;
        }

        /// <summary>
        /// Performed
        /// </summary>
        [MaxLength(36)]
        public string ChangedBy { get; private set; }

        /// <summary>
        /// Transaction 
        /// </summary>
        public TransactionEntity Transaction { get; private set; }

        /// <summary>
        /// Transaction Id
        /// </summary>
        public Guid TransactionId { get; private set; }

        /// <summary>
        /// Transaction status
        /// </summary>
        public TransactionStatus Status { get; private set; }

        /// <summary>
        /// Change Reason
        /// </summary>
        [MaxLength(250)]
        public string ChangeReason { get; private set; }
    }
}
