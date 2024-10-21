using Enplace.Library.Contracts;
using Enplace.Service.DTO;
using Enplace.Service.Services.API;
using System;
using System.Net.Http.Json;

namespace Enplace.WASM.Services
{
    public class RecipeResourceProvider : IResourceProvider<RecipeDTO>
    {
        private readonly HttpClient _client;
        public RecipeResourceProvider(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("res:recipe");
        }

        public async Task<Response<RecipeDTO>> Query(params (string Key, string Value)[] queryParameters)
        {

            string parameters = string.Empty;
            if (queryParameters.Length > 0)
            {
                parameters = string.Join("&", queryParameters.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            }
            if (!string.IsNullOrEmpty(parameters))
            {

                var response = await RequestBuilder<Response<RecipeDTO>>.Create(_client, $"?{parameters}")
                    .ExecuteGetAsync();
                return response ?? new();
            }
            else
            {
                return new();
            }
        }
    }
}
