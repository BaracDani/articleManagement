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
    public class UserDomainComponent : BaseComponent<UserDomain, UserDomainView, IUserDomainRepository>, IUserDomainComponent
    {
        public UserDomainComponent(ICatalog catalog) : base(catalog)
        {
        }

        protected override IUserDomainRepository Repository => Catalog.UserDomainRepository;

        protected override UserDomain ViewToEntity(UserDomainView item)
        {
            return UserDomainView.ToEntity(item);
        }

        protected override UserDomainView EntityToView(UserDomain item)
        {
            return UserDomainView.FromEntity(item);
        }


        public IEnumerable<ApplicationUser> GetUsersOfDomain(long domainId)
        {
            try
            {
                var list = Repository.Filter(userDomains => userDomains.DomainId == domainId);
                var resulted = list.ToArray();
                List<ApplicationUser> users = new List<ApplicationUser>();
                foreach (UserDomain item in resulted)
                {
                    users.Add(item.User);
                }
                
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

    }
}
