using Enplace.Service.Contracts;
using System;
using System.Collections.Generic;

namespace Enplace.Service.Entities;

public partial class IngredientCategory : ILabeled
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Ingredient> Ingredients { get; set; } = [];
}
