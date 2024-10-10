namespace Enplace.Service.Contracts
{
    public interface ICrudable
    {
        public Task<List<TEntity>> GetAll<TEntity>() where TEntity : class, ILabeled;
        public Task<List<TEntity>> GetWhere<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, ILabeled;
        public Task<TEntity?> Get<TEntity>(int id) where TEntity : class, ILabeled;
        public Task<TEntity?> Get<TEntity>(string name) where TEntity : class, ILabeled;
        public Task<TEntity?> Add<TEntity>(TEntity entity) where TEntity : class, ILabeled;
        public Task<TEntity?> Update<TEntity>(TEntity entity) where TEntity : class, ILabeled;
        public Task Delete<TEntity>(TEntity entity) where TEntity : class, ILabeled;
        public Task Delete<TEntity>(string name) where TEntity : class, ILabeled;
        public Task Delete<TEntity>(int id) where TEntity : class, ILabeled;
        public Task<IQueryable<TEntity>> Query<TEntity>() where TEntity : class, ILabeled;
        public Task Link<TBridge>(TBridge bridge) where TBridge : class;
        public Task UnLink<TBridge>(TBridge bridge) where TBridge : class;
    }
    public interface ICrudable<TEntity> where TEntity : class, ILabeled
    {
        public Task<TEntity?> Add(TEntity entity);
        public Task Delete(int id);
        public Task Delete(string name);
        public Task Delete(TEntity entity);
        public Task<TEntity?> Get(int id);
        public Task<TEntity?> Get(string name);
        public Task<List<TEntity>> GetAll();
        public Task<List<TEntity>> GetWhere(Func<TEntity, bool> predicate);
        public Task<TEntity?> Update(TEntity entity);
        public Task<IQueryable<TEntity>> Query();
    }
}
