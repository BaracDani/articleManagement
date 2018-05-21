using DataAccess.Entities;
using System;
using System.Linq;
using Business.Extensions;

namespace Business.Views
{
    public class ReviewedArticleView : BaseView
    {
        public int ReviewStatus { get; set; }

        public int ReviewPoints { get; set; }

        public string Comment { get; set; }

        public long ArticleId { get; set; }

        public string UserId { get; set; }

        public static ReviewedArticle ToEntity(ReviewedArticleView item)
        {
            if (item == null)
                return null;

            return new ReviewedArticle
            {
                Id = item.Id,
                ReviewStatus = item.ReviewStatus,
                ReviewPoints = item.ReviewPoints,
                Comment = item.Comment,
                ArticleId = item.ArticleId,
                UserId = item.UserId
            };
        }

        public static ReviewedArticleView FromEntity(ReviewedArticle item)
        {
            if (item == null)
                return null;

            return new ReviewedArticleView
            {
                Id = item.Id,
                ReviewStatus = item.ReviewStatus,
                ReviewPoints = item.ReviewPoints,
                Comment = item.Comment,
                ArticleId = item.ArticleId,
                UserId = item.UserId
            };
        }

        public static ReviewedArticleView[] FromEntities(ReviewedArticle[] items)
        {
            if (items == null || items.Length == 0)
                return new ReviewedArticleView[0];

            return items.Select(FromEntity).ToArray();
        }

        public static ReviewedArticle[] ToEntities(ReviewedArticleView[] items)
        {
            if (items == null || items.Length == 0)
                return new ReviewedArticle[0];

            return items.Select(ToEntity).ToArray();
        }
    }
}
