using HMS.API.Abstraction.Entities.Billing;
using HMS.API.Abstraction.Interfaces.Billing;
using log4net;

namespace HMS.API.Services.Services.Billing
{
    public class BillingService : IBillingService
    {
        private readonly IBillingDataManager _billingDataManager;
        private readonly IBillingValidationService _billingValidationService;
        private readonly ILog _logger;

        public BillingService(IBillingDataManager billingDataManager, IBillingValidationService billingValidationService)
        {
            _billingDataManager = billingDataManager;
            _billingValidationService = billingValidationService;
            _logger = LogManager.GetLogger(typeof(BillingService));
        }

        public async Task<List<BillingEntity>> GetAllBillings()
        {
            var allBills = await _billingDataManager.GetAllBillings();
            return allBills;
        }

        public async Task<BillingEntity> GetBillByID(int id)
        {
            _logger.Info($"GetBillByID called in Service with ID: {id}");
            await _billingValidationService.ValidateBill(id);
            var bill = await _billingDataManager.GetBillByID(id);
            _logger.Info("GetBillByID from Service returned");
            return bill;
        }

        public async Task CreateBill(BillingRequest billingRequest)
        {
            _logger.Info($"CreateBill from Service called with [request={billingRequest.PatientId}]");
            await _billingValidationService.ValidateBillRequest(billingRequest.PatientId, billingRequest.Amount);
            await _billingValidationService.ValidatebillStatus(billingRequest.BillingStatus);
            await _billingDataManager.CreateBill(billingRequest);
            _logger.Info("CreateBill from Service returned");
        }

        public async Task UpdateBill(int id, BillingUpdate billingUpdate)
        {
            _logger.Info($"UpdateBill from Service called with [id={id}]");
            await _billingValidationService.ValidateBill(id);
            await _billingValidationService.ValidatebillStatus(billingUpdate.BillingStatus);
            await _billingDataManager.UpdateBill(id, billingUpdate);
            _logger.Info("UpdateBill from Service returned");
        }

        public async Task DeleteBill(int id)
        {
            _logger.Info($"DeleteBill from Service called with [id={id}]");
            await _billingValidationService.ValidateBill(id);
            await _billingDataManager.DeleteBill(id);
            _logger.Info("DeleteBill from Service returned");
        }

        public void Dispose()
        {
            _billingDataManager.Dispose();
            _billingValidationService.Dispose();
        }
    }
}