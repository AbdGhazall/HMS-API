using HMS.API.Abstraction.Entities.Appointment;

namespace HMS.API.Abstraction.Interfaces.Appointment
{
    public interface IAppointmentService : IDisposable
    {
        Task<List<AppointmentEntity>> GetAllApointments();

        Task<AppointmentEntity> GetAppointmentById(int id);

        Task CreateAppointment(AppointmentRequest appointment);

        Task UpdateAppointment(int id, AppointmentUpdate appointmentUpdate);

        Task DeleteAppointment(int id);

        Task<List<AppointmentEntity>> GetAppointmentsByPatientName(string patientName);

        Task<List<AppointmentEntity>> GetAppointmentsByPatientNameAndDate(string patientName, DateTime date);

        Task<List<AppointmentEntity>> GetAppointmentsByDoctorName(string doctorName);

        Task<List<AppointmentEntity>> GetAppointmentsByDoctorNameAndDate(string doctorName, DateTime date);
    }
}