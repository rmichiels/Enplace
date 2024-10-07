using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Entities;

namespace Enplace.Service.Services.Converters
{
    public class MenuConverter : IModelConverter<UserMenu, MenuDTO>
    {
        public async Task<UserMenu?> Convert(MenuDTO? viewModel)
        {
            UserMenu entity = new() { Id = viewModel.Id, Name = viewModel.Name, Week = viewModel.Week, UserId = viewModel.UserId };
            var recipeConverter = new RecipeConverter();

            viewModel.MenuRecipes.ForEach(async r =>
            {
                var intermediary = await recipeConverter.Convert(r);
                if (intermediary != null)
                {
                    entity.MenuRecipes.Add(new() { MenuID = viewModel.Id, RecipeID = intermediary.Id });
                }
            });

            return entity;
        }

        public async Task<MenuDTO?> Convert(UserMenu? entity)
        {
            return new() { Id = entity.Id, Name = entity.Name, Week = entity.Week, UserId = entity.UserId };
        }
    }
}
