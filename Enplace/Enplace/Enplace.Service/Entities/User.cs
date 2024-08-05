using System;
using System.Collections.Generic;

namespace Enplace.Service.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual ICollection<UserMenu> UserMenus { get; set; } = new List<UserMenu>();

    public virtual ICollection<Recipe> RecipesNavigation { get; set; } = new List<Recipe>();
}
