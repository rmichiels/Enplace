using Enplace.Library.Context;
using Enplace.Service;
using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Enplace.API
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private ICrudable _service { get; set; }
        public ConfigurationController(ICrudable crudService)
        {
            _service = crudService;
        }

        [HttpGet]
        [Route("ingredientsPerCategory/{categoryId:int}")]
        public async Task<List<Ingredient>> GetIngredientsPerCategory(int categoryId)
        {
            var intermediary = await _service.GetAll<Ingredient>();
            return intermediary.Where(i => i.IngredientCategoryId == categoryId).ToList();
        }

        [HttpGet]
        [Route("resourcebase")]
        public async Task<IActionResult> GetResourceBase()
        {
            var resourceBase = new ResourceDTO
            {
                IngredientCategories = await _service.GetAll<IngredientCategory>(),
                RecipeCategories = await _service.GetAll<RecipeCategory>(),
                Measurements = await _service.GetAll<Measurement>()
            };

            return Ok(resourceBase);
        }
    }
}
