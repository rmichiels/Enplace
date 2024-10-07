using Enplace.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.Service.Services.API
{
    public class MenuAPI : ApiService<MenuDTO>
    {
        public MenuAPI(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<List<RecipeDTO>> GetMenuRecipes(int id)
        {
            var result = await _client.GetFromJsonAsync<List<RecipeDTO>>($"{id}/recipes") ?? [];
            return result;
        }
    }
}
