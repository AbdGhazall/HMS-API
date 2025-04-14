using HMS.API.Abstraction.Entities.User;
using HMS.API.Abstraction.Interfaces;
using HMS.API.Abstraction.Interfaces.User;
using log4net;

namespace HMS.API.Services.Services.User
{
    //here the token will be generated for the user
    public class UserService : IUserService
    {
        private readonly IUserValidationService _userValidationService;
        private readonly IUserDataManager _userDataManager;
        private readonly IJwtService _jwtService;
        private readonly ILog _logger;

        public UserService(IUserValidationService userValidationService, IUserDataManager userDataManager, IJwtService jwtService)
        {
            _userDataManager = userDataManager;
            _userValidationService = userValidationService;
            _jwtService = jwtService;
            _logger = LogManager.GetLogger(typeof(UserService));
        }

        public async Task<List<UserEntity>> GetAllUsers()
        {
            _logger.Info("GetAllUsers from Service called");
            var allUsers = await _userDataManager.GetAllUsers();
            _logger.Info("GetAllUsers from Service returned");
            return allUsers;
        }

        public async Task<UserEntity> GetUserById(int id)
        {
            _logger.Info($"GetUserById from Service called with [id={id}]");
            await _userValidationService.ValidateUser(id);
            var user = await _userDataManager.GetUserById(id);
            _logger.Info($"GetUserById from Service returned");
            return user;
        }

        public async Task UpdateUser(int id, UserUpdate updateduser)
        {
            _logger.Info($"UpdateUser from Service called with [id={id}]");
            await _userValidationService.ValidateUser(id);
            await _userDataManager.UpdateUser(id, updateduser);
            _logger.Info($"UpdateUser from Service returned");
        }

        public async Task DeleteUser(int id)
        {
            _logger.Info($"DeleteUser from Service called with [id={id}]");
            await _userValidationService.ValidateUser(id);
            await _userDataManager.DeleteUser(id);
            _logger.Info($"DeleteUser from Service returned");
        }

        public async Task<LoginDataResponse> LogIn(LoginDataRequest loginData)
        {
            _logger.Info($"LogIn from Service called with [email={loginData.Email}]");
            await _userValidationService.ValidateUserExistance(loginData.Email, loginData.Password);
            var user = await _userDataManager.GetUser(loginData.Email, loginData.Password);
            var jwtToken = _jwtService.GenerateToken(user);
            _logger.Info($"LogIn from Service returned");
            return new LoginDataResponse() { Email = user.Email, AccessToken = jwtToken };
        }

        public async Task<RegisterDataResponse> Register(RegisterDataRequest registerDataRequest)
        {
            _logger.Info($"Register from Service called with [email={registerDataRequest.Email}]");
            await _userValidationService.ValidateUserRequest(registerDataRequest.Email);
            await _userValidationService.ValidateRole(registerDataRequest.RoleName);
            await _userDataManager.RegisterNewUser(registerDataRequest);
            _logger.Info($"Register from Service returned");
            return new RegisterDataResponse() { Success = true };
        }

        public void Dispose()
        {
            _userValidationService.Dispose();
            _userDataManager.Dispose();
        }
    }
}