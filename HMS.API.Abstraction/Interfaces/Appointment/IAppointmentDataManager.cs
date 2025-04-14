using HMS.API.Abstraction.Entities.Appointment;

namespace HMS.API.Abstraction.Interfaces.Appointment
{
    public interface IAppointmentDataManager : IDisposable
    {
        Task<List<AppointmentEntity>> GetAllApointments();

        Task<AppointmentEntity> GetAppointmentById(int id);

        Task CreateAppointment(AppointmentRequest appointment);

        Task UpdateAppointment(int id, AppointmentUpdate appointmentUpdate);

        Task DeleteAppointment(int id);

        // Method to check if an appointment date is available for a specific doctor and patient
        Task<bool> IsAppointmentAvailable(int doctorId, int patientId, DateTime appointmentDate);

        Task<string> GetAppointmentStatus(string appointmentStatus);

        Task<List<AppointmentEntity>> GetAppointmentsByPatientName(string patientName);

        Task<List<AppointmentEntity>> GetAppointmentsByPatientNameAndDate(string patientName, DateTime date);

        Task<List<AppointmentEntity>> GetAppointmentsByDoctorName(string doctorName);

        Task<List<AppointmentEntity>> GetAppointmentsByDoctorNameAndDate(string doctorName, DateTime date);

        Task<bool> IsPatientNameAndDateExist(string patientName, DateTime date);

        Task<bool> IsDoctorNameAndDateExist(string doctorName, DateTime date);

        Task<bool> IsPatientNameExist(string patientName);

        Task<bool> IsDoctorNameExist(string doctorName);
    }
}