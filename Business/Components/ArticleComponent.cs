using Business.Components.Base;
using Business.Interfaces;
using Business.Views;
using DataAccess.Base.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

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
    }
}
