using Enplace.Service.Contracts;
using Enplace.Service.Entities;
using System.Text.Json.Serialization;

namespace Enplace.Service.DTO
{
    public class IngredientDTO : ILabeled
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("ingredientCategoryId")]
        public int CategoryId { get; set; }
        [JsonPropertyName("ingredientCategory")]
        public IngredientCategory? Category { get; set; }
        public int MeasurementId { get; set; }
        public Measurement? Measurement { get; set; }
        public decimal Quantity { get; set; }
        public string? Comment { get; set; }
    }
}
