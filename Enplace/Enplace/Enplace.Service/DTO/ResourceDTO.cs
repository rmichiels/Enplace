using Enplace.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.Service.DTO
{
    public class ResourceDTO
    {
        public List<RecipeCategory> RecipeCategories { get; set; } = [];
        public List<IngredientCategory> IngredientCategories { get; set; } = [];
        public List<Measurement> Measurements { get; set; } = [];
    }
}
