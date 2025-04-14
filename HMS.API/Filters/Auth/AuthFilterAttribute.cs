using System.Net;
using System.Security.Claims;
using HMS.API.Abstraction.Entities;
using HMS.API.Abstraction.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HMS.API.Filters.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var jwtService = context.HttpContext.RequestServices.GetService<IJwtService>();

            var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                SetForbiddenResult(context);
                return;
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();

            try
            {
                var principal = jwtService.ValidateToken(token);

                var issuer = principal.Claims.FirstOrDefault(a => a.Type == "iss")?.Value;
                string email = principal.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Email)?.Value;
                string role = principal.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role)?.Value;

                if (issuer != "localhost")
                {
                    SetForbiddenResult(context);
                }
            }
            catch
            {
                SetForbiddenResult(context);
            }
        }

        private void SetForbiddenResult(AuthorizationFilterContext context)
        {
            context.Result = new ObjectResult(new BaseResponseError
            {
                ErrorCode = 403,
                ErrorMessage = "You cannot access our APIs",
                MessageTime = DateTime.UtcNow
            })
            { StatusCode = (int)HttpStatusCode.Forbidden };
        }
    }
}