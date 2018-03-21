using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Base.Infrastructure;
using DataAccess.Base.Interfaces;

namespace DataAccess.Base.Repositories
{
    public abstract class BaseRepository<T, TU> : Disposable where T : class where TU : DbContext, IDisposable, new()
    {
        #region Fields

        private TU _context;

        #endregion

        #region Constructors

        protected BaseRepository(IDatabaseFactory<TU> databaseFactory)
        {
            DatabaseFactory = databaseFactory;
        }

        #endregion

        #region Properties

        protected IDbSet<T> DbSet => Context.Set<T>();

        protected IDatabaseFactory<TU> DatabaseFactory
        {
            get;
        }

        protected TU Context => _context ?? (_context = DatabaseFactory.Get());

        #endregion

        public virtual IQueryable<T> All()
        {
            return DbSet.AsQueryable();
        }

        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable();
        }

        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 10)
        {
            int skipCount = index * size;
            var resetSet = filter != null ? DbSet.Where(filter).AsQueryable() : DbSet.AsQueryable();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            total = resetSet.Count();
            return resetSet.AsQueryable();
        }

        public virtual bool Contains(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }

        public virtual T Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual T Create(T entity)
        {
            var newEntry = DbSet.Add(entity);
            return newEntry;
        }

        public virtual int Count => DbSet.Count();

        public virtual int Delete(T entity)
        {
            DbSet.Remove(entity);
            return 0;
        }

        public virtual int Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                DbSet.Remove(entity);
            }
            return 0;
        }

        public virtual int Update(T entity)
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return 0;
        }

        public virtual int Delete(Expression<Func<T, bool>> predicate)
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
            {
                DbSet.Remove(obj);
            }
            return 0;
        }

        protected override void DisposeCore()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }
    }
}
