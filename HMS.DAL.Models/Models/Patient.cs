using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.DAL.Models.Models
{
    [Table("Patients")]
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int GenderId { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public DateTime DOB { get; set; }

        [Required, MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        public string MedicalHistory { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }
    }
}