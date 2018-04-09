using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.Components.Base;
using Business.Views;
using ApiService.Infrastructure;
using Microsoft.AspNet.Identity;

namespace ApiService.Controllers
{
    public abstract class CrudApiController<TV, TC> : BaseApiController where TV : BaseView where TC : IBaseComponent<TV>
    {
        public TC Component { get; }

        protected CrudApiController(TC component)
        {
            this.Component = component;
        }

        /// <summary>
        /// TEST Doc
        /// </summary>
        /// <returns>TEST Doc</returns>
        public IEnumerable<TV> Get()
        {
            var list = Component.GetAll();
            return list;
        }
        
        public TV Get(int id)
        {
            var item = Component.GetById(id);
            return item;
        }

        //[HttpGet]
        //public string Create(string userName, string password)
        //{
        //    //param.UserId = 1; //GetContextUser().Name;
        //    //var item = Component.Create(param);
        //    //if (item == null)
        //    //{
        //    //    throw new NotFoundException();
        //    //}
        //    return userName + password;
        //}
        public virtual TV Post([FromBody] TV param)
        {
            var item = Component.Create(param);
            if (item == null)
            {
                throw new Exception("Create Api error");
            }
            return item;
        }
        
        public void Put([FromBody] TV param)
        {
            // param.UserId = 
            // param.ModifiedBy = GetContextUser().Name;
            if (!Component.Update(param))
            {
                throw new Exception("Not Found");
            }
        }
        
        public void Delete(int id)
        {
            Component.Delete(id);
        }

    }
}
