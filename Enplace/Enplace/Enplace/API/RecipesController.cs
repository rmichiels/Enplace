using Enplace.Service.Contracts;
using Enplace.Service.Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.API
{
    [Route("api/v1/recipes/")]
    public class RecipesController(ICrudable crudService, IModelConverter<Recipe, Recipe> modelConverter) : ApiBase<Recipe, Recipe>(crudService, modelConverter)
    {
    }
}
