using DataAccess.Entities;
using System;
using System.Linq;
using Business.Extensions;

namespace Business.Views
{
    public class DomainView : BaseView
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public static Domain ToEntity(DomainView item)
        {
            if (item == null)
                return null;

            return new Domain
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
            };
        }

        public static DomainView FromEntity(Domain item)
        {
            if (item == null)
                return null;

            return new DomainView
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
            };
        }

        public static DomainView[] FromEntities(Domain[] items)
        {
            if (items == null || items.Length == 0)
                return new DomainView[0];

            return items.Select(FromEntity).ToArray();
        }

        public static Domain[] ToEntities(DomainView[] items)
        {
            if (items == null || items.Length == 0)
                return new Domain[0];

            return items.Select(ToEntity).ToArray();
        }
    }
}
