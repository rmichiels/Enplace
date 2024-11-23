namespace Enplace.Service.Entities
{
    public class UserMenuRecipe
    {
        public int MenuID { get; set; }
        public int RecipeID { get; set; }
        public decimal Scale { get; set; } = 1;
        public virtual UserMenu? Menu { get; set; }
        public virtual Recipe? Recipe { get; set; }
    }
}
