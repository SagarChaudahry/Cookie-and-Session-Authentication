using Dapper;
using DbFirstCRUD.Data.Entities;
using System.Data;

namespace DbFirstCRUD.Services
{
    public class DesignationRepository : IDesignationRepository
    {
        private readonly IDbConnection _dbConnection;

        public DesignationRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Designation>> GetAllDesignations()
        {
            string sql = "SELECT * FROM GetAllDesignation();"; // Call the function
            return await _dbConnection.QueryAsync<Designation>(sql);
        }

        public async Task<Designation?> GetDesignatioByIdAsync(int designationId)
        {
            string sql = "SELECT * FROM GetDesignationByDesignationId(@DesignationId);"; // Use the function
            return await _dbConnection.QueryFirstOrDefaultAsync<Designation>(sql, new { DesignationId = designationId });
        }

        public async Task AddDesignation(Designation designation)
        {
            string sql = "AddDesignation"; // Call the stored procedure
            // Since DesignationId is auto-generated, we do not pass it here
            await _dbConnection.ExecuteAsync(sql, new
            {
                DesignationName = designation.DesignationName // Only pass the name
            }, commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateDesignation(Designation designation)
        {
            string sql = "UpdateDesignation"; // Call the stored procedure

            // Ensure both DesignationId and DesignationName are passed
            await _dbConnection.ExecuteAsync(sql, new
            {
                DesignationId = designation.DesignationId, // Include DesignationId
                DesignationName = designation.DesignationName // Include DesignationName
            }, commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteDesignation(int designationId)
        {
            string sql = "DeleteDesignation"; // Call the stored procedure
            // Ensure DesignationId is passed
            await _dbConnection.ExecuteAsync(sql, new { DesignationId = designationId }, commandType: CommandType.StoredProcedure);
        }
    }
}
