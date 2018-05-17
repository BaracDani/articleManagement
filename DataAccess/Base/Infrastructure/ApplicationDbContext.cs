using DataAccess.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataAccess.Base.Infrastructure
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            //: base("DataAccess")
            : base("DefaultConnection")
        {
        }


        #region DbSets

        public DbSet<ActivityLog> ActivityLogs { get; set; }

        public DbSet<Article> Articles { get; set; }
        public DbSet<ReviewedArticle> ReviewedArticles { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Journal> Journals { get; set; }

        //public DbSet<User> Users { get; set; }

        #endregion
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            //relations for ReviewedArticles
            modelBuilder.Entity<ReviewedArticle>().HasKey(c => new { c.UserId, c.ArticleId });
            modelBuilder.Entity<ReviewedArticle>().Ignore(rw => rw.Id);

            modelBuilder.Entity<UserDomain>().HasKey(c => new { c.UserId, c.DomainId });
            modelBuilder.Entity<UserDomain>().Ignore(rw => rw.Id);

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
