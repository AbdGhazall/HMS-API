namespace HMS.API.Abstraction.Interfaces.Patient
{
    public interface IPatientValidationService : IDisposable
    {
        Task ValidatePatient(int PatientId);

        Task ValidatePatientRequest(string patientPhone);

        Task ValidateGender(string patientGender);

        Task ValidateDateOfBirth(DateTime dob);
    }
}