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
            await _dbConnection.ExecuteAsync(sql, new
            {
                DesignationName = designation.DesignationName
            }, commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateDesignation(Designation designation)
        {
            string sql = "UpdateDesignation"; // Call the stored procedure
            await _dbConnection.ExecuteAsync(sql, new
            {
                DesignationId = designation.DesignationId,
                DesignationName = designation.DesignationName
            }, commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteDesignation(int DesignationId)
        {
            string sql = "DeleteDesignation"; // Call the stored procedurea
            DynamicParameters p = new DynamicParameters();
            p.Add("@DesignationId", DesignationId);
            await _dbConnection.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);
        }



        public async Task<IEnumerable<Designation>> GetDesignationsPaged(int pageNumber, int pageSize)
        {
            string sql = "SELECT * FROM dbo.fn_GetDesignationPaged(@PageNumber, @PageSize)"; // Corrected function name and model
            return await _dbConnection.QueryAsync<Designation>(sql, new
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }

        public async Task<double> GetTotalDesignationCount()
        {
            string sql = "SELECT COUNT(*) FROM Designation"; // Corrected table name
            var count = await _dbConnection.ExecuteScalarAsync<int>(sql);
            return Convert.ToDouble(count);
        }
    }
}
