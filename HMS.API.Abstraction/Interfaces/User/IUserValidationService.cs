namespace HMS.API.Abstraction.Interfaces.User
{
    public interface IUserValidationService : IDisposable
    {
        Task ValidateUser(int UserId);

        Task ValidateUserExistance(string userEmail, string userPassword);

        Task ValidateUserRequest(string userEmail);

        Task ValidateRole(string userRole);
    }
}