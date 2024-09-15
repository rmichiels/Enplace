using Enplace.Service;
using Enplace.Service.Contracts;
using Enplace.Service.Entities;

namespace Enplace.Library.Context
{
    public class EnplaceContext
    {
        public EnplaceContext()
        {

        }
        public User? User { get; set; }
        public List<Measurement> Measurements { get; set; } = [];
        public List<IngredientCategory> IngredientCategories { get; set; } = [];
        public List<Ingredient> Ingredients { get; set; } = [];
        private Dictionary<int, List<Ingredient>> _ingredientsPerCategory = [];

        public List<Ingredient> GetIngredients(int categoryId)
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
        public void CacheIngredients(int categoryId, List<Ingredient> ingredients)
        {
            _ingredientsPerCategory.TryAdd(categoryId, ingredients);
        }
    }
}
