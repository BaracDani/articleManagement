using System;
using System.Collections.Generic;
using Business.Components.Base;
using Business.Views;

namespace Business.Interfaces
{
    public interface IReviewedArticleComponent : IBaseComponent<ReviewedArticleView>
    {
        ReviewedArticleView GetReviewedArticle(string userId, long articleId);
        bool CheckIfApproved(ReviewedArticleView reviewedArticle);

        int GetNumberofReviewed(ReviewedArticleView reviewedArticle);

        IEnumerable<ArticleView> GetArticlesInReview(string userId);

        ReviewedArticleView[] GetReviewsForArticle(long articleId);

    }
}

