namespace MoneyBee.Common.Enums
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Customer type
    /// </summary>
    public enum CustomerType : byte
    {
        /// <summary>
        /// Individual
        /// </summary>
        [Display(Name = "Bireysel")]
        Individual = 0,

        /// <summary>
        /// Corporate
        /// </summary>
        [Display(Name = "Kurumsal")]
        Corporate = 1
    }
}