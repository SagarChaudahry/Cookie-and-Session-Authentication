using System.Security.Claims;
using DbFirstCRUD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DbFirstCRUD.CustomJwtFilter
{
    
    public class JwtAuthorizeFilter:IAuthorizationFilter
    {
        private readonly IJwtAuthenticationRepository _jwtRepo;
        


        public JwtAuthorizeFilter(IJwtAuthenticationRepository jwtRepo)
        {
            _jwtRepo = jwtRepo;
              
        }

        

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token) || !_jwtRepo.ValidateToken(token))
            {
                context.Result = new RedirectToActionResult("Login", "JwtAuth", null);
                return;
            }

            // Extract claims from the token
            //var claimsPrincipal = _jwtRepo.GetClaimsFromToken(token);
            //var userRoles = claimsPrincipal.Claims
            //    .Where(c => c.Type == ClaimTypes.Role)
            //    .Select(c => c.Value)
            //    .ToList();

            //// Check if user has any of the required roles
            //if (_roles.Length > 0 && !_roles.Any(role => userRoles.Contains(role)))
            //{
            //    context.Result = new ForbidResult(); // User does not have access
            //}
        }
    }
}
