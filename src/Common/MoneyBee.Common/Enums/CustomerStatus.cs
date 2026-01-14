namespace MoneyBee.Common.Enums
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Customer status
    /// </summary>
    public enum CustomerStatus : byte
    {
        /// <summary>
        /// Active
        /// </summary>
        [Display(Name = "Aktif")]
        Active = 0,

        /// <summary>
        /// Passive
        /// </summary>
        [Display(Name = "Pasif")]
        Passive = 1,

        /// <summary>
        /// Blocked
        /// </summary>
        [Display(Name = "Blokeli")]
        Blocked = 2
    }
}
