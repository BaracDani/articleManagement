using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("Journal")]
    public class Journal : BaseEntity
    {

        public Journal()
        {
            this.Articles = new List<Article>();
        }

        public string Title { get; set; }

        public bool IsPublished { get; set; }

        public string PublishDate { get; set; }
        
        public ApplicationUser Editor { get; set; }

        [Required]
        public string EditorId { get; set; }

        public Domain Domain { get; set; }

        [Required]
        public long DomainId { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        [NotMapped]
        public override string Table => nameof(Journal);
    }
}
