using DataAccess.Entities;
using System;
using System.Linq;
using Business.Extensions;

namespace Business.Views
{
    public class UserDomainView : BaseView
    {
        public string UserId { get; set; }

        public long DomainId { get; set; }

        public static UserDomain ToEntity(UserDomainView item)
        {
            if (item == null)
                return null;

            return new UserDomain
            {
                Id = item.Id,
                UserId = item.UserId,
                DomainId = item.DomainId
            };
        }

        public static UserDomainView FromEntity(UserDomain item)
        {
            if (item == null)
                return null;

            return new UserDomainView
            {
                Id = item.Id,
                UserId = item.UserId,
                DomainId = item.DomainId
            };
        }

        public static UserDomainView[] FromEntities(UserDomain[] items)
        {
            if (items == null || items.Length == 0)
                return new UserDomainView[0];

            return items.Select(FromEntity).ToArray();
        }

        public static UserDomain[] ToEntities(UserDomainView[] items)
        {
            if (items == null || items.Length == 0)
                return new UserDomain[0];

            return items.Select(ToEntity).ToArray();
        }
    }
}
