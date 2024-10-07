using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Entities;
using Enplace.Service.Services.Converters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Enplace.API
{
    [Authorize]
    [Route("api/v1/menus/")]
    public class MenuController : ApiBase<UserMenu, MenuDTO>
    {
        public MenuController(ICrudable crudService, IModelConverter<UserMenu, MenuDTO> modelConverter) : base(crudService, modelConverter)
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

        [Route("list")]
        [HttpGet]
        public override async Task<ICollection<MenuDTO>> GetAll()
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
        public override async Task<Exception?> Update(MenuDTO DTO)
        {
            var entity = await _converter.Convert(DTO);
            var q = await _service.Query<UserMenu>();
            var dbEntity = q.Include(um => um.MenuRecipes).First(um => um.Id == entity.Id);

            if (entity is not null)
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
                return null;
            }
            else
            {
                return new Exception("Entity Conversion Failed & Resulted in 'null'");
            }
        }
    }
}
