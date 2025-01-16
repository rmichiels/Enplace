using Enplace.Service.Contracts;
using System.Text.Json.Serialization;

namespace Enplace.Service.Entities;

public partial class Measurement : ILabeled
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = [];
}
