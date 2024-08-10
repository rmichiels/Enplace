using Enplace.Service.Contracts;
using Enplace.Service.Database;
using Microsoft.EntityFrameworkCore;

namespace Enplace.Service
{
    public class ContextManager : ICrudable
    {
        private readonly Dictionary<RepositoryMarker, DbContext> _contexts = [];
        private List<ICrudable<ILabeled>> _repositories = [];
        public ContextManager(List<DbContext> contexts)
        {
            bool isPrimaryValidated = false;
            foreach (DbContext context in contexts)
            {
                switch (context)
                {
                    case SSDBContext sqlServer:
                        if (sqlServer.Database.CanConnect())
                        {
                            isPrimaryValidated = true;
                            _contexts.Add(RepositoryMarker.Primary, sqlServer);
                        }
                        break;
                    case LiteDBContext sqlLite:
                        if (sqlLite.Database.CanConnect())
                        {
                            if (isPrimaryValidated)
                            {
                                _contexts.Add(RepositoryMarker.Secondary, sqlLite);
                            }
                            else
                            {
                                _contexts.Add(RepositoryMarker.Primary, sqlLite);
                            }
                        }
                        break;
                }
            }
        }

        public async Task<Exception?> Add<TEntity>(TEntity entity) where TEntity : class, ILabeled
        {
            Exception? exception = null;
            foreach (var kvp in _contexts)
            {
                var repo = new GenericRepository<TEntity>(kvp.Value);
                exception = await repo.Add(entity);
                if (exception != null) { return exception; }
            }
            return exception;
        }

        public async Task<Exception?> Delete<TEntity>(TEntity entity) where TEntity : class, ILabeled
        {
            throw new NotImplementedException();
        }

        public Task<Exception?> Delete<TEntity>(string name) where TEntity : class, ILabeled
        {
            throw new NotImplementedException();
        }

        public Task<Exception?> Delete<TEntity>(int id) where TEntity : class, ILabeled
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity?> Get<TEntity>(int id) where TEntity : class, ILabeled
        {
            var repo = new GenericRepository<TEntity>(_contexts[RepositoryMarker.Primary]);
            return await repo.Get(id);
        }

        public async Task<TEntity?> Get<TEntity>(string name) where TEntity : class, ILabeled
        {
            var repo = new GenericRepository<TEntity>(_contexts[RepositoryMarker.Primary]);
            return await repo.Get(name);
        }

        public async Task<ICollection<TEntity>> GetAll<TEntity>() where TEntity : class, ILabeled
        {
            var repo = new GenericRepository<TEntity>(_contexts[RepositoryMarker.Primary]);
            return await repo.GetAll();
        }

        public Task<Exception?> MassDelete<TEntity>(ICollection<TEntity> entities) where TEntity : class, ILabeled
        {
            throw new NotImplementedException();
        }

        public Task<Exception?> MassUpdate<TEntity>(ICollection<TEntity> entities) where TEntity : class, ILabeled
        {
            throw new NotImplementedException();
        }

        public Task<Exception?> Update<TEntity>(TEntity entity) where TEntity : class, ILabeled
        {
            throw new NotImplementedException();
        }
    }
}
