using DataAccess.Entities;
using System;
using System.Linq;
using Business.Extensions;

namespace Business.Views
{
    public class JournalView : BaseView
    {
        public string Title { get; set; }

        public bool IsPublished { get; set; }

        public DateTime PublishDate { get; set; }

        public string UserId { get; set; }
        
        public long DomainId { get; set; }

        public static Journal ToEntity(JournalView item)
        {
            if (item == null)
                return null;

            return new Journal
            {
                Id = item.Id,
                Title = item.Title,
                IsPublished = item.IsPublished,
                PublishDate = item.PublishDate.ToShortDateTime(),
                EditorId = item.UserId,
                DomainId = item.DomainId
            };
        }

        public static JournalView FromEntity(Journal item)
        {
            if (item == null)
                return null;

            return new JournalView
            {
                Id = item.Id,
                Title = item.Title,
                IsPublished = item.IsPublished,
                PublishDate = item.PublishDate.ToLongDateTime(),
                UserId = item.EditorId,
                DomainId = item.DomainId
            };
        }

        public static JournalView[] FromEntities(Journal[] items)
        {
            if (items == null || items.Length == 0)
                return new JournalView[0];

            return items.Select(FromEntity).ToArray();
        }

        public static Journal[] ToEntities(JournalView[] items)
        {
            if (items == null || items.Length == 0)
                return new Journal[0];

            return items.Select(ToEntity).ToArray();
        }
    }
}
