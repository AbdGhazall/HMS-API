using HMS.API.Abstraction.Entities.MedicalRecord;
using HMS.API.Abstraction.Interfaces.MedicalRecord;
using log4net;

namespace HMS.API.Services.Services.MedicalRecord
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordDataManager _DataManager;
        private readonly IMedicalRecordValidationService _MedicalRecordValidationService;
        private readonly ILog _logger;

        public MedicalRecordService(IMedicalRecordDataManager DataManager, IMedicalRecordValidationService MedicalRecordValidationService)
        {
            _DataManager = DataManager;
            _MedicalRecordValidationService = MedicalRecordValidationService;
            _logger = LogManager.GetLogger(typeof(MedicalRecordService));
        }

        public async Task<List<MedicalRecordEntity>> GetAllMedicalRecords()
        {
            _logger.Info("GetAllMedicalRecords from Service called");
            var allRecords = await _DataManager.GetAllMedicalRecords();
            _logger.Info("GetAllMedicalRecords from Service returned");
            return allRecords;
        }

        public async Task<MedicalRecordEntity> GetMedicalRecordById(int id)
        {
            _logger.Info($"GetMedicalRecordById from Service called with [id={id}]");
            await _MedicalRecordValidationService.ValidateMedicalRecord(id);
            var record = await _DataManager.GetMedicalRecordById(id);
            _logger.Info($"GetMedicalRecordById from Service returned");
            return record;
        }

        public async Task CreateMedicalRecord(MedicalRecordRequest medicalRecord)
        {
            _logger.Info($"CreateMedicalRecord from Service called with [PatientId={medicalRecord.PatientId}], [DoctorId={medicalRecord.DoctorId}]");
            await _MedicalRecordValidationService.ValidateMedicalRecordRequest(medicalRecord);
            await _DataManager.CreateMedicalRecord(medicalRecord);
            _logger.Info($"CreateMedicalRecord from Service returned");
        }

        public async Task UpdateMedicalRecord(int id, MedicalRecordUpdate medicalRecordUpdate)
        {
            _logger.Info($"UpdateMedicalRecord from Service called with [id={id}]");
            await _MedicalRecordValidationService.ValidateMedicalRecord(id);
            await _DataManager.UpdateMedicalRecord(id, medicalRecordUpdate);
            _logger.Info($"UpdateMedicalRecord from Service returned");
        }

        public async Task DeleteMedicalRecord(int id)
        {
            _logger.Info($"DeleteMedicalRecord from Service called with [id={id}]");
            await _MedicalRecordValidationService.ValidateMedicalRecord(id);
            await _DataManager.DeleteMedicalRecord(id);
            _logger.Info($"DeleteMedicalRecord from Service returned");
        }

        public void Dispose()
        {
            _DataManager.Dispose();
            _MedicalRecordValidationService.Dispose();
        }
    }
}