
namespace DbFirstCRUD.Data.Entities
{
    public class Designation
    {
        public int? DesignationId { get; set; }
        public string DesignationName { get; set; } = string.Empty;

        internal static List<Department> ToList()
        {
            throw new NotImplementedException();
        }
    }
}
