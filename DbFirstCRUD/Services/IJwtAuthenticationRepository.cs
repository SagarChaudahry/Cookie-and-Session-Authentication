using System.Security.Claims;
using DbFirstCRUD.Data.Entities;

namespace DbFirstCRUD.Services
{
    public interface IJwtAuthenticationRepository
    {
        Task<string> GenerateTokenAsync(Users user);
        ClaimsPrincipal GetClaimsFromToken(string token);
        bool ValidateToken(string Token);
        Task<Users?> ValidateUserAsync(string username, string password);
    }
}
