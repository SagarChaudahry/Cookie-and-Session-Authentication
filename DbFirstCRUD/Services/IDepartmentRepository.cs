using DbFirstCRUD.Data.Entities;

namespace DbFirstCRUD.Services
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<IEnumerable<Department>> GetDepartmentsPaged(int pageNumber , int pageSize);
        Task<double> GetTotalDepartmentCount();
        Task<Department?> GetDepartmentById(int id);
        Task AddDepartment(Department department);
        Task UpdateDepartment(Department department);
        Task DeleteDepartment(int id);
    }
}


