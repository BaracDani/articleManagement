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
    [Authorize(Roles = "Editor")]
    [RoutePrefix("api/journal")]
    public class JournalController : CrudApiController<JournalView, IJournalComponent>
    {

        public JournalController(IJournalComponent component): base(component)
        {
        }


        [AllowAnonymous]
        public override IEnumerable<JournalView> Get()
        {
            var list = Component.GetAllPublished();
            return list;
        }

        public override JournalView Post([FromBody] JournalView param)
        {
            param.UserId = User.Identity.GetUserId();
            var item = Component.Create(param);
            if (item == null)
            {
                throw new Exception("Create Api error");
            }
            return item;
        }


        [Route("userJournals")]
        public async Task<IHttpActionResult> GetUserJournals()
        {
            string userId = User.Identity.GetUserId();
            var user = await this.AppUserManager.FindByIdAsync(userId);

            if (user != null)
            {
                var list = user.Journals;
                var convertedList = JournalView.FromEntities(list.ToArray());
                return Ok(convertedList);
            }

            return NotFound();
        }


    }
}
