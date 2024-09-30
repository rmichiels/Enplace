using Enplace.Service.Contracts;
using Enplace.Service.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Enplace.API
{
    [Route("res/")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly ICrudable _repository;
        public ResourceController(ICrudable repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("q/ingredients")]
        public async Task<IActionResult> QueryIngredients([FromQuery] string name)
        {
            return Ok(await _repository.GetWhere<Ingredient>(i => i.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase)));
        }

        [HttpGet]
        [Route("q/recipes")]
        public async Task<IActionResult> QueryRecipes([FromQuery] string name, [FromQuery] string category)
        {
            return null;
        }
    }
}
