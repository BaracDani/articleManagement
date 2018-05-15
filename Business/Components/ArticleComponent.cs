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
    public class ArticleComponent : BaseComponent<Article, ArticleView, IArticleRepository>, IArticleComponent
    {
        public ArticleComponent(ICatalog catalog) : base(catalog)
        {
        }

        protected override IArticleRepository Repository => Catalog.ArticleRepository;

        protected override Article ViewToEntity(ArticleView item)
        {
            return ArticleView.ToEntity(item);
        }

        protected override ArticleView EntityToView(Article item)
        {
            return ArticleView.FromEntity(item);
        }

        public IEnumerable<ArticleView> GetAllByApprovalStatus(int approvalStatus)
        {
            try
            {
                var list = Repository.Filter(article => article.ApprovalStatus == approvalStatus);
                var result = list.ToArray().Select(EntityToView);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public IEnumerable<ArticleView> GetAllPublished(long journalId)
        {

            try
            {
                var list = Repository.Filter(article => article.JournalId == journalId && article.ApprovalStatus == 2);
                var result = list.ToArray().Select(EntityToView);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
