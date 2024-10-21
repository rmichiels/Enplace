namespace Enplace.Service.DTO
{
    public class Pagination
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int? NextPage { get; set; }
        public int? PreviousPage { get; set; }
    }
}