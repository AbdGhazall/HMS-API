using HMS.API.Abstraction.Entities.MedicalRecord;

namespace HMS.API.Abstraction.Interfaces.MedicalRecord
{
    public interface IMedicalRecordValidationService : IDisposable
    {
        Task ValidateMedicalRecord(int medicalRequestId);

        Task ValidateMedicalRecordRequest(MedicalRecordRequest medicalRecordRequest);
    }
}