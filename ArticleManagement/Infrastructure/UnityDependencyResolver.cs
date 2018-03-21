using System;
using System.Collections.Generic;
using Unity;
using System.Web.Http.Dependencies;

namespace Service.Infrastructure
{
    public class UnityDependencyResolver : IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        readonly IUnityContainer _container;

        public UnityDependencyResolver(IUnityContainer container)
        {
            this._container = container;
        }

        public Object GetService(Type serviceType)
        {
            if (!_container.IsRegistered(serviceType))
            {
                if (serviceType.IsAbstract || serviceType.IsInterface)
                {
                    return null;
                }
            }

            return _container.Resolve(serviceType);
        }

        public IEnumerable<Object> GetServices(Type serviceType)
        {
            return _container.ResolveAll(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}