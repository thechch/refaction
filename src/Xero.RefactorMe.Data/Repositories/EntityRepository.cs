using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xero.RefactorMe.Data.Abstract;
using Xero.RefactorMe.Model;

namespace Xero.RefactorMe.Data.Repositories
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity, new()
    {
        private RefactorMeDbContext _context;

        public EntityRepository(RefactorMeDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public T GetSingle(Guid id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>();
            return query.Where(predicate).FirstOrDefault();
        }

        public IEnumerable<T> GetMultiple(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>();
            return query.Where(predicate);
        }

        public void Add(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public void Update(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Commit()
        {
            _context.SaveChanges();
        }
    }
}