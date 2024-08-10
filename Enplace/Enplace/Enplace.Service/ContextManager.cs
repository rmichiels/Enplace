using Enplace.Service.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.Service
{
    public class ContextManager : ICrudable
    {
        private readonly Dictionary<DbContext, string> _contexts = [];
        private List<ICrudable<ILabeled>> _repositories = [];
        public ContextManager()
        {

        }

        public (bool, Exception?) Add<TEntity>(TEntity entity) where TEntity : class, ILabeled
        {
            (bool, Exception?) result = (false, null);
            var repositories = _repositories.Where(repo => repo is ICrudable<TEntity>).ToList();
            foreach (ICrudable<TEntity> repo in repositories)
            {
                if (repo.Marker == RepositoryMarker.Primary)
                {
                    result = repo.Add(entity);
                }
                else
                {
                    repo.Add(entity);
                }
            }
            return result;
        }

        public (bool, Exception?) Delete<TEntity>(TEntity entity) where TEntity : class, ILabeled
        {
            throw new NotImplementedException();
        }

        public (bool, Exception?) Delete<TEntity>(string name) where TEntity : class, ILabeled
        {
            throw new NotImplementedException();
        }

        public (bool, Exception?) Delete<TEntity>(int id) where TEntity : class, ILabeled
        {
            throw new NotImplementedException();
        }

        public TEntity? Get<TEntity>(int id) where TEntity : class, ILabeled
        {
            throw new NotImplementedException();
        }

        public TEntity? Get<TEntity>(string name) where TEntity : class, ILabeled
        {
            throw new NotImplementedException();
        }

        public ICollection<TEntity> GetAll<TEntity>() where TEntity : class, ILabeled
        {
            throw new NotImplementedException();
        }

        public (bool, Exception?) MassDelete<TEntity>(ICollection<TEntity> entities) where TEntity : class, ILabeled
        {
            throw new NotImplementedException();
        }

        public (bool, Exception?) MassUpdate<TEntity>(ICollection<TEntity> entities) where TEntity : class, ILabeled
        {
            throw new NotImplementedException();
        }

        public (bool, Exception?) Update<TEntity>(TEntity entity) where TEntity : class, ILabeled
        {
            throw new NotImplementedException();
        }
    }
}
