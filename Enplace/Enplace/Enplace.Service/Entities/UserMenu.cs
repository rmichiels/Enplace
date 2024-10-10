using Enplace.Service.Contracts;
using System.Text.Json.Serialization;

namespace Enplace.Service.Entities;

public partial class UserMenu : ILabeled
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int UserId { get; set; }
    public int Week { get; set; }
    [JsonIgnore]
    public virtual User User { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<UserMenuRecipe> MenuRecipes { get; set; } = [];

}
