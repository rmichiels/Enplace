using Enplace.Service.Contracts;

namespace Enplace.Service.Entities;

public partial class User : ILabeled
{
    public int Id { get; set; }
    public Guid SKID { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<Recipe> Recipes { get; set; } = [];

    public virtual ICollection<UserMenu> UserMenus { get; set; } = [];

    public virtual ICollection<Recipe> RecipesNavigation { get; set; } = [];
}
