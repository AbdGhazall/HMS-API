using HMS.API.Abstraction.Entities.User;

namespace HMS.API.Abstraction.Interfaces.User
{
    //the layer that will be called in the API
    public interface IUserService : IDisposable
    {
        Task<List<UserEntity>> GetAllUsers();

        Task<UserEntity> GetUserById(int id);

        Task UpdateUser(int id, UserUpdate updateduser);

        Task DeleteUser(int id);

        Task<LoginDataResponse> LogIn(LoginDataRequest loginData);

        Task<RegisterDataResponse> Register(RegisterDataRequest registerDataRequest);
    }
}