using Enplace.Service.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enplace.Service
{
    public class GenericRepository<TEntity> : ICrudable<TEntity> where TEntity : class, ILabeled
    {
        private readonly DbContext _context;
        public GenericRepository(DbContext context) { _context = context; }

        public RepositoryMarker Marker { get; set; }

        public (bool, Exception?) Add(TEntity entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }

        public (bool, Exception?) Delete(TEntity entity)
        {
            try
            {
                _context.Remove(entity);
                _context.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }

        public (bool, Exception?) Delete(string name)
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
                    throw new Exception($"Entity with Name {name} not found.");
                }
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }
        public (bool, Exception?) Delete(int id)
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
                    throw new Exception($"Entity with ID {id} not found.");
                }
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }
        public TEntity? Get(int id)
        {
            return _context.Find<TEntity>(id);
        }
        public TEntity? Get(string name)
        {
            return _context.Set<TEntity>().FirstOrDefault(e => e.Name == name);
        }
        public ICollection<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }
        public (bool, Exception?) MassDelete(ICollection<TEntity> entities)
        {
            try
            {
                _context.RemoveRange(entities);
                _context.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }
        public (bool, Exception?) MassUpdate(ICollection<TEntity> entities)
        {
            try
            {
                _context.UpdateRange(entities);
                _context.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }
        public (bool, Exception?) Update(TEntity entity)
        {
            try
            {
                _context.Update(entity);
                _context.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }
    }
}
