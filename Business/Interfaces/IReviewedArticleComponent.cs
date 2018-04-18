using System;
using System.Collections.Generic;
using Business.Components.Base;
using Business.Views;

namespace Business.Interfaces
{
    public interface IReviewedArticleComponent : IBaseComponent<ReviewedArticleView>
    {
        int GetNumberofApproved(ReviewedArticleView reviewedArticle);
        int GetNumberofReviewed(ReviewedArticleView reviewedArticle);
    }
}

