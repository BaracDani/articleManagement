using Business.Components.Base;
using Business.Interfaces;
using Business.Views;
using DataAccess.Base.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Components
{
    public class DomainComponent : BaseComponent<Domain, DomainView, IDomainRepository>, IDomainComponent
    {
        public DomainComponent(ICatalog catalog) : base(catalog)
        {
        }

        protected override IDomainRepository Repository => Catalog.DomainRepository;

        protected override Domain ViewToEntity(DomainView item)
        {
            return DomainView.ToEntity(item);
        }

        protected override DomainView EntityToView(Domain item)
        {
            return DomainView.FromEntity(item);
        }
    }
}
