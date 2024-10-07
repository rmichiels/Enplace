using Blazored.Modal;
using Blazored.Modal.Services;
using Enplace.Library.Menus;
using Enplace.Service.DTO;
using Microsoft.AspNetCore.Components;

namespace Enplace.Library.Recipes
{
    public class RecipeShardBase : ComponentBase
    {
        [Inject]
        public required NavigationManager Navigation { get; set; }
        [CascadingParameter] public IModalService Modal { get; set; } = default!;
        [Parameter]
        public RecipeDTO? Recipe { get; set; }
        [Parameter]
        public ComponentState State { get; set; } = ComponentState.Details;

        public void NavigateToRecipeDetails()
        {
            Navigation.NavigateTo($"/recipes/{Recipe?.Id}/details");
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
                .Add(nameof(MenuSelector.Recipe), Recipe);
            var options = new ModalOptions()
            {
                Size = ModalSize.Small,
            };
            Modal.Show<MenuSelector>("Select menu", modalParams, options);
        }

        protected string _imgShardB64 => $"data:{Recipe.RecipeImages.FirstOrDefault(img => img.Size == Service.ImageSize.Header)?.MIME ?? string.Empty};base64," +
            $" {Convert.ToBase64String(Recipe.RecipeImages.FirstOrDefault(img => img.Size == Service.ImageSize.Header)?.Data ?? [])}";
    }
}
