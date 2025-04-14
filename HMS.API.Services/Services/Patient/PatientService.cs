using HMS.API.Abstraction.Entities.Patient;
using HMS.API.Abstraction.Interfaces.Patient;
using log4net;
using Microsoft.Extensions.Caching.Memory;

namespace HMS.API.Services.Services.Patient
{
    public class PatientService : IPatientService
    {
        private readonly IPatientDataManager _patientDataManager;
        private readonly IPatientValidationService _patientValidationService;
        private readonly ILog _logger;
        private readonly IMemoryCache _memoryCache;

        public PatientService(IPatientDataManager patientDataManager, IPatientValidationService patientValidationService, IMemoryCache memoryCache)
        {
            _patientDataManager = patientDataManager;
            _patientValidationService = patientValidationService;
            _logger = LogManager.GetLogger(typeof(PatientService));
            _memoryCache = memoryCache;
        }

        public async Task<List<PatientEntity>> GetAllPatients()
        {
            _logger.Info("GetAllPatients from Service called");
            if (!_memoryCache.TryGetValue("AllPatients", out List<PatientEntity> allPatients))
            {
                allPatients = await _patientDataManager.GetAllPatients();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));
                _memoryCache.Set("AllPatients", allPatients, cacheEntryOptions);
            }
            _logger.Info("GetAllPatients from Service returned");
            return allPatients;
        }

        public async Task<PatientEntity> GetPatientById(int id)
        {
            _logger.Info($"GetPatientById from Service called with [id={id}]");
            await _patientValidationService.ValidatePatient(id);
            var Patient = await _patientDataManager.GetPatientById(id);
            _logger.Info($"GetPatientById from Service returned");
            return Patient;
        }

        public async Task CreatePatient(PatientRequest patientRequest)
        {
            _logger.Info($"CreatePatient from Service called with [request={patientRequest.Phone}]");
            await _patientValidationService.ValidatePatientRequest(patientRequest.Phone);
            await _patientValidationService.ValidateDateOfBirth(patientRequest.DOB);
            await _patientValidationService.ValidateGender(patientRequest.Gender);
            await _patientDataManager.CreatePatient(patientRequest);
            _logger.Info($"CreatePatient from Service returned");
        }

        public async Task UpdatePatient(int id, PatientUpdate patientUpdate)
        {
            _logger.Info($"UpdatePatient from Service called with [id={id}]");
            await _patientValidationService.ValidatePatientRequest(patientUpdate.Phone);
            await _patientValidationService.ValidateDateOfBirth(patientUpdate.DOB);
            await _patientValidationService.ValidateGender(patientUpdate.Gender);
            await _patientDataManager.UpdatePatient(id, patientUpdate);
            _logger.Info($"UpdatePatient from Service returned");
        }

        public async Task DeletePatient(int id)
        {
            _logger.Info($"DeletePatient from Service called with [id={id}]");
            await _patientValidationService.ValidatePatient(id);
            await _patientDataManager.DeletePatient(id);
            _logger.Info($"DeletePatient from Service returned");
        }

        public void Dispose()
        {
            _patientDataManager.Dispose();
            _patientValidationService.Dispose();
        }
    }
}