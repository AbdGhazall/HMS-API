using System.ComponentModel.DataAnnotations;
using HMS.API.Abstraction.Entities;
using HMS.API.Abstraction.Entities.Doctor;
using HMS.API.Abstraction.Interfaces.Doctor;
using HMS.API.Filters;
using HMS.API.Filters.Auth;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [ApiController]
    [AuthorizeFilter("1")]
    [Route("api/[controller]/[action]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly ILog _logger;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
            _logger = LogManager.GetLogger(typeof(DoctorController));
        }

        [HttpGet]
        [ActionName("GetAllDoctors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<DoctorEntity>> GetAllDoctors([Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info("GetAllDoctors endpoint called");
            var data = await _doctorService.GetAllDoctors();
            _logger.Info("GetAllDoctors endpoint returned");
            return data;
        }

        [HttpGet("{id}")]
        [ActionName("GetDoctorById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<DoctorEntity> GetDoctorById([Required] int id, [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"GetDoctorById endpoint called, [Id={id}]");
            var doctor = await _doctorService.GetDoctorById(id);
            _logger.Info($"GetDoctorById endpoint returned");
            return doctor;
        }

        [HttpPost]
        [ActionName("CreateDoctor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseError))]
        public async Task<DoctorResponse> CreateDoctor(
            [Required][FromBody] DoctorRequest doctorRequest,
            [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"CreateDoctor endpoint called, [Phone={doctorRequest.Phone}]");
            await _doctorService.CreateDoctor(doctorRequest);
            _logger.Info($"CreateDoctor endpoint returned");
            return new DoctorResponse() { Success = true };
        }

        [HttpPut("{id}")]
        [ActionName("UpdateDoctor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<DoctorResponse> UpdateDoctor(
            [Required] int id,
            [Required][FromBody] DoctorUpdate updatedDoctor,
            [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"UpdateDoctor endpoint called, [Id={id}]");
            await _doctorService.UpdateDoctor(id, updatedDoctor);
            _logger.Info($"UpdateDoctor endpoint returned");
            return new DoctorResponse() { Success = true };
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteDoctor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<DoctorResponse> DeleteDoctor([Required] int id, [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"DeleteDoctor endpoint called, [Id={id}]");
            await _doctorService.DeleteDoctor(id);
            _logger.Info($"DeleteDoctor endpoint returned");
            return new DoctorResponse() { Success = true };
        }
    }
}