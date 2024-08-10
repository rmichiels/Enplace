using Enplace.Service.Contracts;

namespace Enplace.Service.Entities;

public partial class RecipeStep : ILabeled
{
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
