using System;
using System.Collections.Generic;

namespace Enplace.Service.Entities;

public partial class UserMenu
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int Week { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
