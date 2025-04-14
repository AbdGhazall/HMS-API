using HMS.API.Abstraction.Enums;
using HMS.API.Abstraction.Exceptions;
using HMS.API.Abstraction.Interfaces.Patient;
using log4net;
using Microsoft.AspNetCore.Http;

namespace HMS.API.Services.Services.Patient
{
    public class PatientValidationService : IPatientValidationService
    {
        private readonly IPatientDataManager _patientDataManager;
        private readonly ILog _logger;

        public PatientValidationService(IPatientDataManager patientDataManager)
        {
            _patientDataManager = patientDataManager;
            _logger = LogManager.GetLogger(typeof(PatientValidationService));
        }

        public async Task ValidatePatient(int PatientId)
        {
            _logger.Info($"ValidatePatient called in ValidationService with ID: {PatientId}");
            var patient = await _patientDataManager.GetPatientById(PatientId);
            if (patient == null)
            {
                throw new PatientException("Patient Not Found", (int)ErrorCodes.PatientNotFound, (int)StatusCodes.Status404NotFound);
            }
        }

        public async Task ValidatePatientRequest(string patientPhone)
        {
            _logger.Info($"ValidatePatientRequest called in ValidationService with phone: {patientPhone}");
            var currentPatient = await _patientDataManager.GetPatientByPhone(patientPhone);
            if (currentPatient != null)
            {
                throw new PatientException("Patient is exist", (int)ErrorCodes.PatientIsExist, (int)StatusCodes.Status400BadRequest);
            }
        }

        public async Task ValidateGender(string patientGender)
        {
            _logger.Info($"ValidateGender called in ValidationService with gender: {patientGender}");
            var gender = await _patientDataManager.GetPatientGender(patientGender);
            if (gender == null)
            {
                throw new PatientException("Invalid gender specified.",
                                        (int)ErrorCodes.InvalidGender,
                                        (int)StatusCodes.Status400BadRequest);
            }
        }

        public async Task ValidateDateOfBirth(DateTime dob)
        {
            _logger.Info($"ValidateDateOfBirth called in ValidationService with date: {dob}");
            if (dob > DateTime.UtcNow.Date)
            {
                throw new PatientException("Date of Birth cannot be in the future.",
                                           (int)ErrorCodes.InvalidDateOfBirth,
                                           (int)StatusCodes.Status400BadRequest);
            }
        }

        public void Dispose()
        {
            _patientDataManager.Dispose();
        }
    }
}