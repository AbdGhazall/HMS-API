using System.ComponentModel.DataAnnotations;

namespace HMS.API.Abstraction.Entities.User
{
    public class LoginDataRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}