using HMS.API.Abstraction.Entities.Billing;

namespace HMS.API.Abstraction.Interfaces.Billing
{
    public interface IBillingService : IDisposable
    {
        Task<List<BillingEntity>> GetAllBillings();

        Task<BillingEntity> GetBillByID(int id);

        Task CreateBill(BillingRequest billingRequest);

        Task UpdateBill(int id, BillingUpdate billingUpdate);

        Task DeleteBill(int id);
    }
}