using Enplace.Service.Contracts;

namespace Enplace.Service.Entities;

public partial class Measurement : ILabeled
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = [];
}
