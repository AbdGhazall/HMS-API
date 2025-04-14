using System.Security.Claims;
using HMS.API.Abstraction.Entities.User;

namespace HMS.API.Abstraction.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(UserEntity userEntity);

        ClaimsPrincipal ValidateToken(string token);
    }
}