using System;
using System.Data.Entity;
using System.Linq;
using DataAccess.Base.Infrastructure;
using DataAccess.Base.Interfaces;
using DataAccess.Base.Repositories;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class ReviewedArticleRepository : BaseEntityRepository<ReviewedArticle, ApplicationDbContext>, IReviewedArticleRepository
    {
        public ReviewedArticleRepository(IDatabaseFactory<ApplicationDbContext> databaseFactory)
            : base(databaseFactory)
        {
        }

        public override int Update(ReviewedArticle entity)
        {
            ReviewedArticle entityExists = Context.ReviewedArticles.Local.First(e => e.ArticleId == entity.ArticleId && e.UserId == entity.UserId);
            if (entityExists != null)
            {

                Context.Entry(entityExists).State = EntityState.Detached;
                Context.Entry(entity).State = EntityState.Modified;
                return 0;
            }
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return 0;
        }
    }
}