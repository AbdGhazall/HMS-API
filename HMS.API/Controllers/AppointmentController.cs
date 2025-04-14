using System.ComponentModel.DataAnnotations;
using HMS.API.Abstraction.Entities;
using HMS.API.Abstraction.Entities.Appointment;
using HMS.API.Abstraction.Interfaces.Appointment;
using HMS.API.Filters;
using HMS.API.Filters.Auth;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [ApiController]
    [AuthorizeFilter("1")]
    [Route("api/[controller]/[action]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ILog _logger;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
            _logger = LogManager.GetLogger(typeof(AppointmentController));
        }

        [HttpGet]
        [ActionName("GetAllAppointments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<AppointmentEntity>> GetAllAppointments([Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info("GetAllAppointments endpoint called");
            var allAppointments = await _appointmentService.GetAllApointments();
            _logger.Info("GetAllAppointments endpoint returned");
            return allAppointments;
        }

        [HttpGet("{id}")]
        [ActionName("GetAppointmentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<AppointmentEntity> GetAppointmentById([Required] int id, [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"GetAppointmentById endpoint called, [Id={id}]");
            var appointment = await _appointmentService.GetAppointmentById(id);
            _logger.Info($"GetAppointmentById endpoint returned");
            return appointment;
        }

        [HttpPost]
        [ActionName("CreateAppointment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseError))]
        public async Task<AppointmentResponse> CreateAppointment(
            [Required][FromBody] AppointmentRequest appointmentRequest,
            [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"CreateAppointment endpoint called, [DoctorId={appointmentRequest.DoctorId}, PatientId={appointmentRequest.PatientId}]");
            await _appointmentService.CreateAppointment(appointmentRequest);
            _logger.Info("CreateAppointment endpoint returned");
            return new AppointmentResponse() { Success = true };
        }

        [HttpPut("{id}")]
        [ActionName("UpdateAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<AppointmentResponse> UpdateAppointment(
            [Required] int id,
            [Required][FromBody] AppointmentUpdate updatedAppointment,
            [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"UpdateAppointment endpoint called, [Id={id}]");
            await _appointmentService.UpdateAppointment(id, updatedAppointment);
            _logger.Info("UpdateAppointment endpoint returned");
            return new AppointmentResponse() { Success = true };
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<AppointmentResponse> DeleteAppointment([Required] int id, [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"DeleteAppointment endpoint called, [Id={id}]");
            await _appointmentService.DeleteAppointment(id);
            _logger.Info("DeleteAppointment endpoint returned");
            return new AppointmentResponse() { Success = true };
        }

        [HttpGet("{patientName}")]
        [ActionName("GetAppointmentsByPatientName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<AppointmentEntity>> GetAppointmentsByPatientName([Required] string patientName, [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"GetAppointmentsByPatientName endpoint called, [PatientName={patientName}]");
            var appointments = await _appointmentService.GetAppointmentsByPatientName(patientName);
            _logger.Info("GetAppointmentsByPatientName endpoint returned");
            return appointments;
        }

        [HttpGet("{patientName}/{date}")]
        [ActionName("GetAppointmentsByPatientNameAndDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<AppointmentEntity>> GetAppointmentsByPatientNameAndDate([Required] string patientName, [Required] DateTime date, [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"GetAppointmentsByPatientNameAndDate endpoint called, [PatientName={patientName}, Date={date}]");
            var appointments = await _appointmentService.GetAppointmentsByPatientNameAndDate(patientName, date);
            _logger.Info("GetAppointmentsByPatientNameAndDate endpoint returned");
            return appointments;
        }

        [HttpGet("{doctorName}")]
        [ActionName("GetAppointmentsByDoctorName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<AppointmentEntity>> GetAppointmentsByDoctorName([Required] string doctorName, [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"GetAppointmentsByDoctorName endpoint called, [DoctorName={doctorName}]");
            var appointments = await _appointmentService.GetAppointmentsByDoctorName(doctorName);
            _logger.Info("GetAppointmentsByDoctorName endpoint returned");
            return appointments;
        }

        [HttpGet("{doctorName}/{date}")]
        [ActionName("GetAppointmentsByDoctorNameAndDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<AppointmentEntity>> GetAppointmentsByDoctorNameAndDate([Required] string doctorName, [Required] DateTime date, [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"GetAppointmentsByDoctorNameAndDate endpoint called, [DoctorName={doctorName}, Date={date}]");
            var appointments = await _appointmentService.GetAppointmentsByDoctorNameAndDate(doctorName, date);
            _logger.Info("GetAppointmentsByDoctorNameAndDate endpoint returned");
            return appointments;
        }
    }
}