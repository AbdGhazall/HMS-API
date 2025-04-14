using HMS.API.Abstraction.Entities.MedicalRecord;
using HMS.API.Abstraction.Interfaces.MedicalRecord;
using HMS.DAL.Models.Models;
using log4net;
using Microsoft.EntityFrameworkCore;

namespace HMS.DAL.DataAccess.Managers
{
    public class MedicalRecordDataManager : IMedicalRecordDataManager
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILog _logger;

        public MedicalRecordDataManager(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _logger = LogManager.GetLogger(typeof(MedicalRecordDataManager));
        }

        public async Task<List<MedicalRecordEntity>> GetAllMedicalRecords()
        {
            _logger.Info("GetAllMedicalRecords called in DataManager");
            var medicalRecord = await _applicationDbContext.MedicalRecords
                .AsNoTracking()
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Select(a => new MedicalRecordEntity
                {
                    Id = a.Id,
                    DoctorId = a.Doctor.Id,
                    PatientId = a.Patient.Id,
                    Diagnosis = a.Diagnosis,
                    LabResults = a.LabResults,
                    Prescriptions = a.Prescriptions
                }).ToListAsync();
            _logger.Info("GetAllMedicalRecords returned in DataManager");
            return medicalRecord;
        }

        public async Task<MedicalRecordEntity> GetMedicalRecordById(int id)
        {
            _logger.Info($"GetMedicalRecordById called in DataManager [Id={id}]");
            var medicalRecord = await _applicationDbContext.MedicalRecords
                .AsNoTracking()
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
            if (medicalRecord != null)
            {
                var mrdicalRecord = new MedicalRecordEntity()
                {
                    Id = medicalRecord.Id,
                    DoctorId = medicalRecord.Doctor.Id,
                    PatientId = medicalRecord.Patient.Id,
                    Diagnosis = medicalRecord.Diagnosis,
                    Prescriptions = medicalRecord.Prescriptions,
                    LabResults = medicalRecord.LabResults
                };
                _logger.Info("GetMedicalRecordById returned in DataManager");
                return mrdicalRecord;
            }
            return null;
        }

        public async Task CreateMedicalRecord(MedicalRecordRequest medicalRecord)
        {
            _logger.Info($"CreateMedicalRecord called in DataManager [PatientId={medicalRecord.PatientId}], [DoctorId={medicalRecord.DoctorId}]");
            _applicationDbContext.MedicalRecords.Add(new MedicalRecord()
            {
                DoctorId = medicalRecord.DoctorId,
                PatientId = medicalRecord.PatientId,
                Diagnosis = medicalRecord.Diagnosis,
                Prescriptions = medicalRecord.Prescriptions,
                LabResults = medicalRecord.LabResults
            });
            await _applicationDbContext.SaveChangesAsync();
            _logger.Info("CreateMedicalRecord returned in DataManager");
        }

        public async Task UpdateMedicalRecord(int id, MedicalRecordUpdate medicalRecordUpdate)
        {
            _logger.Info($"UpdateMedicalRecord called in DataManager [Id={id}]");
            var medicalRecord = await _applicationDbContext.MedicalRecords.FindAsync(id);
            if (medicalRecord != null)
            {
                medicalRecord.Diagnosis = medicalRecordUpdate.Diagnosis;
                medicalRecord.Prescriptions = medicalRecordUpdate.Prescriptions;
                medicalRecord.LabResults = medicalRecordUpdate.LabResults;

                await _applicationDbContext.SaveChangesAsync();
                _logger.Info("UpdateMedicalRecord returned in DataManager");
            }
        }

        public async Task DeleteMedicalRecord(int id)
        {
            _logger.Info($"DeleteMedicalRecord called in DataManager [Id={id}]");
            var medicalRecord = await _applicationDbContext.MedicalRecords.FindAsync(id);
            _applicationDbContext.MedicalRecords.Remove(medicalRecord);
            await _applicationDbContext.SaveChangesAsync();
            _logger.Info("DeleteMedicalRecord returned in DataManager");
        }

        public async Task<bool> MedicalRecordExists(string diagnosis, string Prescriptions, string LabResults)
        {
            _logger.Info($"MedicalRecordExists called in DataManager [Diagnosis={diagnosis}], [Prescriptions={Prescriptions}], [LabResults={LabResults}]");
            var medicalRecord = await _applicationDbContext.MedicalRecords
                .Where(mr => mr.Diagnosis == diagnosis && mr.Prescriptions == Prescriptions && mr.LabResults == LabResults)
                .AnyAsync();
            _logger.Info("MedicalRecordExists returned in DataManager");
            return medicalRecord;
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}