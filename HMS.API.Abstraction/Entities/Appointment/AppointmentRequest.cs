using System.ComponentModel.DataAnnotations;

namespace HMS.API.Abstraction.Entities.Appointment
{
    public class AppointmentRequest
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public string AppointmentStatus { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string Notes { get; set; } = string.Empty;
    }
}