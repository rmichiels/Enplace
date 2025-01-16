using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Entities;

namespace Enplace.Service.Services.Converters
{
    public class RecipeStepConverter : IModelConverter<RecipeStep, RecipeStepDTO>
    {
        public Task<RecipeStep> Convert(RecipeStepDTO viewModel)
        {
            return Task.FromResult<RecipeStep>(new() { Description = viewModel.Description, RecipeId = viewModel.RecipeId, Name = viewModel.Name, Id = viewModel.Id });
        }

        public Task<RecipeStepDTO> Convert(RecipeStep entity)
        {
            return Task.FromResult(new RecipeStepDTO() { Id = entity.Id, Description = entity.Description, Name = entity.Name, RecipeId = entity.RecipeId });
        }
    }
}
