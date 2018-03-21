using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Base.Interfaces
{
    public interface IDatabaseFactory<out T> where T : class
    {
        T Get();
    }
}
