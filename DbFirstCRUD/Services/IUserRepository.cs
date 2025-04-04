using DbFirstCRUD.Data.Entities;

namespace DbFirstCRUD.Services
{
    public interface IUserRepository
    {
        Task<Users?> GetByUserNameAsync(Users user);
        Task AddUserAsync(Users user);
    }
}
