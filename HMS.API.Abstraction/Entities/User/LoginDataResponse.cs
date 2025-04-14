namespace HMS.API.Abstraction.Entities.User
{
    public class LoginDataResponse
    {
        public string Email { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;
    }
}