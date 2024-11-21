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
        [JsonPropertyName("Signature")]
        public Guid Signature { get; set; } = Guid.Parse("5F25A490-5A39-47D5-AD56-EDB81CB1FB00");
    }
}
