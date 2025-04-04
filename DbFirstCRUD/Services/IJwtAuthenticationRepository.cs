using DbFirstCRUD.Data.Entities;

namespace DbFirstCRUD.Services
{
    public interface IJwtAuthenticationRepository
    {
        Task<string> GenerateTokenAsync(Users user);
        Task<Users?> ValidateUserAsync(string username, string password);
    }
}
