using System.ComponentModel.DataAnnotations;

namespace HMS.API.Abstraction.Entities.User
{
    public class RegisterDataRequest
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string RoleName { get; set; } = string.Empty;
    }
}