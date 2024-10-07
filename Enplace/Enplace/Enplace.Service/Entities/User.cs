﻿using Enplace.Service.Contracts;
using System.Text.Json.Serialization;

namespace Enplace.Service.Entities;

public partial class User : ILabeled
{
    public int Id { get; set; }
    public Guid SKID { get; set; }
    public string Name { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Recipe> Recipes { get; set; } = [];
    [JsonIgnore]
    public virtual ICollection<UserMenu> UserMenus { get; set; } = [];
    [JsonIgnore]
    public virtual ICollection<Recipe> RecipesNavigation { get; set; } = [];
}
