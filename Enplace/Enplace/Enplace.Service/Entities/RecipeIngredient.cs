namespace Enplace.Service.Entities;

public partial class RecipeIngredient
{
    public int RecipeId { get; set; }

    public int IngredientId { get; set; }

    public int MeasurementId { get; set; }

    public decimal Quantity { get; set; }
    public string? Comment { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual Measurement Measurement { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
