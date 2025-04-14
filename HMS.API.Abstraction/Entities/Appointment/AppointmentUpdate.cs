using System.ComponentModel.DataAnnotations;

namespace HMS.API.Abstraction.Entities.Appointment
{
    public class AppointmentUpdate
    {
        [Required]
        public string AppointmentStatus { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string Notes { get; set; } = string.Empty;
    }
}