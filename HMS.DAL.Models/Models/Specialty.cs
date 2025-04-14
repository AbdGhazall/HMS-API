using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.DAL.Models.Models
{
    [Table("Specialty")]
    public class Specialty
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string DoctorSpecialty { get; set; }
    }
}