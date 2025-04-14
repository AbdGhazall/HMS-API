namespace HMS.API.Abstraction.Enums
{
    public enum ErrorCodes
    {
        InvalidUserId = 1,
        InvalidUserEmail = 2,
        UserNotExist = 3,
        PatientNotFound = 4,
        PatientIsExist = 5,
        DoctorNotFound = 6,
        DoctorIsExist = 7,
        AppoitmentNotFound = 8,
        AppoitmentIsExist = 9,
        MedicalRecordNotFound = 10,
        MedicalRecordIsExist = 11,
        InvalidGender = 12,
        InvalidUserRole = 13,
        InvalidSpecialty = 14,
        InvalidStatus = 15,
        InvalidDateOfBirth = 16,
        InvalidAppointmentDate = 17,
        BillNotFound = 18,
        BillAlreadyExists = 19,
        InvalidBillStatus = 20,
    }
}