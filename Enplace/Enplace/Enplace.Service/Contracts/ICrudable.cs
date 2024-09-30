namespace Enplace.Service.Contracts
{
    public interface ICrudable
    {
        public Task<List<TEntity>> GetAll<TEntity>() where TEntity : class, ILabeled;
        public Task<List<TEntity>> GetWhere<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, ILabeled;
        public Task<TEntity?> Get<TEntity>(int id) where TEntity : class, ILabeled;
        public Task<TEntity?> Get<TEntity>(string name) where TEntity : class, ILabeled;
        public Task<Exception?> Add<TEntity>(TEntity entity) where TEntity : class, ILabeled;
        public Task<Exception?> Update<TEntity>(TEntity entity) where TEntity : class, ILabeled;
        public Task<Exception?> Delete<TEntity>(TEntity entity) where TEntity : class, ILabeled;
        public Task<Exception?> Delete<TEntity>(string name) where TEntity : class, ILabeled;
        public Task<Exception?> Delete<TEntity>(int id) where TEntity : class, ILabeled;
    }
    public interface ICrudable<TEntity> where TEntity : class, ILabeled
    {
        public Task<Exception?> Add(TEntity entity);
        public Task<Exception?> Delete(int id);
        public Task<Exception?> Delete(string name);
        public Task<Exception?> Delete(TEntity entity);
        public Task<TEntity?> Get(int id);
        public Task<TEntity?> Get(string name);
        public Task<List<TEntity>> GetAll();
        public Task<List<TEntity>> GetWhere(Func<TEntity, bool> predicate);
        public Task<Exception?> Update(TEntity entity);
    }
}
