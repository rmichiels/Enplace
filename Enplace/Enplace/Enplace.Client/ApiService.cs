using Enplace.Service.Contracts;

namespace Enplace.Client
{
    public class ApiService<TEntity> : ICrudable<TEntity> where TEntity : class, ILabeled
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        public ApiService(string baseUrl)
        {
            _client = new HttpClient();
            _baseUrl = baseUrl;
            _client.BaseAddress = new Uri(baseUrl);

        }
        public async Task<Exception?> Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Exception?> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Exception?> Delete(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Exception?> Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity?> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity?> Get(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<TEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Exception?> MassDelete(ICollection<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<Exception?> MassUpdate(ICollection<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<Exception?> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
