using System.ComponentModel.DataAnnotations;

namespace HMS.API.Abstraction.Entities.Doctor
{
    public class DoctorEntity
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int SpecialtyId { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty; // from User Table
    }
}