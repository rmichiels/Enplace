using Enplace.Library.Contracts;
using Enplace.Service.DTO;
using System.Net.Http.Json;

namespace Enplace.WASM.Services
{
    public class IngredientResourceService : IResource<IngredientDTO>
    {
        private readonly HttpClient _client;
        public IngredientResourceService(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("res:ingr");
        }

        public async Task<List<IngredientDTO>> Query(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return [];
            }
            else
            {
                return await _client.GetFromJsonAsync<List<IngredientDTO>>($"?name={name}") ?? [];
            };
        }
    }
}
