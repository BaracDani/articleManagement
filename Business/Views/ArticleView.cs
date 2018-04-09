﻿using DataAccess.Entities;
using System.Linq;

namespace Business.Views
{
    public class ArticleView : BaseView
    {
        public string Name { get; set; }
        public string Abstract { get; set; }

        public bool Approved { get; set; }

        public bool Rejected { get; set; }

        public bool InPending { get; set; }

        public bool InReview { get; set; }

        public string UserId { get; set; }

        public static Article ToEntity(ArticleView item)
        {
            if (item == null)
                return null;

            return new Article
            {
                Id = item.Id,
                Name = item.Name,
                Abstract = item.Abstract,
                Approved = item.Approved,
                Rejected = item.Rejected,
                InPending = item.InPending,
                InReview = item.InReview,
                UserId = item.UserId
            };
        }

        public static ArticleView FromEntity(Article item)
        {
            if (item == null)
                return null;

            return new ArticleView
            {
                Id = item.Id,
                Name = item.Name,
                Abstract = item.Abstract,
                Approved = item.Approved,
                Rejected = item.Rejected,
                InPending = item.InPending,
                InReview = item.InReview,
                UserId = item.UserId
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
