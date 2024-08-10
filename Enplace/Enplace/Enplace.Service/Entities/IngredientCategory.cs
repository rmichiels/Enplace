using Enplace.Service.Contracts;

namespace Enplace.Service.Entities;

public partial class IngredientCategory : ILabeled
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Ingredient> Ingredients { get; set; } = [];
}
