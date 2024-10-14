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

        public async Task<TEntity?> Add<TEntity>(TEntity entity) where TEntity : class, ILabeled
        {
            TEntity? returnValue = null;
            foreach (var kvp in _contexts)
            {
                var repo = new GenericRepository<TEntity>(kvp.Value);
                var result = await repo.Add(entity);
                if (kvp.Key == RepositoryMarker.Primary)
                {

                    returnValue = result;
                }
            }
            return returnValue;
        }

        public async Task Delete<TEntity>(TEntity entity) where TEntity : class, ILabeled
        {
            foreach (var kvp in _contexts)
            {
                var repo = new GenericRepository<TEntity>(kvp.Value);
                await repo.Delete(entity);
            }
        }

        public async Task Delete<TEntity>(string name) where TEntity : class, ILabeled
        {
            foreach (var kvp in _contexts)
            {
                var repo = new GenericRepository<TEntity>(kvp.Value);
                await repo.Delete(name);
            }
        }

        public async Task Delete<TEntity>(int id) where TEntity : class, ILabeled
        {

            foreach (var kvp in _contexts)
            {
                var repo = new GenericRepository<TEntity>(kvp.Value);
                await repo.Delete(id);
            }
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

        public async Task<TEntity?> Update<TEntity>(TEntity entity) where TEntity : class, ILabeled
        {
            TEntity? returnValue = null;
            foreach (var kvp in _contexts)
            {
                var repo = new GenericRepository<TEntity>(kvp.Value);
                var result = await repo.Update(entity);
                if (kvp.Key == RepositoryMarker.Primary)
                {
                    returnValue = result;
                }
            }

            return returnValue;
        }

        public async Task<List<TEntity>> GetWhere<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, ILabeled
        {
            var repo = new GenericRepository<TEntity>(_contexts[RepositoryMarker.Primary]);
            return await repo.GetWhere(predicate);
        }

        public async Task<IQueryable<TEntity>> Query<TEntity>() where TEntity : class, ILabeled
        {
            var repo = new GenericRepository<TEntity>(_contexts[RepositoryMarker.Primary]);
            return await repo.Query();
        }

        public Task Link<TBridge>(TBridge bridge) where TBridge : class
        {
            foreach (var kvp in _contexts)
            {
                kvp.Value.Add<TBridge>(bridge);
                kvp.Value.SaveChanges();
            }
            return Task.CompletedTask;
        }

        public Task UnLink<TBridge>(TBridge bridge) where TBridge : class
        {
            foreach (var kvp in _contexts)
            {
                kvp.Value.Remove<TBridge>(bridge);
                kvp.Value.SaveChanges();
            }
            return Task.CompletedTask;
        }
    }
}
