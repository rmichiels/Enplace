using System;
using System.Collections.Generic;

namespace Enplace.Service.Entities;

public partial class RecipeStep
{
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
