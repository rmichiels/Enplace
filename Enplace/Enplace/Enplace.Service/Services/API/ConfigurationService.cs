using Enplace.Service.DTO;
using Enplace.Service.Entities;
using System.Net.Http.Json;

namespace Enplace.Service.Services.API
{
    public class ConfigurationService
    {
        private readonly HttpClient _client;

        public ConfigurationService(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("config");
        }

        public async Task<List<Ingredient>?> GetIngredientsForCategory(int id)
        {
            var result = await _client.GetFromJsonAsync<List<Ingredient>>($"/ingredientsPerCategory/{id}");
            return result;
        }

        public async Task GetBaseResources()
        {
            var result = await _client.GetFromJsonAsync<ResourceDTO>($"resourcebase");
            EnplaceContext.IngredientCategories = result.IngredientCategories;
            EnplaceContext.RecipeCategories = result.RecipeCategories;
            EnplaceContext.Measurements = result.Measurements;
        }
    }
}
