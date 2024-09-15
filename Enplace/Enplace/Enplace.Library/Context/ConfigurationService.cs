using Enplace.Service.Entities;
using System.Net.Http.Json;

namespace Enplace.Library.Context
{
    public class ConfigurationService
    {
        private readonly HttpClient _client;
        private string _baseUrl { get; }
        public static EnplaceContext Context { get; set; }

        public ConfigurationService(string baseUrl)
        {
            _baseUrl = baseUrl;
            _client = new();
        }
        public async Task<EnplaceContext> GetContextAsync()
        {
            var result = await _client.GetFromJsonAsync<EnplaceContext>(_baseUrl);

            return result;
        }

        public async Task<List<Ingredient>?> GetIngredientsForCategory(int id)
        {
            var result = await _client.GetFromJsonAsync<List<Ingredient>>($"{_baseUrl}/ingredientsPerCategory/{id}");
            return result;
        }
    }
}
