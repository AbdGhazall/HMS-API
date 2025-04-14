using HMS.API.Abstraction.Entities.MedicalRecord;
using HMS.API.Abstraction.Enums;
using HMS.API.Abstraction.Exceptions;
using HMS.API.Abstraction.Interfaces.MedicalRecord;
using log4net;
using Microsoft.AspNetCore.Http;

namespace HMS.API.Services.Services.MedicalRecord
{
    public class MedicalRecordValidationService : IMedicalRecordValidationService
    {
        private readonly IMedicalRecordDataManager _dataManager;
        private readonly ILog _logger;

        public MedicalRecordValidationService(IMedicalRecordDataManager dataManager)
        {
            _dataManager = dataManager;
            _logger = LogManager.GetLogger(typeof(MedicalRecordValidationService));
        }

        public async Task ValidateMedicalRecord(int medicalRequestId)
        {
            _logger.Info($"ValidateMedicalRecord called in ValidationService with ID: {medicalRequestId}");
            var medicalrecord = await _dataManager.GetMedicalRecordById(medicalRequestId);
            if (medicalrecord == null)
            {
                throw new MedicalRecordException("Medical Record Not Found", (int)ErrorCodes.MedicalRecordNotFound, (int)StatusCodes.Status404NotFound);
            }
        }

        public async Task ValidateMedicalRecordRequest(MedicalRecordRequest medicalRecordRequest)
        {
            _logger.Info($"ValidateMedicalRecordRequest called in ValidationService with [PatientId={medicalRecordRequest.PatientId}], [DoctorId={medicalRecordRequest.DoctorId}]");
            var medicalRecordExist = await _dataManager.MedicalRecordExists(medicalRecordRequest.Diagnosis, medicalRecordRequest.Prescriptions, medicalRecordRequest.LabResults);
            if (medicalRecordExist)
            {
                throw new MedicalRecordException("Medical Record already exist", (int)ErrorCodes.MedicalRecordIsExist, (int)StatusCodes.Status400BadRequest);
            }
        }

        public void Dispose()
        {
            _dataManager.Dispose();
        }
    }
}