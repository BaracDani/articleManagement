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
        public IUserDomainComponent UserDomainComponent { get; }
        public DomainController(IDomainComponent component, IUserDomainComponent userDomainComponent): base(component)
        {
            this.UserDomainComponent = userDomainComponent;
        }

        [Route("users")]
        [HttpGet]
        public IHttpActionResult GetUsersOfDomain([FromUri] string domainId, string userId)
        {

            if (String.IsNullOrEmpty(domainId))
                return BadRequest("Domain Id invalid");
            if (String.IsNullOrEmpty(userId))
                return BadRequest("User Id invalid");
            long convertedId;
            try
            {
                convertedId = Convert.ToInt64(domainId);
            }
            catch (Exception ex)
            {
                return BadRequest("Domain Id invalid: " + ex.Message);
            }


            var list = UserDomainComponent.GetUsersOfDomain(convertedId);
            list = list.Where(x => x.Id != userId);
            return Ok(list.Select(u => this.TheModelFactory.Create(u)));
        }

    }
}
