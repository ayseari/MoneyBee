namespace MoneyBee.Common.Services.Interfaces
{
    //TODO: silinecek
    public interface IPhoneNumberValidationService
    {
        bool IsValid(string phoneNumber);
        string Normalize(string phoneNumber);
    }
}
