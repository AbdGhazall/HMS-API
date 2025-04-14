using System.ComponentModel.DataAnnotations;

namespace HMS.API.Abstraction.Entities.MedicalRecord
{
    public class MedicalRecordEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public string Diagnosis { get; set; } = string.Empty;

        [Required]
        public string Prescriptions { get; set; } = string.Empty;

        [Required]
        public string LabResults { get; set; } = string.Empty;
    }
}