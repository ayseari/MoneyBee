namespace MoneyBee.Customer.Domain.Entities
{
    using MoneyBee.Common.DomainModels;
    using MoneyBee.Common.Enums;

    /// <summary>
    /// Customer Entity
    /// </summary>
    public class CustomerEntity : Entity<Guid>
    {
        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// National Id
        /// </summary>
        public string NationalId { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Date Of Birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Tax Number
        /// </summary>
        public string TaxNumber { get; set; }

        /// <summary>
        /// Company Name
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Customer Type
        /// </summary>
        public CustomerType Type { get; set; } 

        /// <summary>
        /// Customer status
        /// </summary>
        public CustomerStatus Status { get; set; }

        /// <summary>
        /// Updated date
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
