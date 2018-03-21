using System;
using DataAccess.Entities;
using DataAccess.Interfaces;
using DataAccess.Base.Infrastructure;
using DataAccess.Base.Interfaces;
using DataAccess.Base.Repositories;

namespace DataAccess.Repositories
{
    public class UserRepository : BaseEntityRepository<User, GlossaryDbContext>, IUserRepository
    {
        public UserRepository(IDatabaseFactory<GlossaryDbContext> databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
