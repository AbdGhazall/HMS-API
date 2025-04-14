using HMS.API.Abstraction.Entities.MedicalRecord;

namespace HMS.API.Abstraction.Interfaces.MedicalRecord
{
    public interface IMedicalRecordService : IDisposable
    {
        Task<List<MedicalRecordEntity>> GetAllMedicalRecords();

        Task<MedicalRecordEntity> GetMedicalRecordById(int id);

        Task CreateMedicalRecord(MedicalRecordRequest medicalRecord);

        Task UpdateMedicalRecord(int id, MedicalRecordUpdate medicalRecordUpdate);

        Task DeleteMedicalRecord(int id);
    }
}