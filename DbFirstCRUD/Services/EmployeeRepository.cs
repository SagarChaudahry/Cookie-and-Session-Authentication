using Dapper;
using DbFirstCRUD.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbFirstCRUD.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
            
        public async Task<IEnumerable<Employees>> GetAllEmployees()
        {
            using (var connection = _db.CreateConnection())
            {
                string sql = "Select * from GetAllEmployees()";
                return await connection.QueryAsync<Employees>(sql);
            }
        }

        public async Task<Employees?> GetEmployeeById(int EmployeeId)
        {
            using (var connection = _db.CreateConnection())
            {
                string sql = "SELECT * FROM [GetEmployeeByEmployeeId](@EmployeeId)";
                return await connection.QueryFirstOrDefaultAsync<Employees>(sql, new { EmployeeId });
            }
        }


        public async Task AddEmployee(Employees employee)
        {
            string sql = "AddEmployee";

            using (var connection = _db.CreateConnection())
            {
                await connection.ExecuteAsync(sql, new
                {

                    Name = employee.Name,
                    Email = employee.Email,
                    DepartmentId = employee.DepartmentId,
                    DesignationId = employee.DesignationId
                },commandType:CommandType.StoredProcedure);
            }
        }

        public async Task UpdateEmployee(Employees employee)
        {
            string sql = "UPDATE Employees SET Name = @Name, Email = @Email, DepartmentId = @DepartmentId, " +
                         "DesignationId = @DesignationId WHERE EmployeeId = @EmployeeId";

            using (var connection = _db.CreateConnection())
            {
                await connection.ExecuteAsync(sql, new
                {
                    employee.Name,
                    employee.Email,
                    employee.DepartmentId,
                    employee.DesignationId,
                    employee.EmployeeId
                });
            }
        }

        public async Task DeleteEmployee(int EmployeeId)
        {
            string sql = "DELETE FROM Employees WHERE EmployeeId = @EmployeeId";
            

            using (var connection = _db.CreateConnection())
            {
                await connection.ExecuteAsync(sql, new { EmployeeId });
            }




        }
    }
    
}
