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
    [RoutePrefix("api/article")]
    public class ArticleController : CrudApiController<ArticleView, IArticleComponent>
    {
        public IReviewedArticleComponent ReviewedArticleComponent { get; }

        public ArticleController(IArticleComponent component, IReviewedArticleComponent reviewedArticleComponent): base(component)
        {
            this.ReviewedArticleComponent = reviewedArticleComponent;
        }

        [AllowAnonymous]
        public override IEnumerable<ArticleView> Get()
        {
            var list = Component.GetAllByApprovalStatus((int)ApprovalStatus.Approved);
            return list;
        }

        public override ArticleView Post([FromBody] ArticleView param)
        {
            param.UserId = User.Identity.GetUserId();
            param.ApprovalStatus = 1;
            param.Deadline = DateTime.Now.AddDays(7);
            var item = Component.Create(param);
            if (item == null)
            {
                throw new Exception("Create Api error");
            }
            return item;
        }

        [Route("pendings")]
        public IEnumerable<ArticleView> GetPendings()
        {
            var list = Component.GetAllByApprovalStatus((int)ApprovalStatus.InPending);
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

        [Route("approve")]
        [HttpPut]
        public IHttpActionResult ApproveArticle([FromBody] ArticleView param)
        {
            ReviewedArticleView reviewedArticle = new ReviewedArticleView();
            reviewedArticle.UserId = User.Identity.GetUserId();
            reviewedArticle.ArticleId = param.Id;
            reviewedArticle.Approved = true;
            var item = ReviewedArticleComponent.Create(reviewedArticle);
            int numberOfApproved = ReviewedArticleComponent.GetNumberofReviewed(reviewedArticle);
            if(numberOfApproved == 3)
            {
                bool isApproved = ReviewedArticleComponent.GetNumberofApproved(reviewedArticle) > 1;
                if (isApproved)
                {
                    param.ApprovalStatus = (int)ApprovalStatus.Approved;
                } else
                {
                    param.ApprovalStatus = (int)ApprovalStatus.Rejected;
                }
                bool rez = Component.Update(param);
                if (rez)
                {
                    return Ok(item);
                } else
                {
                    return BadRequest("Server crashed! Article was not updated");
                }
            }
            if (item == null)
            {
                return BadRequest("Server crashed! Review was not added");
            }
            return Ok(item);
        }

    }
}
