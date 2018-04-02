using System;
using DataAccess.Base.Infrastructure;
using DataAccess.Base.Interfaces;
using DataAccess.Base.Repositories;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class ArticleRepository : BaseEntityRepository<Article, ApplicationDbContext>, IArticleRepository
    {
        public ArticleRepository(IDatabaseFactory<ApplicationDbContext> databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}