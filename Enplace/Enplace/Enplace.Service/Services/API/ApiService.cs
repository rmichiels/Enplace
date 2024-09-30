using Blazored.LocalStorage;
using Enplace.Service.Contracts;
using System.Net.Http.Json;

namespace Enplace.Service.Services.API
{
    public class ApiService<TEntity> where TEntity : class, ILabeled
    {
        private readonly HttpClient _client;
        public ApiService(IHttpClientFactory clientFactory, ILocalStorageService storageService)
        {
            _client = clientFactory.CreateClient("data");
            if (_client.DefaultRequestHeaders.Authorization is null)
            {
                /*
                var token = storageService.GetItemAsStringAsync("skid.enplace").AsTask().Result;
                if (token is not null)
                {
                    _client.DefaultRequestHeaders.Authorization = new("Bearer", token);
                }*/
            }
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
                await _client.DeleteAsync($"byid/{id}");
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
                await _client.DeleteAsync($"byname/{name}");
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<Exception?> Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }
        public async Task<TEntity?> Get(int id)
        {
            return await _client.GetFromJsonAsync<TEntity>($"byid/{id}");
        }
        public async Task<TEntity?> Get(string name)
        {
            return await _client.GetFromJsonAsync<TEntity>($" byname/ {name}");
        }

        public async Task<ICollection<TEntity>> GetAll()
        {
            var result = await _client.GetFromJsonAsync<ICollection<TEntity>>($"list");
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
