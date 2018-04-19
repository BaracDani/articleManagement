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
    public class JournalRepository : BaseEntityRepository<Journal, ApplicationDbContext>, IJournalRepository
    {
        public JournalRepository(IDatabaseFactory<ApplicationDbContext> databaseFactory)
            : base(databaseFactory)
        {
        }

        public override int Update(Journal entity)
        {
            if (Context.Journals.Local.Any(e => e.Id == entity.Id))
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