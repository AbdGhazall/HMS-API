using System.ComponentModel.DataAnnotations;
using HMS.API.Abstraction.Entities;
using HMS.API.Abstraction.Entities.User;
using HMS.API.Abstraction.Interfaces.User;
using HMS.API.Filters;
using HMS.API.Filters.Auth;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [ApiController]
    [AuthorizeFilter("1")] // Class level
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILog _logger;

        public UserController(IUserService userService)
        {
            _userService = userService;
            _logger = LogManager.GetLogger(typeof(UserController));
        }

        [HttpGet]
        [ActionName("GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<UserEntity>> GetAllUsers([Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info("GetAllUsers endpoint called");
            var allUsers = await _userService.GetAllUsers();
            _logger.Info("GetAllUsers endpoint returned");
            return allUsers;
        }

        [HttpGet("{id}")]
        [ActionName("GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<UserEntity> GetUserById([Required] int id, [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"GetUserById endpoint called, [Id={id}]");
            var user = await _userService.GetUserById(id);
            _logger.Info($"GetUserById endpoint returned");
            return user;
        }

        [HttpPut("{id}")]
        [ActionName("Updateuser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<UserResponse> UpdateUser(
           [Required] int id,
           [Required][FromBody] UserUpdate updatedUser,
           [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"UpdateUser endpoint called, [Id={id}]");
            await _userService.UpdateUser(id, updatedUser);
            _logger.Info($"UpdateUser endpoint returned");
            return new UserResponse() { Success = true };
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UserResponse> DeleteUser([Required] int id, [Required][FromHeaderModel] HeaderInfo headerInfo)
        {
            _logger.Info($"DeleteUser endpoint called, [Id={id}]");
            await _userService.DeleteUser(id);
            _logger.Info($"DeleteUser endpoint returned");
            return new UserResponse() { Success = true };
        }
    }
}