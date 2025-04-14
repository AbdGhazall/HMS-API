using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HMS.API.Abstraction.Entities.User;
using HMS.API.Abstraction.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HMS.API.Services.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;
        private readonly string _secretKey;
        private readonly string _issuer;

        public JwtService(IConfiguration config)
        {
            _config = config;
            _secretKey = _config["Jwt:Key"];
            _issuer = _config["Jwt:Issuer"];
        }

        //public string GenerateToken(UserEntity userEntity)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Email, userEntity.Email),
        //        new Claim("Role", userEntity.RoleId.ToString())
        //    };

        //    if (userEntity.RoleId.ToString() != null && userEntity.RoleId.ToString().Any())
        //    {
        //        foreach (var role in userEntity.RoleId.ToString())
        //        {
        //            claims.Add(new Claim("Role", role.ToString()));
        //        }
        //    }

        //    var token = new JwtSecurityToken(
        //       _issuer,
        //       _issuer,
        //       claims,
        //       expires: DateTime.UtcNow.AddMinutes(120),
        //       signingCredentials: credentials
        //   );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        public string GenerateToken(UserEntity userEntity)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, userEntity.Email),
        new Claim(ClaimTypes.Role, userEntity.RoleId.ToString())
    };

            var token = new JwtSecurityToken(
                _issuer,
                _issuer,
                claims,
                expires: DateTime.UtcNow.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _issuer,
                ValidAudience = _issuer,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };

            return handler.ValidateToken(token, validationParameters, out _);
        }
    }
}