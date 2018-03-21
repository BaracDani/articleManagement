using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Base.Interfaces;

namespace DataAccess.Base.Infrastructure
{
    // singleton factory class
    public class DatabaseFactory<T> : Disposable, IDatabaseFactory<T> where T : class, IDisposable, new()
    {
        private T _dataContext;

        public T Get()
        {
            return _dataContext ?? (_dataContext = new T());
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null)
            {
                _dataContext.Dispose();
                _dataContext = null;
            }
        }
    }
}
