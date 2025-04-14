using HMS.API.Abstraction.Enums;
using HMS.API.Abstraction.Exceptions;
using HMS.API.Abstraction.Interfaces.Billing;
using log4net;
using Microsoft.AspNetCore.Http;

namespace HMS.API.Services.Services.Billing
{
    public class BillingValidationService : IBillingValidationService
    {
        private readonly IBillingDataManager _dataManager;
        private readonly ILog _logger;

        public BillingValidationService(IBillingDataManager dataManager)
        {
            _dataManager = dataManager;
            _logger = LogManager.GetLogger(typeof(BillingValidationService));
        }

        public async Task ValidateBill(int billID)
        {
            _logger.Info($"ValidateBill called in ValidationService with ID: {billID}");
            var bill = await _dataManager.GetBillByID(billID);
            if (bill == null)
            {
                throw new BillingException("Bill Not Found", (int)ErrorCodes.BillNotFound, (int)StatusCodes.Status404NotFound);
            }
        }

        public async Task ValidateBillRequest(int patientId, decimal billAmount)
        {
            _logger.Info($"ValidateBillRequest called in ValidationService with Patient ID: {patientId} and Amount: {billAmount}");
            var currentBill = await _dataManager.GetBillingForPatientAndAmount(patientId, billAmount);
            if (currentBill != null)
            {
                throw new BillingException("Bill Already Exists", (int)ErrorCodes.BillAlreadyExists, (int)StatusCodes.Status400BadRequest);
            }
        }

        public async Task ValidatebillStatus(string billStatus)
        {
            _logger.Info($"ValidatebillStatus called in ValidationService with Status: {billStatus}");
            var status = await _dataManager.GetBillingStatus(billStatus);
            if (status == null)
            {
                throw new BillingException("Invalid Bill Status", (int)ErrorCodes.InvalidBillStatus, (int)StatusCodes.Status404NotFound);
            }
        }

        public void Dispose()
        {
            _dataManager.Dispose();
        }
    }
}