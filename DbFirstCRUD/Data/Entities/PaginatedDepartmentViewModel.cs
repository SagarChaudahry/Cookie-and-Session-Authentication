namespace DbFirstCRUD.Data.Entities
{
    public class PaginatedDepartmentViewModel
    {

        public List<Department> Departments { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }

    }
}
