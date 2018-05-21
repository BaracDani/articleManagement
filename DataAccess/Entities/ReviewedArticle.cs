using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("ReviewedArticle")]
    public class ReviewedArticle : BaseEntity
    {
        public int ReviewStatus { get; set; }

        public int ReviewPoints { get; set; }

        public string Comment { get; set; }

        public virtual Article Article { get; set; }

        [Required]
        public long ArticleId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }
        [NotMapped]
        public override string Table => nameof(ReviewedArticle);
    }
}
