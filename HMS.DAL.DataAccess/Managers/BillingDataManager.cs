using HMS.API.Abstraction.Entities.Billing;
using HMS.API.Abstraction.Interfaces.Billing;
using HMS.DAL.Models.Models;
using log4net;
using Microsoft.EntityFrameworkCore;

namespace HMS.DAL.DataAccess.Managers
{
    public class BillingDataManager : IBillingDataManager
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILog _logger;

        public BillingDataManager(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _logger = LogManager.GetLogger(typeof(BillingDataManager));
        }

        public async Task<List<BillingEntity>> GetAllBillings()
        {
            _logger.Info("GetAllBillings called in DataManager");
            var bills = await _applicationDbContext.Billings
                .AsNoTracking()
                .Select(b => new BillingEntity
                {
                    Id = b.Id,
                    PatientId = b.PatientId,
                    BillingStatusId = b.BillingStatusId,
                    Amount = b.Amount,
                    InvoiceDate = b.InvoiceDate
                }).ToListAsync();
            _logger.Info("GetAllBillings returned in DataManager");
            return bills;
        }

        public async Task<BillingEntity> GetBillByID(int id)
        {
            _logger.Info($"GetBillByID called in DataManager [Id={id}]");
            var bill = await _applicationDbContext.Billings
                .AsNoTracking()
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();

            if (bill != null)
            {
                var billEntity = new BillingEntity()
                {
                    Id = bill.Id,
                    PatientId = bill.PatientId,
                    BillingStatusId = bill.BillingStatusId,
                    Amount = bill.Amount,
                    InvoiceDate = bill.InvoiceDate
                };
                _logger.Info("GetBillByID returned in DataManager");
                return billEntity;
            }
            return null;
        }

        public async Task CreateBill(BillingRequest billingRequest)
        {
            _logger.Info($"CreateBill called in DataManager [PatientId={billingRequest.PatientId}]");
            var billStatus = await _applicationDbContext.BillingStatuses
                .Where(bs => bs.Status == billingRequest.BillingStatus)
                .FirstOrDefaultAsync();
            if (billStatus != null)
            {
                _applicationDbContext.Billings.Add(new Billing()
                {
                    PatientId = billingRequest.PatientId,
                    Status = billStatus,
                    Amount = billingRequest.Amount,
                    InvoiceDate = billingRequest.InvoiceDate
                });
                await _applicationDbContext.SaveChangesAsync();
                _logger.Info("CreateBill returned in DataManager");
            }
        }

        public async Task UpdateBill(int id, BillingUpdate billingUpdate)
        {
            _logger.Info($"UpdateBill called in DataManager [Id={id}]");
            var bill = await _applicationDbContext.Billings.FindAsync(id);
            if (bill != null)
            {
                var billStatus = await _applicationDbContext.BillingStatuses
                    .Where(bs => bs.Status == billingUpdate.BillingStatus)
                    .FirstOrDefaultAsync();
                if (billStatus != null)
                {
                    bill.Status = billStatus;
                    bill.Amount = billingUpdate.Amount;
                }
                await _applicationDbContext.SaveChangesAsync();
                _logger.Info("UpdateBill returned in DataManager");
            }
        }

        public async Task DeleteBill(int id)
        {
            _logger.Info($"DeleteBill called in DataManager [Id={id}]");
            var bill = await _applicationDbContext.Billings.FindAsync(id);
            _applicationDbContext.Billings.Remove(bill);
            await _applicationDbContext.SaveChangesAsync();
            _logger.Info("DeleteBill returned in DataManager");
        }

        public async Task<string> GetBillingStatus(string billingStatus)
        {
            _logger.Info($"GetBillingStatus called in DataManager [Status={billingStatus}]");
            var status = await _applicationDbContext.BillingStatuses
                .AsNoTracking()
                .Where(bs => bs.Status == billingStatus)
                .Select(bs => bs.Status)
                .FirstOrDefaultAsync();
            _logger.Info("GetBillingStatus returned in DataManager");
            return status;
        }

        public async Task<BillingEntity> GetBillingForPatientAndAmount(int patientId, decimal amount)
        {
            _logger.Info($"GetBillingForPatientAndAmount called in DataManager [PatientId={patientId}, Amount={amount}]");
            var bill = await _applicationDbContext.Billings
                .AsNoTracking()
                .Where(b => b.PatientId == patientId && b.Amount == amount)
                .FirstOrDefaultAsync();
            if (bill != null)
            {
                var billEntity = new BillingEntity()
                {
                    Id = bill.Id,
                    PatientId = bill.PatientId,
                    BillingStatusId = bill.BillingStatusId,
                    Amount = bill.Amount,
                    InvoiceDate = bill.InvoiceDate
                };
                _logger.Info("GetBillingForPatientAndAmount returned in DataManager");
                return billEntity;
            }
            return null;
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}