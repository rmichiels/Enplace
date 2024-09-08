﻿using Enplace.Service.Contracts;
using Enplace.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.Service.DTO
{
    public class RecipeDTO : ILabeled
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserDTO Owner { get; set; }
        public int? ApproximateServingSize { get; set; }
        public int? ApproximateCookingTime { get; set; }
        public RecipeCategory Category { get; set; }
        public List<IngredientDTO> Ingredients { get; set; } = [];
        public List<RecipeStepDTO> RecipeSteps { get; set; } = [];
    }
}
