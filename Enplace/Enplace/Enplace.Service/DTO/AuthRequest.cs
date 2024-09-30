using System.Text.Json.Serialization;

namespace Enplace.Service.DTO
{
    public class AuthRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Credentials { get; set; } = string.Empty;
        [JsonPropertyName("ApplicationSignature")]
        public Guid Signature { get; set; } = Guid.Parse("338e0e5d-16b0-42f0-b4ca-62c007dd452e");
    }
}
