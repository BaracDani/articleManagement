using Business.Components.Base;
using Business.Interfaces;
using Business.Views;
using DataAccess.Base.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Components
{
    public class ReviewedArticleComponent : BaseComponent<ReviewedArticle, ReviewedArticleView, IReviewedArticleRepository>, IReviewedArticleComponent
    {
        public ReviewedArticleComponent(ICatalog catalog) : base(catalog)
        {
        }

        protected override IReviewedArticleRepository Repository => Catalog.ReviewedArticleRepository;

        protected override ReviewedArticle ViewToEntity(ReviewedArticleView item)
        {
            return ReviewedArticleView.ToEntity(item);
        }

        protected override ReviewedArticleView EntityToView(ReviewedArticle item)
        {
            return ReviewedArticleView.FromEntity(item);
        }

        public ReviewedArticleView GetReviewedArticle(string userId, long articleId)
        {
            try
            {
                var list = Repository.Filter(article => article.UserId == userId && article.ArticleId == articleId);

                if (list.ToArray().Length == 0 || list.ToArray().Length > 1)
                {
                    return null;
                }
                return ReviewedArticleView.FromEntity(list.ToArray().Single());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public bool CheckIfApproved(ReviewedArticleView reviewedArticle)
        {
            try
            {
                var list = Repository.Filter(article => (article.ArticleId == reviewedArticle.ArticleId && article.ReviewStatus == 1));
                int count = 0;
                foreach (ReviewedArticle item in list.ToArray())
                {
                    if (item.ReviewPoints == 1 || item.ReviewPoints == 2)
                    {
                        count = count + item.ReviewPoints - 3;
                    }
                    else if (item.ReviewPoints == 3 || item.ReviewPoints == 4)
                    {
                        count = count + item.ReviewPoints - 2;
                    } else
                    {
                        count = count + item.ReviewPoints;
                    }
                }
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public int GetNumberofReviewed(ReviewedArticleView reviewedArticle)
        {
            try
            {
                var list = Repository.Filter(article => (article.ArticleId == reviewedArticle.ArticleId && article.ReviewStatus == 1));
                int result = list.ToArray().Count();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return -1;
        }

        public IEnumerable<ArticleView> GetArticlesInReview(string userId)
        {
            try
            {
                var list = Repository.Filter(article => article.UserId == userId && article.ReviewStatus == 0);

                List<ArticleView> articles = new List<ArticleView>();
                foreach (ReviewedArticle item in list.ToArray())
                {
                    if(item.Article.ApprovalStatus == 4)
                        articles.Add(ArticleView.FromEntity(item.Article));
                }
                return articles;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }


        public ReviewedArticleView[] GetReviewsForArticle(long articleId)
        {
            try
            {
                var list = Repository.Filter(article => article.ArticleId == articleId);
                
                return ReviewedArticleView.FromEntities(list.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
