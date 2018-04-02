using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using DataAccess.Base.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Repositories;

namespace DataAccess.Base.Infrastructure
{
    public class Catalog : UnitOfWork<ApplicationDbContext, DatabaseFactory<ApplicationDbContext>>, ICatalog
    {
        private IActivityLogRepository _activityLogRepository;

        private IUserRepository _userRepository;

        private IArticleRepository _articleRepository;

        #region IRepositories

        public IActivityLogRepository ActivityLogRepository => _activityLogRepository ?? (_activityLogRepository = new ActivityLogRepository(DatabaseFactory));

        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(DatabaseFactory));
    
        public IArticleRepository ArticleRepository => _articleRepository ?? (_articleRepository = new ArticleRepository(DatabaseFactory));

        #endregion

        #region Constructors

        public Catalog(DatabaseFactory<ApplicationDbContext> databaseFactory)
            : base(databaseFactory)
        {
        }

        #endregion

        public override int SaveChanges()
        {
            try
            {

                return DataContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }

                System.IO.File.AppendAllLines(@"dataerrors.txt", outputLines);

                throw e;
            }
        }
    }
}
