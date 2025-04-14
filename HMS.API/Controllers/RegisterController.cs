using HMS.API.Abstraction.Entities.User;
using HMS.API.Abstraction.Interfaces.User;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RegisterController : ControllerBase, IDisposable
    {
        private readonly IUserService _userService;
        private readonly ILog _logger;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
            _logger = LogManager.GetLogger(typeof(RegisterController));
        }

        [AllowAnonymous] // Allows unauthenticated users to access this endpoint.
        [HttpPost]
        [ActionName("Login")]
        public async Task<LoginDataResponse> Login([FromBody] LoginDataRequest loginData)
        {
            _logger.Info($"Login endpoint called, [Email={loginData.Email}]");
            var login = await _userService.LogIn(loginData);
            _logger.Info("Login endpoint returned");
            return login;
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("Register")]
        public async Task<RegisterDataResponse> Register([FromBody] RegisterDataRequest registerDataRequest)
        {
            _logger.Info($"Register endpoint called, [Email={registerDataRequest.Email}]");
            var register = await _userService.Register(registerDataRequest);
            _logger.Info("Register endpoint returned");
            return register;
        }

        public void Dispose()
        {
            _userService?.Dispose();
        }
    }
}