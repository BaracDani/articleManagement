using Unity;
using System.Web.Http;
using Business.Components;
using Business.Interfaces;
using DataAccess.Base.Infrastructure;
using DataAccess.Base.Interfaces;
using Service.Infrastructure;

namespace Service
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<ICatalog, Catalog>();
            container.RegisterType<IAccountComponent, AccountComponent>();
            container.RegisterType<IUserComponent, UserComponent>();

            var resolver = new UnityDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            // not sure why this is not working
            // DependencyResolver.SetResolver(resolver);
        }
    }
}