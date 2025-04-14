using System.ComponentModel.DataAnnotations;
using HMS.API.Abstraction.Entities;
using HMS.API.Abstraction.Entities.Patient;
using HMS.API.Abstraction.Interfaces.Patient;
using HMS.API.Filters;
using HMS.API.Filters.Auth;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [ApiController]
    [AuthorizeFilter("1")]
    [Route("api/[controller]/[action]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILog _logger;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
            _logger = LogManager.GetLogger(typeof(PatientController));
        }

        [HttpGet]
        [ActionName("GetAllPatients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<PatientEntity>> GetAllPatients([Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info("GetAllPatients endpoint called");
            var patients = await _patientService.GetAllPatients();
            _logger.Info("GetAllPatients endpoint returned");
            return patients;
        }

        [HttpGet("{id}")]
        [ActionName("GetPatientById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<PatientEntity> GetPatientById([Required] int id, [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"getPatientById endpoint called, [Id={id}]");
            var patient = await _patientService.GetPatientById(id);
            _logger.Info($"getPatientById endpoint returned");
            return patient;
        }

        [HttpPost]
        [ActionName("CreatePatient")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseError))]
        public async Task<PatientResponse> CreatePatient(
            [Required][FromBody] PatientRequest patientRequest,
            [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"CreatePatient endpoint called, [Phone={patientRequest.Phone}]");
            await _patientService.CreatePatient(patientRequest);
            _logger.Info("CreatePatient endpoint returned");
            return new PatientResponse() { Success = true };
        }

        [HttpPut("{id}")]
        [ActionName("UpdatePatient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<PatientResponse> UpdatePatient(
            [Required] int id,
            [Required][FromBody] PatientUpdate updatedPatient,
            [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"UpdatePatient endpoint called, [Id={id}]");
            await _patientService.UpdatePatient(id, updatedPatient);
            _logger.Info("UpdatePatient endpoint returned");
            return new PatientResponse() { Success = true };
        }

        [HttpDelete("{id}")]
        [ActionName("DeletePatient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PatientResponse> DeletePatient([Required] int id, [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Warn($"DeletePatient endpoint called, [Id={id}]");
            await _patientService.DeletePatient(id);
            _logger.Warn("DeletePatient endpoint returned");
            return new PatientResponse() { Success = true };
        }
    }
}