using Enplace.Service.Contracts;
using Enplace.Service.Database;
using Microsoft.EntityFrameworkCore;

namespace Enplace.Service.Services.Managers
{
    public class ContextManager : ICrudable
    {
        private readonly Dictionary<RepositoryMarker, DbContext> _contexts = [];
        private List<ICrudable<ILabeled>> _repositories = [];
        public ContextManager(DbContext[] contexts)
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
                        else
                        {
                            sqlServer.Database.OpenConnection();
                            sqlServer.Database.CloseConnection();
                        }
                        break;
                    case LiteDBContext sqlLite:
                        if (sqlLite.Database.CanConnect())
                        {
                            sqlLite.Database.Migrate();
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

        public async Task<List<TEntity>> GetAll<TEntity>() where TEntity : class, ILabeled
        {
            var repo = new GenericRepository<TEntity>(_contexts[RepositoryMarker.Primary]);
            return await repo.GetAll();
        }

        public async Task<Exception?> Update<TEntity>(TEntity entity) where TEntity : class, ILabeled
        {
            try
            {
                foreach (var kvp in _contexts)
                {
                    kvp.Value.Update<TEntity>(entity);
                    kvp.Value.SaveChanges();
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<List<TEntity>> GetWhere<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, ILabeled
        {
            var repo = new GenericRepository<TEntity>(_contexts[RepositoryMarker.Primary]);
            return await repo.GetWhere(predicate);
        }
    }
}
