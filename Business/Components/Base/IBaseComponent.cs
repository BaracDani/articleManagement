using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Views;

namespace Business.Components.Base
{
    public interface IBaseComponent<TV> where TV : BaseView
    {
        TV Create(TV param);
        bool Update(TV param);

        bool Delete(int id);

        TV GetById(int id);
        IEnumerable<TV> GetAll();
    }
}
