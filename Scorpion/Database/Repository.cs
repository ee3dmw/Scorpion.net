using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Scorpion.Database
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;

        public Repository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        public virtual void Add(T item)
        {
            _dbSet.Add(item);
        }

        public virtual T Find(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public virtual IEnumerable<T> All()
        {
            return _dbSet.ToList();
        }

        public virtual void Delete(T item)
        {
            _dbSet.Remove(item);
        }
    }
}