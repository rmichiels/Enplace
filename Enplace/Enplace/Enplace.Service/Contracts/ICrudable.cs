using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.Service.Contracts
{
    public interface ICrudable
    {
        public ICollection<TEntity> GetAll<TEntity>() where TEntity : class, ILabeled;
        public TEntity? Get<TEntity>(int id) where TEntity : class, ILabeled;
        public TEntity? Get<TEntity>(string name) where TEntity : class, ILabeled;
        public (bool, Exception?) Add<TEntity>(TEntity entity) where TEntity : class, ILabeled;
        public (bool, Exception?) Update<TEntity>(TEntity entity) where TEntity : class, ILabeled;
        public (bool, Exception?) Delete<TEntity>(TEntity entity) where TEntity : class, ILabeled;
        public (bool, Exception?) Delete<TEntity>(string name) where TEntity : class, ILabeled;
        public (bool, Exception?) Delete<TEntity>(int id) where TEntity : class, ILabeled;
        public (bool, Exception?) MassUpdate<TEntity>(ICollection<TEntity> entities) where TEntity : class, ILabeled;
        public (bool, Exception?) MassDelete<TEntity>(ICollection<TEntity> entities) where TEntity : class, ILabeled;
    }
    public interface ICrudable<TEntity> where TEntity : class, ILabeled
    {
        public RepositoryMarker Marker { get; set; }
        public ICollection<TEntity> GetAll();
        public TEntity? Get(int id);
        public TEntity? Get(string name);
        public (bool, Exception?) Add(TEntity entity);
        public (bool, Exception?) Update(TEntity entity);
        public (bool, Exception?) Delete(TEntity entity);
        public (bool, Exception?) Delete(string name);
        public (bool, Exception?) Delete(int id);
        public (bool, Exception?) MassUpdate(ICollection<TEntity> entities);
        public (bool, Exception?) MassDelete(ICollection<TEntity> entities);
    }
}
