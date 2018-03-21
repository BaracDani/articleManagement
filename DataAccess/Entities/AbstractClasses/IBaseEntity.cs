using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public interface IBaseEntity
    {
        long Id { get; set; }

        bool Active { get; set; }

        string Table { get; set; }

    }
}
