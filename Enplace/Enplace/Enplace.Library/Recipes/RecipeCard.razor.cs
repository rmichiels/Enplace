using Blazored.Modal;
using Enplace.Library.Layout;
using Enplace.Library.Menus;
using Enplace.Service.DTO;

namespace Enplace.Library.Recipes
{
    public class RecipeCardBase : BaseTile<RecipeDTO>
    {
        public void ShowRecipeDetails()
        {
            var modalOpts = new ModalOptions() { Size = ModalSize.Large };
            var modalParams = new ModalParameters()
                .Add(nameof(RecipeEditor.Id), Item.Id)
                .Add(nameof(RecipeEditor.State), ComponentState.Details)
                .Add(nameof(RecipeEditor.Action), "details")
                .Add(nameof(RecipeEditor.Step), Item.Scale * Item.ApproximateServingSize);
            ModalService.Show<RecipeEditor>(Item.Name, modalParams, modalOpts);
        }

        public void ShowRecipeCreator()
        {
            var modalOpts = new ModalOptions() { Size = ModalSize.Large };
            var modalParams = new ModalParameters()
                .Add(nameof(RecipeEditor.State), ComponentState.Create)
                .Add(nameof(RecipeEditor.Action), "create");
            ModalService.Show<RecipeEditor>("Add New Recipe", modalParams, modalOpts);
        }

        public void ShowRecipeEditor()
        {
            var modalOpts = new ModalOptions() { Size = ModalSize.Large };
            var modalParams = new ModalParameters()
                .Add(nameof(RecipeEditor.Id), Item.Id)
                .Add(nameof(RecipeEditor.State), ComponentState.Edit)
                .Add(nameof(RecipeEditor.Action), "edit");
            ModalService.Show<RecipeEditor>("Add New Recipe", modalParams, modalOpts);
        }

        public void ShowMenuSelector()
        {
            var modalParams = new ModalParameters()
                .Add(nameof(MenuSelector.Recipe), Item);
            var options = new ModalOptions()
            {
                Size = ModalSize.Automatic,
            };
            ModalService.Show<MenuSelector>("Select menu", modalParams, options);
        }

        public async Task ToggleLike()
        {
            await API.Like(Item);
        }

        protected string _imgShardB64 => $"data:{Item?.RecipeImages?.FirstOrDefault(img => img.Size == Service.ImageSize.Header)?.MIME ?? string.Empty};base64," +
            $" {Convert.ToBase64String(Item?.RecipeImages?.FirstOrDefault(img => img.Size == Service.ImageSize.Header)?.Data ?? [])}";
    }
}
