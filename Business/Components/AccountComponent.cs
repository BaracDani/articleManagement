using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Components.Base;
// using Business.Encryption;
using Business.Interfaces;
using Business.Views;
using DataAccess.Base.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace Business.Components
{
    public class AccountComponent : BaseComponent<User, UserView, IUserRepository>, IAccountComponent
    {
        private User _user;


        public AccountComponent(ICatalog catalog) : base(catalog)
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

        public bool Login(string email, string password)
        {

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return false;

            var userExists = UserExists(email);
            return userExists && IsValidUser(password);
        }

        public bool Register(string firstname, string lastname, string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || UserExists(email))
                return false;

            var user = Create(new UserView
            {
                FirstName = firstname,
                LastName = lastname,
                Password = password,
                Email = email
            });


            return user != null;
        }

        public bool ChangePassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            var userExists = UserExists(email);
            return userExists;
        }

        #region Private Methods

        private bool UserExists(string email)
        {
            _user = Repository.AllActive().FirstOrDefault(e => e.Email == email);
            return _user != null;
        }

        private bool IsValidUser(string password)
        {
            return _user != null && password == _user.Password; //PasswordHash.ValidatePassword(password, _user.Password);
        }

        #endregion
    }
}
