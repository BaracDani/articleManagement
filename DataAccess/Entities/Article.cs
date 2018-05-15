using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("Article")]
    public class Article : BaseEntity
    {

        public Article()
        {
            this.Reviewers = new List<ReviewedArticle>();
        }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Abstract { get; set; }
        
        public int ApprovalStatus { get; set; }

        public string Deadline { get; set; }
        
        public ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        public Journal Journal { get; set; }

        public long JournalId { get; set; }

        public string FilePath { get; set; }

        public virtual List<ReviewedArticle> Reviewers { get; set; }

        [NotMapped]
        public override string Table => nameof(Article);
    }
}
