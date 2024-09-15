using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Entities;

namespace Enplace.Service.Services.Converters
{
    public class IngredientConverter : IModelConverter<RecipeIngredient, IngredientDTO>
    {
        public async Task<RecipeIngredient?> Convert(IngredientDTO? viewModel)
        {
            return new RecipeIngredient()
            {
                RecipeId = viewModel.RecipeId,
                IngredientId = viewModel.Id,
                MeasurementId = viewModel.MeasurementId,
                Quantity = viewModel.Quantity,
                Comment = viewModel.Comment
            };
        }

        public async Task<IngredientDTO?> Convert(RecipeIngredient? entity)
        {
            return new IngredientDTO()
            {
                Id = entity.IngredientId,
                Name = entity.Ingredient.Name,
                RecipeId = entity.RecipeId,
                Category = entity.Ingredient.IngredientCategory,
                MeasurementId = entity.MeasurementId,
                Measurement = entity.Measurement,
                Quantity = entity.Quantity,
                Comment = entity.Comment,
            };
        }
    }
}