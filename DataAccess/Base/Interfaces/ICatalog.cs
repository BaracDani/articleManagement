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
        IUserRepository UserRepository { get; }

        IActivityLogRepository ActivityLogRepository { get; }
    }
}
