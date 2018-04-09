using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("Article")]
    public class Article : BaseEntity
    {
        public string Name { get; set; }
        public string Abstract { get; set; }

        public bool Approved { get; set; }

        public bool Rejected { get; set; }

        public bool InPending { get; set; }

        public bool InReview { get; set; }
        
        public ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }
        [NotMapped]
        public override string Table => nameof(Article);
    }
}
