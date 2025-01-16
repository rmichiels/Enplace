using Enplace.Service.Contracts;
using System.Text.Json.Serialization;

namespace Enplace.Service.Entities
{
    public class RecipeCategory : ILabeled
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Recipe> Recipes { get; set; } = [];
    }
}
