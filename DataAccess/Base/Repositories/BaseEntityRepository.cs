using System;
using System.Data.Entity;
using System.Linq;
using DataAccess.Base.Interfaces;
using DataAccess.Entities;

namespace DataAccess.Base.Repositories
{
    public class BaseEntityRepository<T, TU> : BaseRepository<T, TU>
        where T : class, IBaseEntity
        where TU : DbContext, IDisposable, new()
    {
        protected BaseEntityRepository(IDatabaseFactory<TU> databaseFactory)
            : base(databaseFactory)
        {
        }

        public virtual IQueryable<T> AllActive()
        {
            try
            {
                return DbSet.Where(x => x.Active).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T GetById(long id)
        {
            return AllActive().SingleOrDefault(x => x.Id == id);
        }

        public void LogicalDelete(long id)
        {
            GetById(id).Active = false;
        }

        public void Delete(long id)
        {
            var t = GetById(id);
            Delete(t);
        }
    }
}
