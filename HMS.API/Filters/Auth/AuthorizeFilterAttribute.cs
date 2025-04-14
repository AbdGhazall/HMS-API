using System.Net;
using System.Security.Claims;
using HMS.API.Abstraction.Entities;
using HMS.API.Abstraction.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HMS.API.Filters.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeFilterAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _allowedRoles;

        public AuthorizeFilterAttribute(params string[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var jwtService = context.HttpContext.RequestServices.GetService<IJwtService>();

            var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                SetForbiddenResult(context, "Authorization header missing or invalid.");
                return;
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();

            try
            {
                var principal = jwtService.ValidateToken(token);

                var issuer = principal.Claims.FirstOrDefault(a => a.Type == "iss")?.Value;
                var email = principal.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Email)?.Value;
                var roles = principal.Claims
                                    .Where(a => a.Type == ClaimTypes.Role)
                                    .Select(a => a.Value)
                                    .ToList();

                if (issuer != "localhost")
                {
                    SetForbiddenResult(context, "Localhost is invalid.");
                    return;
                }

                if (roles == null || !roles.Any())
                {
                    SetForbiddenResult(context, "User role is missing.");
                    return;
                }

                // Check if the user has at least one of the required roles
                if (!_allowedRoles.Intersect(roles).Any())
                {
                    SetForbiddenResult(context, "You do not have permission to access this resource.");
                    return;
                }
            }
            catch
            {
                SetForbiddenResult(context, "Invalid token.");
            }
        }

        private void SetForbiddenResult(AuthorizationFilterContext context, string message)
        {
            context.Result = new ObjectResult(new BaseResponseError
            {
                ErrorCode = 403,
                ErrorMessage = message,
                MessageTime = DateTime.UtcNow
            })
            { StatusCode = (int)HttpStatusCode.Forbidden };
        }
    }
}