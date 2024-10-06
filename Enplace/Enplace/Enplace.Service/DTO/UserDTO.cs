using Enplace.Service.Contracts;
using System.Text.Json.Serialization;

namespace Enplace.Service.DTO
{
    public class UserDTO : ILabeled
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
    }
}