
using DbFirstCRUD.Data.Entities;

namespace DbFirstCRUD.Services
{
    public interface IDesignationRepository 
    {
        Task<IEnumerable<Designation>> GetAllDesignations();
        Task<Designation?> GetDesignatioByIdAsync(int DesignationId);
        Task AddDesignation(Designation designation);
        Task UpdateDesignation(Designation designation);
        Task DeleteDesignation(int designationId);




    }
}
