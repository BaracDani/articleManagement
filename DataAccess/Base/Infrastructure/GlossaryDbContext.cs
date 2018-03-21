using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using System.Configuration;

namespace DataAccess.Base.Infrastructure
{
    public class GlossaryDbContext : DbContext
    {
        public GlossaryDbContext()
            : base(ConfigurationManager.ConnectionStrings["DataAccess"].ConnectionString)
        {

        }

        #region DbSets

        public DbSet<ActivityLog> ActivityLogs { get; set; }

        public DbSet<User> Users { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Contact>().Property(x => x.Address).HasMaxLength(255).IsRequired();
            //modelBuilder.Entity<Contact>().Property(x => x.City).HasMaxLength(50).IsRequired();
            //modelBuilder.Entity<Contact>().Property(x => x.CreatedBy).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
