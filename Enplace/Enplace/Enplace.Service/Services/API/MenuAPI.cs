using Enplace.Service.DTO;
using System.Net;
using System.Net.Http.Json;

namespace Enplace.Service.Services.API
{
    public class MenuAPI(IHttpClientFactory clientFactory) : ApiService<MenuDTO>(clientFactory)
    {
        public async Task<List<RecipeDTO>> GetMenuRecipes(int id)
        {
            var result = await _client.GetFromJsonAsync<List<RecipeDTO>>($"{id}/recipes") ?? [];
            return result;
        }

        public async Task<Dictionary<string, List<GroceryListItem>>> GetGroceriesFor(int id)
        {
            (HttpStatusCode Code, Dictionary<string, List<GroceryListItem>> Items) = await RequestBuilder<Dictionary<string, List<GroceryListItem>>>
                .Create(_client, $"{id}/shoppinglist")
                .AddResponseHandler(HttpStatusCode.RequestTimeout, (_, b) => Console.WriteLine(b.RequestMessage))
                .AddResponseHandler(HttpStatusCode.NotFound, (_, r) => Console.WriteLine($"Not Found | Handler - {r.RequestMessage} - {r.ReasonPhrase}"))
                .ExecuteGetAsync();
            return Items;
        }
    }
}
