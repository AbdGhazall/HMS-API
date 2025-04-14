using System.ComponentModel.DataAnnotations;

namespace HMS.API.Abstraction.Entities.Doctor
{
    public class DoctorRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Specialty { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;
    }
}