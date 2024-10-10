using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Enplace.API
{
    [Authorize(Policy = "config:lo")]
    [Route("api/v1/ingredients/")]
    public class IngredientsController : ApiBase<Ingredient, IngredientDTO>
    {
        public IngredientsController(ICrudable crudService, IModelConverter<Ingredient, IngredientDTO> modelConverter) : base(crudService, modelConverter)
        {
        }
    }
}
