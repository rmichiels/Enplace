using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Entities;
using Enplace.Service.Services.Converters;
using Microsoft.AspNetCore.Authorization;
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
                var user = await GetUser();
                if (user is not null)
                {
                    var q = await Service.Query<Recipe>();
                    intermediary = await q.Include(r => r.Likes)
                        .Include(r => r.OwnerUser)
                        .Where(r => r.OwnerUser.Name == user.Name || r.Likes.Any(u => u.UserID == user.Id))
                        .ToListAsync() ?? [];
                }
                else
                {
                    return results;
                }
            }
            else
            {
                intermediary = await Service.GetAll<Recipe>();
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

        [Route("like/{recipeid}")]
        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> ToggleLike([FromRoute] int recipeid)
        {
            var user = await GetUser();
            if (user is null)
            {
                return BadRequest("You can only like recipes while logged in.");
            }
            else
            {
                var query = await Service.Query<Recipe>();
                var recipe = await query
                .Include(r => r.Likes)
                .FirstAsync(r => r.Id == recipeid);
                UserRecipe ur = new() { UserID = user.Id, RecipeID = recipeid };
                try
                {
                    if (!recipe.Likes.Any(like => like.UserID == user.Id && like.RecipeID == recipeid))
                    {
                        await Service.Link(ur);
                        if (user is not null)
                        {
                            Task.Run(() => TrackActivityFor(user, recipeid));
                        }
                        return NoContent();
                    }
                    else
                    {
                        await Service.UnLink(recipe.Likes.First(like => like.UserID == user.Id && like.RecipeID == recipeid));
                        return NoContent();
                    }
                }
                catch (Exception ex)
                {
                    return UnprocessableEntity(ex.Message);
                }
            }
        }
    }
}
