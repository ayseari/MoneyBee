namespace MoneyBee.Transfer.Domain.Entities
{
    using MoneyBee.Common.DomainModels;
    using MoneyBee.Common.Enums;
    using MoneyBee.Transfer.Domain.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Represents a money transfer transaction between two customers.
    /// </summary>
    public class TransactionEntity : Entity<Guid>
    {
        public TransactionEntity()
        {
            StatusLogs = new List<TransactionStatusLogEntity>();
        }

        /// <summary>
        /// Identifier of the sender customer.
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        /// Identifier of the receiver customer.
        /// </summary>
        public Guid ReceiverId { get; set; }

        /// <summary>
        /// Unique business transaction code.
        /// </summary>
        [MaxLength(20)]
        public string TransactionCode { get; set; }

        /// <summary>
        /// Current status of the transaction.
        /// </summary>
        public TransactionStatus Status { get; set; }

        /// <summary>
        /// Transaction amount in original currency.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Transaction fee amount.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Fee { get; set; }

        /// <summary>
        /// Currency of the transaction.
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// Transaction amount converted to TRY.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountInTRY { get; set; }

        /// <summary>
        /// Exchange rate used for currency conversion.
        /// </summary>
        public decimal? ExchangeRate { get; set; }

        /// <summary>
        /// Risk level assigned by fraud detection.
        /// </summary>
        [Required]
        public RiskLevel RiskLevel { get; set; }

        /// <summary>
        /// Calculated fraud risk score.
        /// </summary>
        [Required]
        public int RiskScore { get; set; }

        /// <summary>
        /// Indicates whether the transaction fee has been refunded.
        /// </summary>
        public bool FeeRefunded { get; set; }

        /// <summary>
        /// Status Logs
        /// </summary>
        public ICollection<TransactionStatusLogEntity> StatusLogs { get; set; }

        /// <summary>
        /// Add status log 
        /// </summary>
        public void AddStatusLog(string changedBy, string changeReason = null)
        {
            var statusLog = new TransactionStatusLogEntity(changedBy, Status, changeReason?.Length > 250 ? changeReason?.Substring(0, 250) : changeReason);
            StatusLogs.Add(statusLog);
        }
    }
}
