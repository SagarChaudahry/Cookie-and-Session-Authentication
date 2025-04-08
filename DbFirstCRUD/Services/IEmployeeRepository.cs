using DbFirstCRUD.Data.Entities;

namespace DbFirstCRUD.Services
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employees>> GetAllEmployees();
        Task<IEnumerable<Employees>> GetEmployeesPaged(int pageNumber, int pageSize);
        Task<Employees?> GetEmployeeById(int id);
        Task AddEmployee(Employees employee);
        Task UpdateEmployee(Employees employee);
        Task DeleteEmployee(int id);
        Task<double> GetTotalEmployeeCount();
    }
}
