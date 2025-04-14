using HMS.API.Abstraction.Entities.Patient;
using HMS.API.Abstraction.Interfaces.Patient;
using HMS.DAL.Models.Models;
using log4net;
using Microsoft.EntityFrameworkCore;

namespace HMS.DAL.DataAccess.Managers
{
    public class PatientDataManager : IPatientDataManager
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILog _logger;

        public PatientDataManager(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _logger = LogManager.GetLogger(typeof(PatientDataManager));
        }

        public async Task<List<PatientEntity>> GetAllPatients()
        {
            _logger.Info("GetAllPatients called in DataManager");
            var patients = await _applicationDbContext.Patients
                .AsNoTracking()
                .Include(p => p.User)
                .Select(p => new PatientEntity
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    GenderId = p.GenderId,
                    FullName = p.FullName,
                    Email = p.User.Email,
                    DobDate = p.DOB,
                    Phone = p.Phone,
                    MedicalHistory = p.MedicalHistory,
                }).ToListAsync();
            _logger.Info("GetAllPatients returned in DataManager");
            return patients;
        }

        public async Task<PatientEntity> GetPatientById(int id)
        {
            _logger.Info($"GetPatientById called in DataManager [Id={id}]");
            var patient = await _applicationDbContext.Patients
                .AsNoTracking()
                .Include(p => p.User)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (patient != null)
            {
                var patientEntity = new PatientEntity()
                {
                    Id = patient.Id,
                    UserId = patient.User.Id,
                    GenderId = patient.GenderId,
                    FullName = patient.FullName,
                    Email = patient.User.Email,
                    DobDate = patient.DOB,
                    Phone = patient.Phone,
                    MedicalHistory = patient.MedicalHistory,
                };
                _logger.Info($"GetPatientById returned in DataManager");
                return patientEntity;
            }
            return null;
        }

        public async Task CreatePatient(PatientRequest patient)
        {
            _logger.Info($"CreatePatient called in DataManager [Phone={patient.Phone}]");
            var gender = await _applicationDbContext.Genders
                .Where(r => r.GenderType == patient.Gender)
                .FirstOrDefaultAsync();

            if (gender != null)
            {
                _applicationDbContext.Patients.Add(new Patient()
                {
                    UserId = patient.UserId,
                    Gender = gender,
                    FullName = patient.FullName,
                    DOB = patient.DOB,
                    Phone = patient.Phone,
                    MedicalHistory = patient.MedicalHistory,
                });
                await _applicationDbContext.SaveChangesAsync();
                _logger.Info("CreatePatient returned in DataManager");
            }
        }

        public async Task UpdatePatient(int id, PatientUpdate patientUpdate)
        {
            _logger.Info($"UpdatePatient called in DataManager [Id={id}]");
            var patient = await _applicationDbContext.Patients.FindAsync(id);
            if (patient != null)
            {
                patient.FullName = patientUpdate.FullName;
                patient.DOB = patientUpdate.DOB;
                patient.Phone = patientUpdate.Phone;
                patient.MedicalHistory = patientUpdate.MedicalHistory;

                var gender = await _applicationDbContext.Genders
                    .Where(r => r.GenderType == patientUpdate.Gender)
                    .FirstOrDefaultAsync();
                if (gender != null)
                {
                    patient.Gender = gender;
                }

                await _applicationDbContext.SaveChangesAsync();
                _logger.Info($"UpdatePatient returned in DataManager");
            }
        }

        public async Task DeletePatient(int id)
        {
            _logger.Warn($"DeletePatient called in DataManager [Id={id}]");
            var patient = await _applicationDbContext.Patients.FindAsync(id);
            _applicationDbContext.Patients.Remove(patient);
            await _applicationDbContext.SaveChangesAsync();
            _logger.Warn($"DeletePatient returned in DataManager");
        }

        public async Task<PatientEntity> GetPatientByPhone(string phone)
        {
            _logger.Info($"GetPatientByPhone called in DataManager [Phone={phone}]");
            var patient = await _applicationDbContext.Patients
                .Include(x => x.User)
                .Where(x => x.Phone == phone)
                .FirstOrDefaultAsync();
            if (patient != null)
            {
                var PatientEntity = new PatientEntity()
                {
                    Id = patient.Id,
                    DobDate = patient.DOB,
                    MedicalHistory = patient.MedicalHistory,
                    GenderId = patient.GenderId,
                    Phone = patient.Phone,
                    UserId = patient.UserId,
                    FullName = patient.FullName,
                    Email = patient.User.Email
                };
                _logger.Info($"GetPatientByPhone returned in DataManager");
                return PatientEntity;
            }
            return null;
        }

        public async Task<string> GetPatientGender(string patirntGender)
        {
            _logger.Info($"GetPatientGender called in DataManager [GenderType={patirntGender}]");
            var gender = await _applicationDbContext.Genders
                        .Where(r => r.GenderType == patirntGender)
                        .Select(r => r.GenderType)
                        .FirstOrDefaultAsync();
            _logger.Info($"GetPatientGender returned in DataManager");
            return gender;
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}