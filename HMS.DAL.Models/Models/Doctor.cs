using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.DAL.Models.Models
{
    [Table("Doctors")]
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int SpecialtyId { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("SpecialtyId")]
        public virtual Specialty Specialty { get; set; }
    }
}