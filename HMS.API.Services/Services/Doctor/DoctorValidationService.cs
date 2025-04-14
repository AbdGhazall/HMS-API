using HMS.API.Abstraction.Enums;
using HMS.API.Abstraction.Exceptions;
using HMS.API.Abstraction.Interfaces.Doctor;
using log4net;
using Microsoft.AspNetCore.Http;

namespace HMS.API.Services.Services.Doctor
{
    public class DoctorValidationService : IDoctorValidationService
    {
        private readonly IDoctorDataManager _dataManager;
        private readonly ILog _logger;

        public DoctorValidationService(IDoctorDataManager dataManager)
        {
            _dataManager = dataManager;
            _logger = LogManager.GetLogger(typeof(DoctorValidationService));
        }

        public async Task ValidateDoctor(int DoctorId)
        {
            _logger.Info($"ValidateDoctor called in ValidationService with ID: {DoctorId}");
            var doctor = await _dataManager.GetDoctorById(DoctorId);
            if (doctor == null)
            {
                throw new DoctorException("Doctor Not Found", (int)ErrorCodes.DoctorNotFound, (int)StatusCodes.Status404NotFound);
            }
        }

        public async Task ValidateDoctorRequest(string doctorPhone)
        {
            _logger.Info($"ValidateDoctorRequest called in ValidationService with phone: {doctorPhone}");
            var cuurentDoctor = await _dataManager.GetDoctorByPhone(doctorPhone);
            if (cuurentDoctor != null)
            {
                throw new DoctorException("Doctor is exist", (int)ErrorCodes.DoctorIsExist, (int)StatusCodes.Status400BadRequest);
            }
        }

        public async Task ValidateSpecialty(string doctorSpecialty)
        {
            _logger.Info($"ValidateSpecialty called in ValidationService with specialty: {doctorSpecialty}");
            var specialty = await _dataManager.GetDoctorSpecialty(doctorSpecialty);
            if (specialty == null)
            {
                throw new DoctorException("Invalid Specialty specified", (int)ErrorCodes.InvalidSpecialty, (int)StatusCodes.Status404NotFound);
            }
        }

        public void Dispose()
        {
            _dataManager.Dispose();
        }
    }
}