using HMS.API.Abstraction.Entities.User;

namespace HMS.API.Abstraction.Interfaces.User
{
    public interface IUserDataManager : IDisposable
    {
        Task<List<UserEntity>> GetAllUsers();

        Task<UserEntity> GetUserById(int id);

        Task UpdateUser(int id, UserUpdate updateduser);

        Task DeleteUser(int id);

        Task RegisterNewUser(RegisterDataRequest registerDataRequest);

        Task<UserEntity> GetUserByEmail(string email);

        Task<UserEntity> GetUser(string UserEmail, string UserPassword);

        Task<string> GetUserRole(string userRole);
    }
}