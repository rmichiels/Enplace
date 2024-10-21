using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Services.Managers;

namespace Enplace.Service.Services.API
{
    public class MenuAPI(IHttpClientFactory clientFactory, AsyncEventManager<Notification> eventManager) : ApiService<MenuDTO>(clientFactory, eventManager)
    {
        public async Task<List<RecipeDTO>> GetMenuRecipes(ILabeled item)
        {
            return await RequestBuilder<List<RecipeDTO>>.Create(_client, $"{item.Id}/recipes")
                .ExecuteGetAsync() ?? [];
        }

        public async Task<Dictionary<string, List<GroceryListItem>>?> GetGroceriesFor(int id)
        {
            return await RequestBuilder<Dictionary<string, List<GroceryListItem>>>
                .Create(_client, $"{id}/shoppinglist")
                .ExecuteGetAsync();
        }
    }
}
