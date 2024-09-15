using Enplace.Library.Context;
using Enplace.Service.DTO;
using Enplace.Service.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Enplace.Library.Ingredients
{
    public class IngredientListBase : ComponentBase
    {
        [Inject]
        public EnplaceContext ApplicationContext { get; set; }
        [CascadingParameter]
        public EditContext EditContext { get; set; }
        [Parameter]
        public List<IngredientDTO> Ingredients { get; set; } = [];
        [Parameter]
        public EventCallback<List<IngredientDTO>> IngredientsChanged { get; set; }
        [Parameter]
        public ComponentState State { get; set; }
        [Parameter]
        public EventCallback<ComponentState> StateChanged { get; set; }

        public Dictionary<IngredientCategory, List<IngredientDTO>> IngredientsPerCategory { get; set; } = [];
        protected IngredientDTO NewIngredient { get; set; } = new() { Id = 0 };

        protected virtual async void Callback()
        {
            await IngredientsChanged.InvokeAsync(Ingredients);
            StateHasChanged();
        }

        protected virtual async void AddIngredientAsync(IngredientDTO ingredient)
        {
            Console.WriteLine(ingredient.Id);
            Console.WriteLine(NewIngredient.Id);
            Ingredients.Add(ingredient);
            NewIngredient = new() { Id = 0 };
            await IngredientsChanged.InvokeAsync(Ingredients);
            StateHasChanged();
        }
    }
}
