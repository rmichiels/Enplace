using Enplace.Library.Context;
using Enplace.Service.Contracts;
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
        public async Task<EnplaceContext> GetBaseContext()
        {

            return new()
            {
                IngredientCategories = _service.GetAll<IngredientCategory>().Result.ToList(),
                Measurements = _service.GetAll<Measurement>().Result.ToList(),
                User = await _service.Get<User>(1)
            };
        }
        [HttpGet]
        [Route("ingredientsPerCategory/{categoryId:int}")]
        public async Task<List<Ingredient>> GetIngredientsPerCategory(int categoryId)
        {
            var intermediary = await _service.GetAll<Ingredient>();
            return intermediary.Where(i => i.IngredientCategoryId == categoryId).ToList();
        }
    }
}
