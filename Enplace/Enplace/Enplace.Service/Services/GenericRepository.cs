using Enplace.Service.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Enplace.Service.Services
{
    public class GenericRepository<TEntity> : ICrudable<TEntity> where TEntity : class, ILabeled
    {
        private readonly DbContext _context;
        public GenericRepository(DbContext context) { _context = context; }
        public async Task<Exception?> Add(TEntity entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<Exception?> Delete(TEntity entity)
        {
            try
            {
                _context.Remove(entity);
                _context.SaveChanges();
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
                var entity = _context.Set<TEntity>().FirstOrDefault(e => e.Name == name);
                if (entity is not null)
                {
                    _context.Remove(entity);
                    _context.SaveChanges();
                }
                else
                {
                    return new Exception($"Entity with Name {name} not found.");
                }
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
                var entity = _context.Find<TEntity>(id);
                if (entity is not null)
                {
                    _context.Remove(entity);
                    _context.SaveChanges();
                }
                else
                {
                    return new Exception($"Entity with ID {id} not found.");
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public async Task<TEntity?> Get(int id)
        {
            return await _context.FindAsync<TEntity>(id);
        }
        public async Task<TEntity?> Get(string name)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Name == name);
        }
        public async Task<List<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetWhere(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).OrderBy(e => e.Name).ToList();
        }

        public async Task<IQueryable<TEntity>> Query()
        {
            return _context.Set<TEntity>();
        }

        public async Task<Exception?> Update(TEntity entity)
        {
            try
            {
                _context.Update(entity);
                _context.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
