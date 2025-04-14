using HMS.API.Abstraction.Entities.Billing;

namespace HMS.API.Abstraction.Interfaces.Billing
{
    public interface IBillingDataManager : IDisposable
    {
        Task<List<BillingEntity>> GetAllBillings();

        Task<BillingEntity> GetBillByID(int id);

        Task CreateBill(BillingRequest billingRequest);

        Task UpdateBill(int id, BillingUpdate billingUpdate);

        Task DeleteBill(int id);

        Task<string> GetBillingStatus(string billingStatus);

        Task<BillingEntity> GetBillingForPatientAndAmount(int patientId, decimal amount);
    }
}