namespace MoneyBee.Transfer.Application.Models
{
    /// <summary>
    /// ReceiveMoneyRequest
    /// </summary>
    public class ReceiveMoneyRequest
    {
        public string TransactionCode { get; set; }
        public CustomerInfo IdentityInfo { get; set; }
        public string CompletedByUser { get; set; }
    }
}
