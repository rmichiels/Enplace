using Enplace.Service.Contracts;
using System;
using System.Collections.Generic;

namespace Enplace.Service.Entities;

public partial class Ingredient : ILabeled
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int IngredientCategoryId { get; set; }

    public virtual IngredientCategory IngredientCategory { get; set; } = null!;

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = [];
}
