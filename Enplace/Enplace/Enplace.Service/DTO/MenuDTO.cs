using Enplace.Service.Contracts;

namespace Enplace.Service.DTO
{
    public class MenuDTO : ILabeled
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Week { get; set; }
        public int UserId { get; set; }
        public List<RecipeDTO> MenuRecipes { get; set; } = [];
    }
}
