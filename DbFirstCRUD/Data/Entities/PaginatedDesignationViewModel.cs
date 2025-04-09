namespace DbFirstCRUD.Data.Entities
{
    public class PaginatedDesignationViewModel
    {
        public List<Designation> Designations { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}
