using HMS.API.Abstraction.Entities.Patient;

namespace HMS.API.Abstraction.Interfaces.Patient
{
    public interface IPatientService : IDisposable
    {
        Task<List<PatientEntity>> GetAllPatients();

        Task<PatientEntity> GetPatientById(int id);

        Task CreatePatient(PatientRequest patientRequest);

        Task UpdatePatient(int id, PatientUpdate patientUpdate);

        Task DeletePatient(int id);
    }
}