using Blazored.Modal;
using Blazored.Modal.Services;
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
    }
}
