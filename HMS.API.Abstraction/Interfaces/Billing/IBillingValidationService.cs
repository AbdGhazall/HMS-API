namespace HMS.API.Abstraction.Interfaces.Billing
{
    public interface IBillingValidationService : IDisposable
    {
        Task ValidateBill(int billID);

        Task ValidateBillRequest(int patientId, decimal billAmount);

        Task ValidatebillStatus(string billStatus);
    }
}