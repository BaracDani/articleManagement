using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("UserDomain")]
    public class UserDomain : BaseEntity
    {
        public virtual ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual Domain Domain { get; set; }

        [Required]
        public long DomainId { get; set; }
        [NotMapped]
        public override string Table => nameof(Domain);
    }
}
