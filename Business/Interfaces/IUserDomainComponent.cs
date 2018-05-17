using System;
using System.Collections.Generic;
using Business.Components.Base;
using Business.Views;
using DataAccess.Entities;

namespace Business.Interfaces
{
    public interface IUserDomainComponent : IBaseComponent<UserDomainView>
    {
        IEnumerable<ApplicationUser> GetUsersOfDomain(long domainId);
    }
}

