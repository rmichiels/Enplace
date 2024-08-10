using Enplace.Service.Contracts;
using System;
using System.Collections.Generic;

namespace Enplace.Service.Entities;

public partial class User : ILabeled
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Recipe> Recipes { get; set; } = [];

    public virtual ICollection<UserMenu> UserMenus { get; set; } = [];

    public virtual ICollection<Recipe> RecipesNavigation { get; set; } = [];
}
