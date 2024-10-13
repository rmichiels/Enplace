using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Enplace.Service.DTO
{
    public class AuthRequest
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string Credentials { get; set; } = string.Empty;
        [JsonPropertyName("ApplicationSignature")]
        public Guid Signature { get; set; } = Guid.Parse("338e0e5d-16b0-42f0-b4ca-62c007dd452e");
    }
}
