using HMS.API.Abstraction.Entities.Appointment;

namespace HMS.API.Abstraction.Interfaces.Appointment
{
    public interface IAppointmentValidationService : IDisposable
    {
        Task ValidateAppointment(int appointmentId);

        Task ValidateAppointmentRequest(AppointmentRequest appointmentRequest);

        Task ValidateAppointmentStatus(string appointmentStatus);

        Task ValidateAppointmentDate(DateTime dob);

        Task ValidateAppointmentByPatientNameAndDate(string patientName, DateTime date);

        Task ValidateAppointmentByDoctorNameAndDate(string doctorName, DateTime date);

        Task ValidatePatientName(string patientName);

        Task ValidateDoctorName(string doctorName);
    }
}