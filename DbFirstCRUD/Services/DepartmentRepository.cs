using Dapper;
using DbFirstCRUD.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbFirstCRUD.Services
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _db;

        public DepartmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            using (var connection = _db.CreateConnection())
            {
                string sql = "SELECT * FROM GetAllDepartments();"; 
                return await connection.QueryAsync<Department>(sql);
            }
        }

        public async Task<Department?> GetDepartmentById(int id)
        {
            using (var connection = _db.CreateConnection())
            {
                string sql = "SELECT * FROM GetDepartmentByDepartmentId(@DepartmentId);"; 
                return await connection.QueryFirstOrDefaultAsync<Department>(sql, new { DepartmentId = id });
            }
        }

        public async Task AddDepartment(Department department)
        {
            string sql = "AddDepartment";

            using (var connection = _db.CreateConnection())
            {
                await connection.ExecuteAsync(sql, new
                {
                    Name = department.Name 
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateDepartment(Department department)
        {
            string sql = "UpdateDepartment"; 

            using (var connection = _db.CreateConnection())
            {
                await connection.ExecuteAsync(sql, new
                {
                    DepartmentId = department.DepartmentId,
                    Name = department.Name 
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteDepartment(int id)
        {
            string sql = "DeleteDepartment"; 

            using (var connection = _db.CreateConnection())
            {
                await connection.ExecuteAsync(sql, new { DepartmentId = id }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
