using Blazored.Modal;
using Enplace.Library.Layout;
using Enplace.Library.Menus;
using Enplace.Service.DTO;

namespace Enplace.Library.Recipes
{
    public class RecipeCardBase : BaseTile<RecipeDTO>
    {
        public void NavigateToRecipeDetails()
        {
            Navigation.NavigateTo($"/recipes/{Item?.Id}/details");
        }

        public void ShowRecipeCreator()
        {
            var modalParams = new ModalParameters()
                .Add(nameof(RecipeEditor.State), ComponentState.Create)
                .Add(nameof(RecipeEditor.Action), "create");
            Modal.Show<RecipeEditor>("Add New Recipe", modalParams);
        }

        public void ShowMenuSelector()
        {
            var modalParams = new ModalParameters()
                .Add(nameof(MenuSelector.Recipe), Item);
            var options = new ModalOptions()
            {
                Size = ModalSize.Small,
            };
            Modal.Show<MenuSelector>("Select menu", modalParams, options);
        }

        protected string _imgShardB64 => $"data:{Item?.RecipeImages?.FirstOrDefault(img => img.Size == Service.ImageSize.Header)?.MIME ?? string.Empty};base64," +
            $" {Convert.ToBase64String(Item?.RecipeImages?.FirstOrDefault(img => img.Size == Service.ImageSize.Header)?.Data ?? [])}";
    }
}
