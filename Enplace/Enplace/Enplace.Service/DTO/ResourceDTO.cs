using Enplace.Service.Entities;

namespace Enplace.Service.DTO
{
    public class ResourceDTO
    {
        public List<RecipeCategory> RecipeCategories { get; set; } = [];
        public List<IngredientCategory> IngredientCategories { get; set; } = [];
        public List<Measurement> Measurements { get; set; } = [];
    }
}
