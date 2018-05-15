using DataAccess.Entities;
using System;
using System.Linq;
using Business.Extensions;

namespace Business.Views
{
    public class ArticleView : BaseView
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Abstract { get; set; }
        
        public int ApprovalStatus { get; set; }

        public DateTime Deadline { get; set; }

        public string UserId { get; set; }

        public long JournalId { get; set; }
        
        public string FilePath { get; set; }

        public static Article ToEntity(ArticleView item)
        {
            if (item == null)
                return null;

            return new Article
            {
                Id = item.Id,
                Title = item.Title,
                Author = item.Author,
                Abstract = item.Abstract,
                ApprovalStatus = item.ApprovalStatus,
                Deadline = item.Deadline.ToShortDateTime(),
                UserId = item.UserId,
                JournalId = item.JournalId,
                FilePath = item.FilePath
            };
        }

        public static ArticleView FromEntity(Article item)
        {
            if (item == null)
                return null;

            return new ArticleView
            {
                Id = item.Id,
                Title = item.Title,
                Author = item.Author,
                Abstract = item.Abstract,
                ApprovalStatus = item.ApprovalStatus,
                Deadline = item.Deadline.ToLongDateTime(),
                UserId = item.UserId,
                JournalId = item.JournalId,
                FilePath = item.FilePath
            };
        }

        public static ArticleView[] FromEntities(Article[] items)
        {
            if (items == null || items.Length == 0)
                return new ArticleView[0];

            return items.Select(FromEntity).ToArray();
        }

        public static Article[] ToEntities(ArticleView[] items)
        {
            if (items == null || items.Length == 0)
                return new Article[0];

            return items.Select(ToEntity).ToArray();
        }
    }
}
