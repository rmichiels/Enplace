using Enplace.Service.Contracts;

namespace Enplace.Service.DTO
{
    public class UserDTO : ILabeled
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}