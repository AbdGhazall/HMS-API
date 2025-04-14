using HMS.API.Abstraction.Enums;
using HMS.API.Abstraction.Exceptions;
using HMS.API.Abstraction.Interfaces.User;
using log4net;
using Microsoft.AspNetCore.Http;

namespace HMS.API.Services.Services.User
{
    public class UserValidationService : IUserValidationService
    {
        private readonly IUserDataManager _userDataManager;
        private readonly ILog _logger;

        public UserValidationService(IUserDataManager userDataManager)
        {
            _userDataManager = userDataManager;
            _logger = LogManager.GetLogger(typeof(UserValidationService));
        }

        public async Task ValidateUser(int UserId)
        {
            _logger.Info($"ValidateUser called in ValidationService with ID: {UserId}");
            var user = await _userDataManager.GetUserById(UserId);
            if (user == null)
            {
                throw new UserException("User Not Found", (int)ErrorCodes.InvalidUserId, (int)StatusCodes.Status400BadRequest);
            }
        }

        public async Task ValidateUserExistance(string userEmail, string userPassword)
        {
            _logger.Info($"ValidateUserExistance called in ValidationService with email: {userEmail}");
            var existingUser = await _userDataManager.GetUser(userEmail, userPassword);
            if (existingUser == null)
            {
                throw new UserException("User is not exist", (int)ErrorCodes.UserNotExist, (int)StatusCodes.Status404NotFound);
            }
        }

        public async Task ValidateUserRequest(string userEmail)
        {
            _logger.Info($"ValidateUserRequest called in ValidationService with email: {userEmail}");
            var currentUser = await _userDataManager.GetUserByEmail(userEmail);
            if (currentUser != null)
            {
                throw new UserException("User Email is already exist", (int)ErrorCodes.InvalidUserEmail, (int)StatusCodes.Status400BadRequest);
            }
        }

        public async Task ValidateRole(string userRole)
        {
            _logger.Info($"ValidateRole called in ValidationService with role: {userRole}");
            var role = await _userDataManager.GetUserRole(userRole);
            if (role == null)
            {
                throw new UserException("Invalid role specified.",
                                        (int)ErrorCodes.InvalidUserRole,
                                        (int)StatusCodes.Status400BadRequest);
            }
        }

        public void Dispose()
        {
            _userDataManager.Dispose();
        }
    }
}