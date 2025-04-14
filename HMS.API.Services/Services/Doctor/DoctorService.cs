using HMS.API.Abstraction.Entities.Doctor;
using HMS.API.Abstraction.Interfaces.Doctor;
using log4net;
using Microsoft.Extensions.Caching.Memory;

namespace HMS.API.Services.Services.Doctor
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorDataManager _DataManager;
        private readonly IDoctorValidationService _ValidationService;
        private readonly IMemoryCache _memoryCache;
        private readonly ILog _logger;

        public DoctorService(IDoctorDataManager DataManager,
            IDoctorValidationService validationService,
            IMemoryCache memoryCache)

        {
            _DataManager = DataManager;
            _ValidationService = validationService;
            _memoryCache = memoryCache;
            _logger = LogManager.GetLogger(typeof(DoctorService));
        }

        public async Task<List<DoctorEntity>> GetAllDoctors()
        {
            _logger.Info("GetAllDoctors from Service called");
            var allDoctors = await _DataManager.GetAllDoctors();
            _logger.Info("GetAllDoctors from Service returned");
            return allDoctors;
        }

        public async Task<DoctorEntity> GetDoctorById(int id)
        {
            _logger.Info($"GetDoctorById from Service called with [id={id}]");
            await _ValidationService.ValidateDoctor(id);
            var doctor = await _DataManager.GetDoctorById(id);
            _logger.Info($"GetDoctorById from Service returned");
            return doctor;
        }

        public async Task CreateDoctor(DoctorRequest doctor)
        {
            _logger.Info($"CreateDoctor from Service called with [request={doctor.Phone}]");
            await _ValidationService.ValidateDoctorRequest(doctor.Phone);
            await _ValidationService.ValidateSpecialty(doctor.Specialty);
            await _DataManager.CreateDoctor(doctor);
            _logger.Info($"CreateDoctor from Service returned");
        }

        public async Task UpdateDoctor(int id, DoctorUpdate doctor)
        {
            _logger.Info($"UpdateDoctor from Service called with [id={id}]");
            await _ValidationService.ValidateDoctorRequest(doctor.Phone);
            await _ValidationService.ValidateSpecialty(doctor.Specialty);
            await _DataManager.UpdateDoctor(id, doctor);
            _logger.Info($"UpdateDoctor from Service returned");
        }

        public async Task DeleteDoctor(int id)
        {
            _logger.Info($"DeleteDoctor from Service called with [id={id}]");
            await _ValidationService.ValidateDoctor(id);
            await _DataManager.DeleteDoctor(id);
            _logger.Info($"DeleteDoctor from Service returned");
        }

        public void Dispose()
        {
            _DataManager.Dispose();
            _ValidationService.Dispose();
        }
    }
}