namespace MoneyBee.Common.Enums
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Currency
    /// </summary>
    public enum Currency
    {
        [Display(Name = "Türk Lirası")]
        TRY = 0,

        [Display(Name = "Amerikan Doları")]
        USD = 1,

        [Display(Name = "Euro")]
        EUR = 2,

        [Display(Name = "Sterlin")]
        GBP = 3,

        [Display(Name = "Japon Yeni")]
        JPY = 4,

        [Display(Name = "İsviçre Frangı")]
        CHF = 5,

        [Display(Name = "Kanada Doları")]
        CAD = 6,

        [Display(Name = "Avustralya Doları")]
        AUD = 7,

        [Display(Name = "Çin Yuanı")]
        CNY = 8
    }
}