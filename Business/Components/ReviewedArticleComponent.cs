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


        public int GetNumberofApproved(ReviewedArticleView reviewedArticle)
        {
            try
            {
                var list = Repository.Filter(article => article.ArticleId == reviewedArticle.ArticleId && article.Approved == true);
                int result = list.ToArray().Count();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return -1;
        }

        public int GetNumberofReviewed(ReviewedArticleView reviewedArticle)
        {
            try
            {
                var list = Repository.Filter(article => article.ArticleId == reviewedArticle.ArticleId);
                int result = list.ToArray().Count();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return -1;
        }
    }
}
