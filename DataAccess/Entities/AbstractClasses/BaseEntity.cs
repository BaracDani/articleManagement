using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        protected BaseEntity()
        {
            Active = true;
        }

        public long Id { get; set; }

        public bool Active { get; set; }

        [NotMapped]
        public virtual string Table { get; set; }
    }
}
