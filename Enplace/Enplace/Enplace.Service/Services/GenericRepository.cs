using Enplace.Service.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Enplace.Service.Services
{
    public class GenericRepository<TEntity> : ICrudable<TEntity> where TEntity : class, ILabeled
    {
        private readonly DbContext _context;
        public GenericRepository(DbContext context) { _context = context; }
        public Task<TEntity?> Add(TEntity entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();
                return Task.FromResult<TEntity?>(entity);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return Task.FromResult<TEntity?>(null);
            }
        }

        public Task Delete(TEntity entity)
        {
            try
            {
                _context.Remove(entity);
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return Task.FromException(ex);
            }
        }

        public Task Delete(string name)
        {
            try
            {
                var entity = _context.Set<TEntity>().FirstOrDefault(e => e.Name == name);
                if (entity is not null)
                {
                    _context.Remove(entity);
                    _context.SaveChanges();
                    return Task.CompletedTask;
                }
                else
                {
                    return Task.FromException(new Exception($"Entity with Name {name} not found."));
                }
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }
        public Task Delete(int id)
        {
            try
            {
                var entity = _context.Find<TEntity>(id);
                if (entity is not null)
                {
                    _context.Remove(entity);
                    _context.SaveChanges();
                    return Task.CompletedTask;
                }
                else
                {
                    return Task.FromException(new Exception($"Entity with ID {id} not found."));
                }
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
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

        public Task<List<TEntity>> GetWhere(Func<TEntity, bool> predicate)
        {
            return Task.FromResult(_context.Set<TEntity>().Where(predicate).OrderBy(e => e.Name).ToList());
        }

        public Task<IQueryable<TEntity>> Query()
        {
            return Task.FromResult(_context.Set<TEntity>().AsQueryable());
        }

        public Task<TEntity?> Update(TEntity entity)
        {
            try
            {
                _context.Update(entity);
                _context.SaveChanges();
                return Task.FromResult<TEntity?>(entity);
            }
            catch (Exception)
            {
                return Task.FromResult<TEntity?>(null);
            }
        }
    }
}
