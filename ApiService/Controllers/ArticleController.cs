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

namespace ApiService.Controllers
{
    [Authorize]
    [RoutePrefix("api/article")]
    public class ArticleController : CrudApiController<ArticleView, IArticleComponent>
    {
        public ArticleController(IArticleComponent component): base(component)
        {
        }
        
        public override ArticleView Post([FromBody] ArticleView param)
        {
            param.UserId = User.Identity.GetUserId();
            param.InPending = true;
            var item = Component.Create(param);
            if (item == null)
            {
                throw new Exception("Create Api error");
            }
            return item;
        }

        [AllowAnonymous]
        [Route("pendings")]
        public IEnumerable<ArticleView> GetPendings()
        {
            var list = Component.GetAllPendings();
            return list;
        }

        [Route("userArticle")]
        public async Task<IHttpActionResult> GetUserArticles()
        {
            string userId = User.Identity.GetUserId();
            var user = await this.AppUserManager.FindByIdAsync(userId);
            
            if (user != null)
            {
                var list = user.Articles;
                var convertedList = ArticleView.FromEntities(list.ToArray());
                return Ok(convertedList);
            }

            return NotFound();
        }

    }
}
