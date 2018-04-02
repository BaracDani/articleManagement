using System;
using DataAccess.Entities;
using DataAccess.Interfaces;
using DataAccess.Base.Infrastructure;
using DataAccess.Base.Interfaces;
using DataAccess.Base.Repositories;

namespace DataAccess.Repositories
{
    public class UserRepository : BaseEntityRepository<User, ApplicationDbContext>, IUserRepository
    {
        public UserRepository(IDatabaseFactory<ApplicationDbContext> databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
