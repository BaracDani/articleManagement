using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("Domain")]
    public class Domain : BaseEntity
    {

        public Domain()
        {
            this.Journals = new List<Journal>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Journal> Journals { get; set; }

        [NotMapped]
        public override string Table => nameof(Domain);
    }
}
