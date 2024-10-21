using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Entities;
using Enplace.Service.Services.Converters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Claims;

namespace Enplace.API
{
    [AllowAnonymous]
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
        public async Task<IActionResult> QueryRecipes([FromQuery] string name, [FromQuery] bool foruser = false, [FromQuery] bool isliked = false, [FromQuery] string category = "", [FromQuery] int pagesize = 5, [FromQuery] int page = 1)
        {
            var query = await _repository.Query<Recipe>();
            query = query.IgnoreAutoIncludes()
                .Include(r => r.Category)
                .Include(r => r.OwnerUser);
            User? userToMatch = null;
            List<RecipeDTO> result = [];

            if (page < 1)
            {
                return BadRequest("Cannot select a page below the 1st");
            }

            if (foruser)
            {
                var id = User.Identity as ClaimsIdentity;
                userToMatch = await _repository.Get<User>(id.FindFirst("username")?.Value ?? string.Empty);
                if (userToMatch is null)
                {
                    return BadRequest();
                }
                if (isliked)
                {
                    query = query.Include(r => r.Users).Where(r => r.Users.Contains(userToMatch));
                }
                else
                {
                    query = query.Where(r => r.Users == userToMatch);
                }
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(r => r.Name.ToUpper().Contains(name.ToUpper()));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(r => r.Category.Name.ToUpper().Contains(category.ToUpper()));
            }

            var remainder = query.ToList().Skip(page - 1 * pagesize);
            var queryResult = remainder.Take(pagesize);
            RecipeConverter converter = new();

            foreach (var item in queryResult)
            {
                result.Add(await converter.Convert(item));
            }

            Response<RecipeDTO> response = new()
            {
                Data = result,
                Pagination = new()
                {
                    CurrentPage = pagesize,
                    PageSize = result.Count,
                    NextPage = remainder.Count() > pagesize ? page + 1 : null,
                    PreviousPage = page - 1 > 1 ? page - 1 : null,
                }
            };

            return Ok(response);
        }
    }
}
