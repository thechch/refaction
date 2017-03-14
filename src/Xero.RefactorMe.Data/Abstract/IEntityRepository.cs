using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xero.RefactorMe.Model;

namespace Xero.RefactorMe.Data.Abstract
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        IEnumerable<T> GetAll();

        T GetSingle(Guid id);

        T GetSingle(Expression<Func<T, bool>> predicate);

        IEnumerable<T> GetMultiple(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);

        void Commit();
    }
}