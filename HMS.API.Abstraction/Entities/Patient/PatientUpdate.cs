using System.ComponentModel.DataAnnotations;

namespace HMS.API.Abstraction.Entities.Patient
{
    public class PatientUpdate
    {
        [Required, MaxLength(50)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required, MaxLength(9)]
        public string Phone { get; set; } = string.Empty;

        public string MedicalHistory { get; set; } = string.Empty;
    }
}