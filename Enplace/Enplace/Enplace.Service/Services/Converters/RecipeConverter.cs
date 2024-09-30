using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Entities;

namespace Enplace.Service.Services.Converters
{
    public class RecipeConverter : IModelConverter<Recipe, RecipeDTO>
    {
        private readonly UserConverter _userConverter = new();
        private readonly IngredientConverter _ingredientConverter = new();
        private readonly RecipeStepConverter _stepConverter = new();
        public async Task<Recipe?> Convert(RecipeDTO? viewModel)
        {
            if (viewModel == null) return null;
            var recipe = new Recipe()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                OwnerUserId = viewModel.Owner.Id,
                ApproximateCookingTime = viewModel.ApproximateCookingTime,
                ApproximateServingSize = viewModel.ApproximateServingSize,
                RecipeCategoryId = viewModel.Category.Id,
                Comment = viewModel.Comment
            };

            foreach (IngredientDTO dto in viewModel.Ingredients)
            {
                var recipeIngredient = await _ingredientConverter.Convert(dto);
                if (recipeIngredient != null)
                {
                    recipeIngredient.RecipeId = viewModel.Id;
                    recipe.RecipeIngredients.Add(recipeIngredient);
                };
            };

            foreach (RecipeStepDTO dto in viewModel.RecipeSteps)
            {
                var recipeStep = await _stepConverter.Convert(dto);
                if (recipeStep != null) { recipe.RecipeSteps.Add(recipeStep); };
            };

            foreach (RecipeImage recipeImage in viewModel.RecipeImages)
            {
                recipeImage.RecipeId = viewModel.Id;
                recipe.RecipeImages.Add(recipeImage);
            }

            return recipe;
        }

        public async Task<RecipeDTO?> Convert(Recipe? entity)
        {
            if (entity == null) return null;
            RecipeDTO dto = new()
            {
                Id = entity.Id,
                Name = entity.Name,
                ApproximateCookingTime = entity.ApproximateCookingTime,
                ApproximateServingSize = entity.ApproximateServingSize,
                Owner = await _userConverter.Convert(entity.OwnerUser),
                Category = entity.Category,
                Comment = entity.Comment
            };
            foreach (RecipeIngredient recipeIngredient in entity.RecipeIngredients)
            {
                var ingredientDTO = await _ingredientConverter.Convert(recipeIngredient);
                if (ingredientDTO is not null)
                {
                    dto.Ingredients.Add(ingredientDTO);
                }
            }

            foreach (RecipeStep recipeStep in entity.RecipeSteps)
            {
                var step = await _stepConverter.Convert(recipeStep);
                if (step != null) { dto.RecipeSteps.Add(step); };
            };

            dto.RecipeImages = entity.RecipeImages.ToList();

            return dto;
        }
    }
}
