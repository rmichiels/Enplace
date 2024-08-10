using Enplace.Service.Contracts;
using System;
using System.Collections.Generic;

namespace Enplace.Service.Entities;

public partial class Recipe : ILabeled
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int OwnerUserId { get; set; }

    public int? ApproximateServingSize { get; set; }

    public int? ApproximateCookingTime { get; set; }

    public virtual User OwnerUser { get; set; } = null!;

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = [];

    public virtual ICollection<RecipeStep> RecipeSteps { get; set; } = [];

    public virtual ICollection<UserMenu> Menus { get; set; } = [];

    public virtual ICollection<User> Users { get; set; } = [];
}
