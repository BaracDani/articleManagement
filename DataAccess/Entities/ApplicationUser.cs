﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccess.Entities
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }

        public virtual List<Article> Articles { get; set; }

        public virtual List<ReviewedArticle> ReviewedArticles { get; set; }

        public virtual List<Journal> Journals { get; set; }

        public virtual List<UserDomain> UserDomains { get; set; }

        public ApplicationUser()
        {
            this.Articles = new List<Article>();
            this.ReviewedArticles = new List<ReviewedArticle>();
            this.Journals = new List<Journal>();
            this.UserDomains = new List<UserDomain>();
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
