using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Base.Interfaces
{
    public interface ICatalog : IUnitOfWork
    {
        IActivityLogRepository ActivityLogRepository { get; }

        IArticleRepository ArticleRepository { get; }

        IReviewedArticleRepository ReviewedArticleRepository { get; }

        IUserDomainRepository UserDomainRepository { get; }

        IDomainRepository DomainRepository { get; }

        IJournalRepository JournalRepository { get; }
    }
}
