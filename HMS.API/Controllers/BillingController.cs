using System.ComponentModel.DataAnnotations;
using HMS.API.Abstraction.Entities;
using HMS.API.Abstraction.Entities.Billing;
using HMS.API.Abstraction.Interfaces.Billing;
using HMS.API.Filters;
using HMS.API.Filters.Auth;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [ApiController]
    [AuthorizeFilter("1")]
    [Route("api/[controller]/[action]")]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;
        private readonly ILog _logger;

        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
            _logger = LogManager.GetLogger(typeof(BillingController));
        }

        [HttpGet]
        [ActionName("GetAllBillings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<BillingEntity>> GetAllBillings([Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info("GetAllBillings endpoint called");
            var allBillings = await _billingService.GetAllBillings();
            _logger.Info("GetAllBillings endpoint returned");
            return allBillings;
        }

        [HttpGet("{id}")]
        [ActionName("GetBillById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<BillingEntity> GetBillById([Required] int id, [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"GetBillById endpoint called, [Id={id}]");
            var bill = await _billingService.GetBillByID(id);
            _logger.Info($"GetBillById endpoint returned");
            return bill;
        }

        [HttpPost]
        [ActionName("CreateBill")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseError))]
        public async Task<BillingResponse> CreateBill(
            [Required][FromBody] BillingRequest billRequest,
            [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"CreateBill endpoint called, [PatientId={billRequest.PatientId}]");
            await _billingService.CreateBill(billRequest);
            _logger.Info("CreateBill endpoint returned");
            return new BillingResponse() { Success = true };
        }

        [HttpPut("{id}")]
        [ActionName("UpdateBill")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<BillingResponse> UpdateBill(
            [Required] int id,
            [Required][FromBody] BillingUpdate updatedBill,
            [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"UpdateBill endpoint called, [Id={id}]");
            await _billingService.UpdateBill(id, updatedBill);
            _logger.Info("UpdateBill endpoint returned");
            return new BillingResponse() { Success = true };
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteBill")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<BillingResponse> DeleteBill([Required] int id, [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"DeleteBill endpoint called, [Id={id}]");
            await _billingService.DeleteBill(id);
            _logger.Info("DeleteBill endpoint returned");
            return new BillingResponse() { Success = true };
        }
    }
}