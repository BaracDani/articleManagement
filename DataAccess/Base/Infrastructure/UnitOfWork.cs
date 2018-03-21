using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Base.Interfaces;

namespace DataAccess.Base.Infrastructure
{
    public abstract class UnitOfWork<T, TU> : Disposable, IUnitOfWork where T : class, IDisposable, new() where TU : DatabaseFactory<T>
    {
        private T _dataContext;

        protected UnitOfWork(TU databaseFactory)
        {
            this.DatabaseFactory = databaseFactory;
        }

        protected T DataContext => _dataContext ?? (_dataContext = DatabaseFactory.Get());

        protected TU DatabaseFactory { get; }

        public abstract int SaveChanges();
    }
}
