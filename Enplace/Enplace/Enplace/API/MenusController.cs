using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Entities;
using Enplace.Service.Services.Converters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace Enplace.API
{
    [Authorize]
    [Route("api/v1/menus/")]
    public class MenusController : ApiBase<UserMenu, MenuDTO>
    {
        public MenusController(ICrudable crudService, IModelConverter<UserMenu, MenuDTO> modelConverter) : base(crudService, modelConverter)
        {
        }
        [HttpGet]
        [Route("{id}/recipes")]
        public async Task<List<RecipeDTO>> GetMenuRecipes(int id)
        {
            List<RecipeDTO> returnRecipes = [];
            RecipeConverter recipeConverter = new();

            var query = await _service.Query<Recipe>();
            var recipes = await query.Include(r => r.MenuRecipes).Where(r => r.MenuRecipes.Any(m => m.MenuID == id)).ToListAsync();
            recipes.ForEach(async recipe =>
            {
                var recipeToAdd = await recipeConverter.Convert(recipe);
                if (recipeToAdd != null)
                {
                    returnRecipes.Add(recipeToAdd);
                }
            });

            return returnRecipes;
        }

        [HttpGet]
        [Route("{id}/shoppinglist")]
        [SwaggerResponse(200, "Request Executed w/o any issues.")]
        [SwaggerResponse(404, "Either the menu does not exist, or there are no recipes associated w/ the menu.")]
        public async Task<IActionResult> GetMenuShoppingList(int id)
        {
            Dictionary<string, List<GroceryListItem>> shoppingList = [];
            var query = await _service.Query<Recipe>();
            var recipes = await query
                .Include(r => r.MenuRecipes)
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .Where(r => r.MenuRecipes.Any(m => m.MenuID == id)).ToListAsync();

            // Early return if no recipes are found/menu doesn't exist
            if (recipes.Count == 0)
            {
                return NotFound();
            }

            RecipeConverter recipeConverter = new();
            var recipeIngredients = new List<IngredientDTO>();
            recipes.ForEach(async recipe =>
            {
                var recipeToAdd = await recipeConverter.Convert(recipe);
                if (recipeToAdd != null)
                {
                    recipeIngredients.AddRange(recipeToAdd.Ingredients);
                }
            });

            var itemsWithCategory = recipeIngredients.GroupBy(ri => new
            {
                ri.Measurement,
                ri.Category,
                ri.Name
            }).Select(group => new
            {
                Item = new GroceryListItem()
                {
                    Name = group.Key.Name,
                    Measurement = group.Key.Measurement.Name,
                    Quantity = group.Sum(ri => ri.Quantity)
                },
                Category = group.Key.Category.Name,
            }).ToList();

            foreach (string cat in itemsWithCategory.Select(item => item.Category).Distinct().ToList())
            {
                var itemsForCategory = itemsWithCategory.Where(item => item.Category == cat).Select(i => i.Item).ToList();
                shoppingList.Add(cat, itemsForCategory);
            }

            return Ok(shoppingList);
        }


        [Route("list")]
        [HttpGet]
        public override async Task<ICollection<MenuDTO>> GetAll([FromQuery] bool foruser = false)
        {
            List<MenuDTO> results = [];
            var id = User.Identity as ClaimsIdentity;
            var response = await _service.Get<User>(id.FindFirst("username")?.Value ?? string.Empty);
            var intermediary = await _service.GetWhere<UserMenu>(m => m.UserId == response?.Id);
            foreach (UserMenu item in intermediary)
            {
                var DTO = await _converter.Convert(item);
                if (DTO is not null)
                {
                    results.Add(DTO);
                }
            }
            return results;
        }

        [HttpPatch]
        public override async Task<IActionResult> Update(MenuDTO DTO)
        {
            var entity = await _converter.Convert(DTO);
            var q = await _service.Query<UserMenu>();
            var dbEntity = q.Include(um => um.MenuRecipes).First(um => um.Id == entity.Id);

            try
            {
                await _service.Update(entity);
                foreach (UserMenuRecipe umr in entity.MenuRecipes)
                {
                    // if no match w/ existing collection, add record
                    if (!dbEntity.MenuRecipes.Any(e => e.MenuID == umr.MenuID && e.RecipeID == umr.RecipeID))
                    {
                        await _service.Link(umr);
                    }
                }

                foreach (UserMenuRecipe source in dbEntity.MenuRecipes)
                {
                    // if no match w/ incoming collection, remove record
                    if (!entity.MenuRecipes.Any(e => e.MenuID == source.MenuID && e.RecipeID == source.RecipeID))
                    {
                        await _service.UnLink(source);
                    }
                }
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
