using Business.Interfaces;
using Business.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using ApiService.Models;

namespace ApiService.Controllers
{
    [Authorize]
    [RoutePrefix("api/domain")]
    public class DomainController : CrudApiController<DomainView, IDomainComponent>
    {
        public DomainController(IDomainComponent component): base(component)
        { 
        }

    }
}
