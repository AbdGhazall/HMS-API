using System.ComponentModel.DataAnnotations;
using HMS.API.Abstraction.Entities;
using HMS.API.Abstraction.Entities.MedicalRecord;
using HMS.API.Abstraction.Interfaces.MedicalRecord;
using HMS.API.Filters;
using HMS.API.Filters.Auth;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;
        private readonly ILog _logger;

        public MedicalRecordController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
            _logger = LogManager.GetLogger(typeof(MedicalRecordController));
        }

        [HttpGet]
        [AuthorizeFilter("1")]
        [ActionName("GetAllMedicalRecords")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<MedicalRecordEntity>> GetAllMedicalRecords([Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info("GetAllMedicalRecords endpoint called");
            var medicalRecord = await _medicalRecordService.GetAllMedicalRecords();
            _logger.Info("GetAllMedicalRecords endpoint returned");
            return medicalRecord;
        }

        [HttpGet("{id}")]
        [AuthorizeFilter("1")]
        [ActionName("GetMedicalRecordById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<MedicalRecordEntity> GetMedicalRecordById([Required] int id, [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"GetMedicalRecordById endpoint called, [Id={id}]");
            var medicalRecord = await _medicalRecordService.GetMedicalRecordById(id);
            _logger.Info($"GetMedicalRecordById endpoint returned");
            return medicalRecord;
        }

        [HttpPost]
        [AuthorizeFilter("1", "2")]
        [ActionName("CreateMedicalRecord")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseError))]
        public async Task<MedicalRecordResponse> CreateMedicalRecord(
            [Required][FromBody] MedicalRecordRequest medicalRecordRequest,
            [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"CreateMedicalRecord endpoint called, [PatientId={medicalRecordRequest.PatientId}], [DoctorId={medicalRecordRequest.DoctorId}]");
            await _medicalRecordService.CreateMedicalRecord(medicalRecordRequest);
            _logger.Info("CreateMedicalRecord endpoint returned");
            return new MedicalRecordResponse() { Success = true };
        }

        [HttpPut("{id}")]
        [AuthorizeFilter("1", "2")]
        [ActionName("UpdateMedicalRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<MedicalRecordResponse> UpdateMedicalRecord(
            [Required] int id,
            [Required][FromBody] MedicalRecordUpdate updatedmedicalRecord,
            [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"UpdateMedicalRecord endpoint called, [Id={id}]");
            await _medicalRecordService.UpdateMedicalRecord(id, updatedmedicalRecord);
            _logger.Info("UpdateMedicalRecord endpoint returned");
            return new MedicalRecordResponse() { Success = true };
        }

        [HttpDelete("{id}")]
        [AuthorizeFilter("1")]
        [ActionName("DeleteMedicalRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<MedicalRecordResponse> DeleteMedicalRecord([Required] int id, [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"DeleteMedicalRecord endpoint called, [Id={id}]");
            await _medicalRecordService.DeleteMedicalRecord(id);
            _logger.Info("DeleteMedicalRecord endpoint returned");
            return new MedicalRecordResponse() { Success = true };
        }
    }
}