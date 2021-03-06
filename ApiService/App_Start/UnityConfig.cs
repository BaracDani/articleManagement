﻿using System.Web.Http;
using Business.Components;
using Business.Interfaces;
using DataAccess.Base.Infrastructure;
using DataAccess.Base.Interfaces;
using ApiService.Infrastructure;
using Unity;

namespace ApiService
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<ICatalog, Catalog>();
            container.RegisterType<IArticleComponent, ArticleComponent>();
            container.RegisterType<IReviewedArticleComponent, ReviewedArticleComponent>();
            container.RegisterType<IDomainComponent, DomainComponent>();
            container.RegisterType<IJournalComponent, JournalComponent>();
            container.RegisterType<IUserDomainComponent, UserDomainComponent>();

            var resolver = new UnityDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            //config.DependencyResolver = resolver;
            // not sure why this is not working
            //System.Web.Mvc.DependencyResolver.SetResolver(resolver);
        }
    }
}