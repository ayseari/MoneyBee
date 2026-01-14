using MoneyBee.Common.Enums;

namespace MoneyBee.Transfer.Application.Models
{
    /// <summary>
    /// Send Money Request
    /// </summary>
    public class SendMoneyRequest
    {
        public CustomerInfo SenderInfo { get; set; }
        public CustomerInfo ReceiverInfo { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string RequestedBy { get; set; }
    }
}
