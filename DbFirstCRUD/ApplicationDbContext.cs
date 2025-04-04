using System.Data;
using Microsoft.Data.SqlClient;

namespace DbFirstCRUD
{
    public class ApplicationDbContext
    {

        private readonly string? _connectionString;
        public ApplicationDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        internal async Task<int> ExecuteAsync(string sql, object employees)
        {
            throw new NotImplementedException();
        }
    }
}
