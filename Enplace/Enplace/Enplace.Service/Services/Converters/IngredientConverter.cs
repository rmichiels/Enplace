using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.Service.Services.Converters
{
    public class IngredientConverter : IModelConverter<Ingredient, IngredientDTO>
    {
        public Task<Ingredient> Convert(IngredientDTO viewModel)
        {
            return Task.FromResult<Ingredient>(new() { Id = viewModel.Id, IngredientCategoryId = viewModel.CategoryId, Name = viewModel.Name });
        }

        public Task<IngredientDTO> Convert(Ingredient entity)
        {
            return Task.FromResult<IngredientDTO>(new() { Id = entity.Id, Name = entity.Name, Category = entity.IngredientCategory, CategoryId = entity.IngredientCategoryId });
        }
    }
}
