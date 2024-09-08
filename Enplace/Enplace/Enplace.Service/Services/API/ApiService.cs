using Enplace.Service.Contracts;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Enplace.Service.Services.API
{
    public class ApiService<TEntity> : ICrudable<TEntity> where TEntity : class, ILabeled
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        public ApiService(string baseUrl)
        {
            _client = new HttpClient();
            _baseUrl = baseUrl;
        }
        public async Task<Exception?> Add(TEntity entity)
        {
            try
            {
                await _client.PostAsJsonAsync($"{_baseUrl}/add", entity);
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
                await _client.DeleteAsync($"{_baseUrl}/byid/{id}");
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
                await _client.DeleteAsync($"{_baseUrl}/byname/{name}");
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
            return await _client.GetFromJsonAsync<TEntity>($"{_baseUrl}/byid/{id}");
        }

        public async Task<TEntity?> Get(string name)
        {
            return await _client.GetFromJsonAsync<TEntity>($"{_baseUrl} /byname/ {name}");
        }

        public async Task<ICollection<TEntity>> GetAll()
        {
            var result = await _client.GetFromJsonAsync<ICollection<TEntity>>($"{_baseUrl}/list");
            return result;
        }

        public async Task<Exception?> MassDelete(ICollection<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<Exception?> MassUpdate(ICollection<TEntity> entities)
        {
            try
            {
                await _client.PatchAsJsonAsync($"{_baseUrl}/range", entities);
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<Exception?> Update(TEntity entity)
        {
            try
            {
                await _client.PatchAsJsonAsync(_baseUrl, entity);
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
