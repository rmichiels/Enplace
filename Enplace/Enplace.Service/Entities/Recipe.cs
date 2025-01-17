﻿using Enplace.Service.Contracts;
using System.Text.Json.Serialization;

namespace Enplace.Service.Entities;

public partial class Recipe : ILabeled
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int OwnerUserId { get; set; }

    public int? ApproximateServingSize { get; set; }

    public int? ApproximateCookingTime { get; set; }
    public string? Description { get; set; }
    public virtual RecipeCategory Category { get; set; }
    public int RecipeCategoryId { get; set; } = 1;

    public virtual User OwnerUser { get; set; } = null!;
    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = [];

    public virtual ICollection<RecipeStep> RecipeSteps { get; set; } = [];
    public virtual ICollection<RecipeImage> RecipeImages { get; set; } = [];
    [JsonIgnore]
    public virtual ICollection<UserMenuRecipe> MenuRecipes { get; set; } = [];
    [JsonIgnore]
    public virtual ICollection<UserRecipe> Likes { get; set; } = [];
}
