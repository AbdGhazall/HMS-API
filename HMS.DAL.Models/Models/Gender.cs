using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.DAL.Models.Models
{
    [Table("Genders")]
    public class Gender
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string GenderType { get; set; } = string.Empty;
    }
}