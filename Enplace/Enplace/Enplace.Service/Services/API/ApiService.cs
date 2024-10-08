using Blazored.LocalStorage;
using Enplace.Service.Contracts;
using System.Net.Http.Json;

namespace Enplace.Service.Services.API
{
    public class ApiService<TEntity> where TEntity : class, ILabeled
    {
        protected readonly HttpClient _client;
        public ApiService(IHttpClientFactory clientFactory)
        {
            string clientName = $"data:{typeof(TEntity).Name}";
            _client = clientFactory.CreateClient(clientName);
        }
        public async Task<Exception?> Add(TEntity entity)
        {
            try
            {
                await _client.PostAsJsonAsync($"add", entity);
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<Exception?> Delete(int id)
        {
            try
            {
                await _client.DeleteAsync($"{id}");
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<Exception?> Delete(string name)
        {
            try
            {
                await _client.DeleteAsync($"{name}");
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<TEntity?> Get(int id)
        {
            return await _client.GetFromJsonAsync<TEntity>($"{id}");
        }
        public async Task<TEntity?> Get(string name)
        {
            return await _client.GetFromJsonAsync<TEntity>($"{name}");
        }

        public async Task<List<TEntity>> GetAll()
        {
            var result = await _client.GetFromJsonAsync<List<TEntity>>($"list") ?? [];
            return result;
        }

        public async Task<Exception?> Update(TEntity entity)
        {
            try
            {
                await _client.PatchAsJsonAsync("", entity);
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
