using Enplace.Service.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Enplace.Library.Recipes
{
    public class RecipeShardBase : ComponentBase
    {
        [Inject]
        public required NavigationManager Navigation { get; set; }
        [Parameter]
        public RecipeDTO Recipe { get; set; }
        [Parameter]
        public ComponentState State { get; set; } = ComponentState.ReadOnly;

        public void NavigateToRecipeDetails()
        {
            Navigation.NavigateTo($"/recipes/byid/{Recipe.Id}");
        }
    }
}
