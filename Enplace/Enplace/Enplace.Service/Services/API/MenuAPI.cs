using Enplace.Service.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Services.Managers;
using System.Net;
using System.Net.Http.Json;

namespace Enplace.Service.Services.API
{
    public class MenuAPI(IHttpClientFactory clientFactory, AsyncEventManager<Notification> eventManager) : ApiService<MenuDTO>(clientFactory, eventManager)
    {
        public async Task<List<RecipeDTO>> GetMenuRecipes(ILabeled item)
        {
            return await RequestBuilder<List<RecipeDTO>>.Create(_client, $"{item.Id}/recipes")
                .AddResponseHandler(HttpStatusCode.OK, (r, _) => Console.WriteLine($"Fetching Recipes for Menu {item.Id} - {r?.Count} recipes found."))
                .ExecuteGetAsync() ?? [];
        }

        public async Task<Dictionary<string, List<GroceryListItem>>?> GetGroceriesFor(int id)
        {
            return await RequestBuilder<Dictionary<string, List<GroceryListItem>>>
                .Create(_client, $"{id}/shoppinglist")
                .AddResponseHandler(HttpStatusCode.RequestTimeout, (_, b) => Console.WriteLine(b.RequestMessage))
                .AddResponseHandler(HttpStatusCode.NotFound, (_, r) => Console.WriteLine($"Not Found | Handler - {r.RequestMessage} - {r.ReasonPhrase}"))
                .ExecuteGetAsync();
        }
    }
}
