using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Entities;
using Enplace.Service.Services;
using Enplace.Service.Services.Converters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Enplace.API
{
    [Route("api/v1/recipes/")]
    public class RecipesController(ICrudable crudService) : ApiBase<Recipe, RecipeDTO>(crudService, new RecipeConverter())
    {
    }
}
