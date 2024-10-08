using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Enplace.Service.DTO
{
    public class GroceryListItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("measurement")]
        public string Measurement { get; set; }
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
    }
}
