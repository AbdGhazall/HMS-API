using System.ComponentModel.DataAnnotations;

namespace HMS.API.Abstraction.Entities.MedicalRecord
{
    public class MedicalRecordUpdate
    {
        [Required]
        public string Diagnosis { get; set; } = string.Empty;

        [Required]
        public string Prescriptions { get; set; } = string.Empty;

        [Required]
        public string LabResults { get; set; } = string.Empty;
    }
}