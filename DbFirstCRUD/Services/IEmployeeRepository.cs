using DbFirstCRUD.Data.Entities;

namespace DbFirstCRUD.Services
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employees>> GetAllEmployees();
        Task<Employees?> GetEmployeeById(int id);
        Task AddEmployee(Employees employee);
        Task UpdateEmployee(Employees employee);
        Task DeleteEmployee(int id);
        
    }
}
