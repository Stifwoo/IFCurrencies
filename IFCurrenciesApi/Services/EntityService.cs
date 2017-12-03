using System;
using IFCurrenciesApi.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IFCurrenciesApi.Models;

namespace IFCurrenciesApi.Services
{
    public class EntityService<T> : IEntityService<T>
        where T : EntityBase
    {
        protected IContext _context;
        protected IDbSet<T> _dbSet;

        public EntityService(IContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public T GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        public void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}