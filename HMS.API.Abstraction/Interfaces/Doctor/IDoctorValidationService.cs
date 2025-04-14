namespace HMS.API.Abstraction.Interfaces.Doctor
{
    public interface IDoctorValidationService : IDisposable
    {
        Task ValidateDoctor(int DoctorId);

        Task ValidateDoctorRequest(string doctorPhone);

        Task ValidateSpecialty(string doctorSpecialty);
    }
}