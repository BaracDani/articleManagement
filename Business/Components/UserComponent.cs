using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Components.Base;
using Business.Interfaces;
using Business.Views;
using DataAccess.Base.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace Business.Components
{
    public class UserComponent : BaseComponent<User, UserView, IUserRepository>, IUserComponent
    {
        public UserComponent(ICatalog catalog) : base(catalog)
        {
        }

        protected override IUserRepository Repository => Catalog.UserRepository;

        protected override User ViewToEntity(UserView item)
        {
            return UserView.ToEntity(item);
        }

        protected override UserView EntityToView(User item)
        {
            return UserView.FromEntity(item);
        }
    }
}
