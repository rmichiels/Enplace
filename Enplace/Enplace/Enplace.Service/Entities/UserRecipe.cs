namespace Enplace.Service.Entities
{
    public class UserRecipe
    {
        public int UserID { get; set; }
        public int RecipeID { get; set; }

        public virtual User User { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
