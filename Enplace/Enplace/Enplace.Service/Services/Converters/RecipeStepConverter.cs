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
    public class RecipeStepConverter : IModelConverter<RecipeStep, RecipeStepDTO>
    {
        public async Task<RecipeStep?> Convert(RecipeStepDTO? viewModel)
        {
            return new() { Description = viewModel.Description, RecipeId = viewModel.RecipeId, Name = viewModel.Name, Id = viewModel.Id };
        }

        public async Task<RecipeStepDTO?> Convert(RecipeStep? entity)
        {
            return new() { Id = entity.Id, Description = entity.Description, Name = entity.Name, RecipeId = entity.RecipeId };
        }
    }
}
