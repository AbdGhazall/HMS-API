using HMS.API.Abstraction.Entities.User;
using HMS.API.Abstraction.Interfaces.User;
using HMS.DAL.DataAccess.Utilities;
using HMS.DAL.Models.Models;
using log4net;
using Microsoft.EntityFrameworkCore;

namespace HMS.DAL.DataAccess.Managers
{
    public class UserDataManager : IUserDataManager
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILog _logger;

        public UserDataManager(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _logger = LogManager.GetLogger(typeof(UserDataManager));
        }

        public async Task<List<UserEntity>> GetAllUsers()
        {
            _logger.Info("GetAllUsers called in DataManager");
            var users = await _applicationDbContext.Users
                .AsNoTracking()
                .Select(a => new UserEntity()
                {
                    Id = a.Id,
                    Email = a.Email,
                    RoleId = a.RoleId,
                }).ToListAsync();
            _logger.Info("GetAllUsers returned in DataManager");
            return users;
        }

        public async Task UpdateUser(int id, UserUpdate updateduser)
        {
            _logger.Info($"UpdateUser called in DataManager [Id={id}]");
            var user = await _applicationDbContext.Users.FindAsync(id);
            if (user != null)
            {
                user.Email = updateduser.Email;
                var role = await _applicationDbContext.Roles
                    .Where(r => r.Name == updateduser.Role)
                    .FirstOrDefaultAsync();
                if (role != null)
                {
                    user.Role = role;
                }
                await _applicationDbContext.SaveChangesAsync();
                _logger.Info("UpdateUser returned in DataManager");
            }
        }

        public async Task DeleteUser(int id)
        {
            _logger.Info($"DeleteUser called in DataManager [Id={id}]");
            var user = await _applicationDbContext.Users.FindAsync(id);
            _applicationDbContext.Users.Remove(user);
            await _applicationDbContext.SaveChangesAsync();
            _logger.Info("DeleteUser returned in DataManager");
        }

        public async Task<UserEntity> GetUserById(int id)
        {
            _logger.Info($"GetUserById called in DataManager [Id={id}]");
            var user = await _applicationDbContext.Users
                .FindAsync(id);
            if (user != null)
            {
                var useres = user.ToUserEntity();
                _logger.Info("GetUserById returned in DataManager");
                return useres;
            }
            return null;
        }

        public async Task<UserEntity> GetUserByEmail(string email)
        {
            _logger.Info($"GetUserByEmail called in DataManager [Email={email}]");
            var user = await _applicationDbContext.Users
                .Include(r => r.Role)
                .Where(a => a.Email == email).FirstOrDefaultAsync();
            if (user != null)
            {
                var userEmail = user.ToUserEntity();
                _logger.Info("GetUserByEmail returned in DataManager");
                return userEmail;
            }
            return null;
        }

        public async Task<UserEntity> GetUser(string userEmail, string userPassword)
        {
            _logger.Info($"GetUser called in DataManager [Email={userEmail}]");
            var user = await _applicationDbContext.Users
                .Include(u => u.Role)
                .Where(a => a.Email == userEmail && a.Password == userPassword).FirstOrDefaultAsync();
            if (user != null)
            {
                var getUser = user.ToUserEntity();
                _logger.Info("GetUser returned in DataManager");
                return getUser;
            }
            return null;
        }

        public async Task<string> GetUserRole(string userRole)
        {
            _logger.Info($"GetUserRole called in DataManager [Role={userRole}]");
            var role = await _applicationDbContext.Roles
                        .Where(r => r.Name == userRole)
                        .Select(r => r.Name)
                        .FirstOrDefaultAsync();
            _logger.Info("GetUserRole returned in DataManager");
            return role;
        }

        public async Task RegisterNewUser(RegisterDataRequest registerDataRequest)
        {
            _logger.Info($"RegisterNewUser called in DataManager [Email={registerDataRequest.Email}]");
            var role = await _applicationDbContext.Roles
                            .Where(r => r.Name == registerDataRequest.RoleName)
                            .FirstOrDefaultAsync();
            var user = new User()
            {
                Password = registerDataRequest.Password,
                Email = registerDataRequest.Email,
                RoleId = role.Id
            };
            _applicationDbContext.Users.Add(user);
            await _applicationDbContext.SaveChangesAsync();
            _logger.Info("RegisterNewUser returned in DataManager");
        }

        public void Dispose()
        {
            _applicationDbContext?.Dispose();
        }
    }
}