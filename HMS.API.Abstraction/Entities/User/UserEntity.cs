namespace HMS.API.Abstraction.Entities.User
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public int RoleId { get; set; }
    }
}