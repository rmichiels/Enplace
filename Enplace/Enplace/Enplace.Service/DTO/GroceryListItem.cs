using System.Text.Json.Serialization;

namespace Enplace.Service.DTO
{
    public class GroceryListItem
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("measurement")]
        public required string Measurement { get; set; }
        [JsonPropertyName("quantity")]
        public required decimal Quantity { get; set; }
    }
}
