using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.DAL.Models.Models
{
    [Table("AppointmentStatuses")]
    public class AppointmentStatus
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Status { get; set; }
    }
}