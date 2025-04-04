using Dapper;
using DbFirstCRUD.Data.Entities;
using System.Data;

namespace DbFirstCRUD.Services
{
  
        public class UserRepository(IDbConnection dbConnection) : IUserRepository
        {

            private readonly IDbConnection _dbConnection = dbConnection;

            public async Task<Users?> GetByUserNameAsync(Users user)
            {
                var sql = "SELECT * FROM Users WHERE UserName = @UserName and Password = @Password";
                return await _dbConnection.QueryFirstOrDefaultAsync<Users>(sql, new { UserName = user.UserName, Password = user.Password});
            }

            public async Task AddUserAsync(Users user)
            {
                var sql = "INSERT INTO Users (UserName,Password) VALUES (@UserName,@Password)";
                await _dbConnection.ExecuteAsync(sql, user);
            }


    }
}




