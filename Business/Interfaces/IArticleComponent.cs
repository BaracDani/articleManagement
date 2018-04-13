using System;
using System.Collections.Generic;
using Business.Components.Base;
using Business.Views;

namespace Business.Interfaces
{
    public interface IArticleComponent : IBaseComponent<ArticleView>
    {
        IEnumerable<ArticleView> GetAllByApprovalStatus(int approvalStatus);
    }
}

