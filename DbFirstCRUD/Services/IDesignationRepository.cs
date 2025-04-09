
using DbFirstCRUD.Data.Entities;

namespace DbFirstCRUD.Services
{
    public interface IDesignationRepository 
    {
        Task<IEnumerable<Designation>> GetAllDesignations();
        Task<IEnumerable<Designation>> GetDesignationsPaged(int pageNumber, int pageSize);
        Task<double> GetTotalDesignationCount();
        Task<Designation?> GetDesignatioByIdAsync(int DesignationId);
        Task AddDesignation(Designation designation);
        Task UpdateDesignation(Designation designation);
        Task DeleteDesignation(int designationId);




    }
}
