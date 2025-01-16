namespace Enplace.Service.DTO
{
    public class RecipeStepDTO
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}