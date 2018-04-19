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
    public class DomainRepository : BaseEntityRepository<Domain, ApplicationDbContext>, IDomainRepository
    {
        public DomainRepository(IDatabaseFactory<ApplicationDbContext> databaseFactory)
            : base(databaseFactory)
        {
        }

        public override int Update(Domain entity)
        {
            if (Context.Domains.Local.Any(e => e.Id == entity.Id))
            {

                Context.Entry(entity).State = EntityState.Modified;
                return 0;
            }
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return 0;
        }
    }
}