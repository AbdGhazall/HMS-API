using HMS.API.Abstraction.Entities.Patient;

namespace HMS.API.Abstraction.Interfaces.Patient
{
    public interface IPatientDataManager : IDisposable
    {
        Task<List<PatientEntity>> GetAllPatients();

        Task<PatientEntity> GetPatientById(int id);

        Task CreatePatient(PatientRequest patient);

        Task UpdatePatient(int id, PatientUpdate patientUpdate);

        Task DeletePatient(int id);

        Task<PatientEntity> GetPatientByPhone(string phone);

        Task<string> GetPatientGender(string patirntGender);
    }
}