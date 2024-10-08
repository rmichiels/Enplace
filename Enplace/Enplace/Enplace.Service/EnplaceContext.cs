using Enplace.Service.DTO;
using Enplace.Service.Entities;

namespace Enplace.Service
{
    public static class EnplaceContext
    {
        public static UserDTO User { get; set; }
        public static MenuDTO Menu { get; set; }
        public static string Token { get; set; }
        public static List<Measurement> Measurements { get; set; } = [];
        public static List<IngredientCategory> IngredientCategories { get; set; } = [];
        public static List<Ingredient> Ingredients { get; set; } = [];
        private static Dictionary<int, List<Ingredient>> _ingredientsPerCategory = [];
        public static DateTime TimeStamp { get; set; }
        public static List<RecipeCategory> RecipeCategories { get; set; } = [];

        public static List<Ingredient> GetIngredients(int categoryId)
        {
            if (_ingredientsPerCategory.TryGetValue(categoryId, out List<Ingredient>? payload))
            {
                return payload;
            }
            else
            {
                throw new DataNotCachedException();
            }
        }
        public static void CacheIngredients(int categoryId, List<Ingredient> ingredients)
        {
            _ingredientsPerCategory.TryAdd(categoryId, ingredients);
        }
    }
}
