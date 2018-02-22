using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Scorpion.Database
{
    public interface IRepository<T>
    {
        void Add(T item);
        T Find(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> All();
        void Delete(T item);
    }
}