﻿using Enplace.Service.Contracts;
using Enplace.Service.Entities;

namespace Enplace.Service.DTO
{
    public class RecipeDTO : ILabeled
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public UserDTO Owner { get; set; } = new();
        public int? ApproximateServingSize { get; set; } = 1;
        public int? ApproximateCookingTime { get; set; }
        public decimal Scale { get; set; } = 1;
        public RecipeCategory? Category { get; set; }
        public List<IngredientDTO> Ingredients { get; set; } = [];
        public List<RecipeStepDTO> RecipeSteps { get; set; } = [];
        public List<ImageDTO> RecipeImages { get; set; } = [];
    }
}
