using HMS.API.Abstraction.Entities.Appointment;
using HMS.API.Abstraction.Enums;
using HMS.API.Abstraction.Exceptions;
using HMS.API.Abstraction.Interfaces.Appointment;
using log4net;
using Microsoft.AspNetCore.Http;

namespace HMS.API.Services.Services.Appointment
{
    public class AppointmentValidationService : IAppointmentValidationService
    {
        private readonly IAppointmentDataManager _appointmentDataManager;
        private readonly ILog _logger;

        public AppointmentValidationService(IAppointmentDataManager appointmentDataManager)
        {
            _appointmentDataManager = appointmentDataManager;
            _logger = LogManager.GetLogger(typeof(AppointmentValidationService));
        }

        public async Task ValidateAppointment(int appointmentId)
        {
            _logger.Info($"ValidateAppointment called in ValidationService with ID: {appointmentId}");
            var appointment = await _appointmentDataManager.GetAppointmentById(appointmentId);
            if (appointment == null)
            {
                throw new AppointmentException("Appointment Not Found", (int)ErrorCodes.AppoitmentNotFound, (int)StatusCodes.Status404NotFound);
            }
        }

        public async Task ValidateAppointmentRequest(AppointmentRequest appointmentRequest)
        {
            _logger.Info($"ValidateAppointmentRequest called in ValidationService with DoctorId: {appointmentRequest.DoctorId} and PatientId: {appointmentRequest.PatientId}");
            var AppointmentAvailable = await _appointmentDataManager.IsAppointmentAvailable(appointmentRequest.DoctorId, appointmentRequest.PatientId, appointmentRequest.AppointmentDate);
            if (!AppointmentAvailable)
            {
                throw new AppointmentException("An appointment already exists on this date for this doctor and patient.", (int)ErrorCodes.AppoitmentIsExist, (int)StatusCodes.Status400BadRequest);
            }
        }

        public async Task ValidateAppointmentStatus(string appointmentStatus)
        {
            _logger.Info($"ValidateAppointmentStatus called in ValidationService with Status: {appointmentStatus}");
            var status = await _appointmentDataManager.GetAppointmentStatus(appointmentStatus);
            if (status == null)
            {
                throw new AppointmentException("Invalid Status specified", (int)ErrorCodes.InvalidStatus, (int)StatusCodes.Status404NotFound);
            }
        }

        public async Task ValidateAppointmentDate(DateTime dob)
        {
            _logger.Info($"ValidateAppointmentDate called in ValidationService with Date: {dob}");
            if (dob < DateTime.UtcNow.Date)
            {
                throw new AppointmentException("Appointment date cannot be in the past.",
                                           (int)ErrorCodes.InvalidAppointmentDate,
                                           (int)StatusCodes.Status400BadRequest);
            }
        }

        public async Task ValidateAppointmentByPatientNameAndDate(string patientName, DateTime date)
        {
            _logger.Info($"ValidateAppointmentByPatientNameAndDate called in ValidationService with PatientName: {patientName} and Date: {date}");
            var appointments = await _appointmentDataManager.IsPatientNameAndDateExist(patientName, date.Date);
            if (!appointments)
            {
                throw new AppointmentException("No appointments found for the specified patient name and date.",
                                               (int)ErrorCodes.AppoitmentNotFound,
                                               (int)StatusCodes.Status404NotFound);
            }
        }

        public async Task ValidateAppointmentByDoctorNameAndDate(string doctorName, DateTime date)
        {
            _logger.Info($"ValidateAppointmentByDoctorNameAndDate called in ValidationService with DoctorName: {doctorName} and Date: {date}");
            var appointments = await _appointmentDataManager.IsDoctorNameAndDateExist(doctorName, date.Date);
            if (!appointments)
            {
                throw new AppointmentException("No appointments found for the specified doctor name and date.",
                                               (int)ErrorCodes.AppoitmentNotFound,
                                               (int)StatusCodes.Status404NotFound);
            }
        }

        public async Task ValidatePatientName(string patientName)
        {
            _logger.Info($"ValidatePatientName called in ValidationService with PatientName: {patientName}");
            var patient = await _appointmentDataManager.IsPatientNameExist(patientName);
            if (!patient)
            {
                throw new AppointmentException("No appointments found for the specified patient name.",
                                               (int)ErrorCodes.AppoitmentNotFound,
                                               (int)StatusCodes.Status404NotFound);
            }
        }

        public async Task ValidateDoctorName(string doctorName)
        {
            _logger.Info($"ValidateDoctorName called in ValidationService with DoctorName: {doctorName}");
            var doctor = await _appointmentDataManager.IsDoctorNameExist(doctorName);
            if (!doctor)
            {
                throw new AppointmentException("No appointments found for the specified doctor name.",
                                               (int)ErrorCodes.AppoitmentNotFound,
                                               (int)StatusCodes.Status404NotFound);
            }
        }

        public void Dispose()
        {
            _appointmentDataManager.Dispose();
        }
    }
}