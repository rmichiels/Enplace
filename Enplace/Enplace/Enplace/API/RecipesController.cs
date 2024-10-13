using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Entities;
using Enplace.Service.Services.Converters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Enplace.API
{
    [Route("api/v1/recipes/")]
    public class RecipesController(ICrudable crudService) : ApiBase<Recipe, RecipeDTO>(crudService, new RecipeConverter())
    {
        [Route("list")]
        [HttpGet]
        public override async Task<ICollection<RecipeDTO>> GetAll([FromQuery] bool foruser = false)
        {
            List<RecipeDTO> results = [];
            List<Recipe> intermediary = [];

            if (foruser)
            {
                var username = await GetUserName();
                if (username != string.Empty)
                {
                    var q = await _service.Query<Recipe>();
                    intermediary = await q.Include(r => r.Users)
                        .Include(r => r.OwnerUser)
                        .Where(r => r.OwnerUser.Name == username || r.Users.Any(u => u.Name == username))
                        .ToListAsync() ?? [];
                }
                else
                {
                    return results;
                }
            }
            else
            {
                intermediary = await _service.GetAll<Recipe>();
            }

            foreach (Recipe item in intermediary)
            {
                var DTO = await _converter.Convert(item);
                if (DTO is not null)
                {
                    results.Add(DTO);
                }
            }
            return results;
        }
    }
}
