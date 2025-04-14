using HMS.API.Abstraction.Entities.Appointment;
using HMS.API.Abstraction.Interfaces.Appointment;
using log4net;

namespace HMS.API.Services.Services.Appointment
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentDataManager _DataManager;
        private readonly IAppointmentValidationService _AppointmentValidationService;
        private readonly ILog _logger;

        public AppointmentService(IAppointmentDataManager DataManager, IAppointmentValidationService AppointmentValidationService)
        {
            _AppointmentValidationService = AppointmentValidationService;
            _DataManager = DataManager;
            _logger = LogManager.GetLogger(typeof(AppointmentService));
        }

        public async Task<List<AppointmentEntity>> GetAllApointments()
        {
            _logger.Info("GetAllApointments from Service called");
            var allAppointments = await _DataManager.GetAllApointments();
            _logger.Info("GetAllApointments from Service returned");
            return allAppointments;
        }

        public async Task<AppointmentEntity> GetAppointmentById(int id)
        {
            _logger.Info($"GetAppointmentById from Service called with [id={id}]");
            await _AppointmentValidationService.ValidateAppointment(id);
            var appointment = await _DataManager.GetAppointmentById(id);
            _logger.Info($"GetAppointmentById from Service returned");
            return appointment;
        }

        public async Task CreateAppointment(AppointmentRequest appointment)
        {
            _logger.Info($"CreateAppointment from Service called with [DoctorId={appointment.DoctorId}, PatientId={appointment.PatientId}");
            await _AppointmentValidationService.ValidateAppointmentRequest(appointment);
            await _AppointmentValidationService.ValidateAppointmentStatus(appointment.AppointmentStatus);
            await _AppointmentValidationService.ValidateAppointmentDate(appointment.AppointmentDate);
            await _DataManager.CreateAppointment(appointment);
            _logger.Info($"CreateAppointment from Service returned");
        }

        public async Task UpdateAppointment(int id, AppointmentUpdate appointmentUpdate)
        {
            _logger.Info($"UpdateAppointment from Service called with [id={id}]");
            await _AppointmentValidationService.ValidateAppointment(id);
            await _AppointmentValidationService.ValidateAppointmentStatus(appointmentUpdate.AppointmentStatus);
            await _AppointmentValidationService.ValidateAppointmentDate(appointmentUpdate.AppointmentDate);
            await _DataManager.UpdateAppointment(id, appointmentUpdate);
            _logger.Info($"UpdateAppointment from Service returned");
        }

        public async Task DeleteAppointment(int id)
        {
            _logger.Info($"DeleteAppointment from Service called with [id={id}]");
            await _AppointmentValidationService.ValidateAppointment(id);
            await _DataManager.DeleteAppointment(id);
            _logger.Info($"DeleteAppointment from Service returned");
        }

        public async Task<List<AppointmentEntity>> GetAppointmentsByPatientName(string patientName)
        {
            _logger.Info($"GetAppointmentsByPatientName from Service called with [patientName={patientName}]");
            await _AppointmentValidationService.ValidatePatientName(patientName);
            var gabpn = await _DataManager.GetAppointmentsByPatientName(patientName);
            _logger.Info($"GetAppointmentsByPatientName from Service returned");
            return gabpn;
        }

        public async Task<List<AppointmentEntity>> GetAppointmentsByPatientNameAndDate(string patientName, DateTime date)
        {
            _logger.Info($"GetAppointmentsByPatientNameAndDate from Service called with [patientName={patientName}, date={date}]");
            await _AppointmentValidationService.ValidateAppointmentByPatientNameAndDate(patientName, date.Date);
            var gabpnad = await _DataManager.GetAppointmentsByPatientNameAndDate(patientName, date.Date);
            _logger.Info($"GetAppointmentsByPatientNameAndDate from Service returned");
            return gabpnad;
        }

        public async Task<List<AppointmentEntity>> GetAppointmentsByDoctorName(string doctorName)
        {
            _logger.Info($"GetAppointmentsByDoctorName from Service called with [doctorName={doctorName}]");
            await _AppointmentValidationService.ValidateDoctorName(doctorName);
            var gabdn = await _DataManager.GetAppointmentsByDoctorName(doctorName);
            _logger.Info($"GetAppointmentsByDoctorName from Service returned");
            return gabdn;
        }

        public async Task<List<AppointmentEntity>> GetAppointmentsByDoctorNameAndDate(string doctorName, DateTime date)
        {
            _logger.Info($"GetAppointmentsByDoctorNameAndDate from Service called with [doctorName={doctorName}, date={date}]");
            await _AppointmentValidationService.ValidateAppointmentByDoctorNameAndDate(doctorName, date.Date);
            var gabdnad = await _DataManager.GetAppointmentsByDoctorNameAndDate(doctorName, date.Date);
            _logger.Info($"GetAppointmentsByDoctorNameAndDate from Service returned");
            return gabdnad;
        }

        public void Dispose()
        {
            _DataManager.Dispose();
            _AppointmentValidationService.Dispose();
        }
    }
}