using System;
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
    }
}