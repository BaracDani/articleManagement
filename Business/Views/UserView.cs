using DataAccess.Entities;
using System.Linq;

namespace Business.Views
{
    public class UserView : BaseView
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public static User ToEntity(UserView item)
        {
            if (item == null)
                return null;

            return new User
            {
                Id = item.Id,
                Email = item.Email,
                Password = item.Password,
                FirstName = item.FirstName,
                LastName = item.LastName,
            };
        }

        public static UserView FromEntity(User item)
        {
            if (item == null)
                return null;

            return new UserView
            {
                Id = item.Id,
                Email = item.Email,
                Password = item.Password,
                FirstName = item.FirstName,
                LastName = item.LastName,
            };
        }

        public static UserView[] FromEntities(User[] items)
        {
            if (items == null || items.Length == 0)
                return new UserView[0];

            return items.Select(FromEntity).ToArray();
        }

        public static User[] ToEntities(UserView[] items)
        {
            if (items == null || items.Length == 0)
                return new User[0];

            return items.Select(ToEntity).ToArray();
        }
    }
}
