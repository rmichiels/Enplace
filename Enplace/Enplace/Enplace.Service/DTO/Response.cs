namespace Enplace.Service.DTO
{
    public class Response<TResponse>
    {
        public List<TResponse>? Data { get; set; }
        public Pagination? Pagination { get; set; }
    }
}
