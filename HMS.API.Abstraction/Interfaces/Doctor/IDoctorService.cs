using HMS.API.Abstraction.Entities.Doctor;

namespace HMS.API.Abstraction.Interfaces.Doctor
{
    public interface IDoctorService : IDisposable
    {
        Task<List<DoctorEntity>> GetAllDoctors();

        Task<DoctorEntity> GetDoctorById(int id);

        Task CreateDoctor(DoctorRequest doctor);

        Task UpdateDoctor(int id, DoctorUpdate doctor);

        Task DeleteDoctor(int id);
    }
}