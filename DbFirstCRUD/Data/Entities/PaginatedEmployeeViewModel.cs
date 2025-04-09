namespace DbFirstCRUD.Data.Entities
{
    public class PaginatedEmployeeViewModel
    {
        public List<Employees> Employees { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}
