﻿using Business.Interfaces;
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
using System.Web;
using System.IO;
using System.Net.Http.Headers;

namespace ApiService.Controllers
{
    [Authorize]
    [RoutePrefix("api/article")]
    public class ArticleController : CrudApiController<ArticleView, IArticleComponent>
    {
        public IReviewedArticleComponent ReviewedArticleComponent { get; }

        public ArticleController(IArticleComponent component, IReviewedArticleComponent reviewedArticleComponent) : base(component)
        {
            this.ReviewedArticleComponent = reviewedArticleComponent;
        }

        [AllowAnonymous]
        public override IEnumerable<ArticleView> Get()
        {
            var list = Component.GetAllByApprovalStatus((int)ApprovalStatus.Approved);
            return list;
        }



        [AllowAnonymous]
        [Route("published")]
        [HttpGet]
        public IHttpActionResult GetPublishedArticles([FromUri]string journalId)
        {
            if (String.IsNullOrEmpty(journalId))
                return BadRequest("Journal Id invalid");
            long convertedId;
            try
            {
                convertedId = Convert.ToInt64(journalId);
            }
            catch (Exception ex)
            {
                return BadRequest("Journal Id invalid: " + ex.Message);
            }


            var list = Component.GetAllPublished(convertedId);
            return Ok(list);
        }

        [Route("inReview")]
        [HttpGet]
        public IEnumerable<ArticleView> GetInReviewArticles()
        {

            string userId = User.Identity.GetUserId();
            var list = ReviewedArticleComponent.GetArticlesInReview(userId);
            return list;
        }

        [Route("reviewsForArticle")]
        [HttpGet]
        public IHttpActionResult GetReviewsForArticle([FromUri]string articleId)
        {
            if (String.IsNullOrEmpty(articleId))
                return BadRequest("Article Id invalid");
            long convertedId;
            try
            {
                convertedId = Convert.ToInt64(articleId);
            }
            catch (Exception ex)
            {
                return BadRequest("Article Id invalid: " + ex.Message);
            }

            var list = ReviewedArticleComponent.GetReviewsForArticle(convertedId);
            var resultedList = list.Select((reviewedArticle) => { return reviewedArticle.Comment; });
            return Ok(resultedList);
        }

        [Authorize(Roles = "Editor")]
        [Route("pendings")]
        [HttpGet]
        public IHttpActionResult GetArticlesByJournal([FromUri]string journalId)
        {
            if (String.IsNullOrEmpty(journalId))
                return BadRequest("Journal Id invalid");
            long convertedId;
            try
            {
                convertedId = Convert.ToInt64(journalId);
            }
            catch (Exception ex)
            {
                return BadRequest("Journal Id invalid: " + ex.Message);
            }


            var list = Component.GetAllByJournal(convertedId);
            return Ok(list);
        }
        //public override ArticleView Post([FromBody] ArticleView param)
        //{
        //    param.UserId = User.Identity.GetUserId();
        //    param.ApprovalStatus = 1;
        //    param.Deadline = DateTime.Now.AddDays(7);
        //    var item = Component.Create(param);
        //    if (item == null)
        //    {
        //        throw new Exception("Create Api error");
        //    }
        //    return item;
        //}


        [Route("postArticle")]
        [HttpPost]
        public HttpResponseMessage PostArticle()
        {
            // Check if the request contains multipart/form-data.
            //if (!Request.Content.IsMimeMultipartContent())
            //{
            //    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            //}

            //string root = HttpContext.Current.Server.MapPath("~/uploads");
            //var provider = new MultipartFormDataStreamProvider(root);

            //try
            //{
            //    // Read the form data.
            //    await Request.Content.ReadAsMultipartAsync(provider);

            //    string file1 = provider.FileData.First().LocalFileName;
            //    // This illustrates how to get the file names.
            //    foreach (MultipartFileData file in provider.FileData)
            //    {
            //        var test = file.Headers.ContentDisposition.FileName;//get FileName
            //        var test1 = file.LocalFileName;//get File Path
            //        string title = HttpContext.Current.Request.Form.GetValues("title")[0];
            //    }
            //    return Request.CreateResponse(HttpStatusCode.OK, "upload article successed!");
            //}
            //catch (System.Exception e)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            //}
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                ArticleView item = null;

                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    int hasheddate = DateTime.Now.GetHashCode();
                    //Good to use an updated name always, since many can use the same file name to upload.
                    string changed_name = hasheddate.ToString() + "_" + postedFile.FileName;

                    var filePath = HttpContext.Current.Server.MapPath("~/uploads/" + changed_name);
                    postedFile.SaveAs(filePath); // save the file to a folder "Images" in the root of your app

                    changed_name = @"~\uploads\" + changed_name; //store this complete path to database
                    string title = HttpContext.Current.Request.Form.GetValues("title")[0];
                    string author = HttpContext.Current.Request.Form.GetValues("author")[0];
                    string abstractContent = HttpContext.Current.Request.Form.GetValues("abstract")[0];
                    long journalId = Convert.ToInt64(HttpContext.Current.Request.Form.GetValues("journalId")[0]);
                    ArticleView param = new ArticleView();
                    param.UserId = User.Identity.GetUserId();
                    param.ApprovalStatus = 1;
                    param.Deadline = DateTime.Now;
                    param.Title = title;
                    param.Author = author;
                    param.Abstract = abstractContent;
                    param.JournalId = journalId;
                    param.FilePath = changed_name;
                    item = Component.Create(param);
                   
                }
                if (item == null)
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest,"Create Article api error");
                } else
                {
                    result = Request.CreateResponse(HttpStatusCode.OK, "upload article successed!");
                }
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return result;
        }

        [Route("downloadFile")]
        [HttpGet]
        public HttpResponseMessage GetFile([FromUri]string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            
            string localFilePath = fileName;

            localFilePath = HttpContext.Current.Server.MapPath(fileName);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = Path.GetFileName(localFilePath);
            string extension = Path.GetExtension(localFilePath);
            if(extension == ".pdf")
            {
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            }
            if (extension == ".doc")
            {
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/msword");
            }
            if (extension == ".docx")
            {
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            }

            return response;
        }

        [Route("pendings2")]
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

        [Authorize(Roles = "Editor")]
        [Route("editorApprove")]
        [HttpPut]
        public async Task<IHttpActionResult> ApproveArticle([FromBody] UsersToReview param)
        {
            foreach (string userId in param.UserIds)
            {
                ReviewedArticleView reviewedArticle = new ReviewedArticleView();
                reviewedArticle.UserId = userId;
                reviewedArticle.ArticleId = param.Article.Id;
                var item = ReviewedArticleComponent.Create(reviewedArticle);
                param.Article.ApprovalStatus = (int)ApprovalStatus.InReview;
                if (item == null)
                {
                    return BadRequest("Server crashed! Review was not added");
                }

                var userToReview = await this.AppUserManager.FindByIdAsync(userId);
                await this.AppUserManager.SendEmailAsync(userToReview.Id,
                                                   "Article needs review!",
                                                   "A new article '" + param.Article.Title + "' needs to be reviewed by you!");
            }

            if (param.UserIds.ToArray().Length > 0)
            {
                bool rez = Component.Update(param.Article);
            }

            var user = await this.AppUserManager.FindByIdAsync(param.Article.UserId);
            await this.AppUserManager.SendEmailAsync(user.Id,
                                               "Article approved by editor",
                                               "Your article '" + param.Article.Title + "' was approved by the editor of the journal you applied");
            return Ok("Users added for review");
        }

        [Authorize(Roles = "Editor")]
        [Route("editorReject")]
        [HttpPut]
        public async Task<IHttpActionResult> RejectArticle([FromBody] ArticleView param)
        {
            param.ApprovalStatus = (int)ApprovalStatus.Rejected;
            bool rez = Component.Update(param);
            if (rez)
            {

                var user = await this.AppUserManager.FindByIdAsync(param.UserId);
                await this.AppUserManager.SendEmailAsync(user.Id,
                                                   "Article rejected",
                                                   "Your article '" + param.Title + "' was rejected");
                return Ok("Article rejected succesful");
            }

            return Ok("Article not rejected succesful");
        }

        [Route("userReview")]
        [HttpPut]
        public IHttpActionResult ApproveArticle([FromBody] ReviewArticleRequest param)
        {

            string userId = User.Identity.GetUserId();
            ReviewedArticleView reviewedArticle = ReviewedArticleComponent.GetReviewedArticle(userId, param.Article.Id);

            if (reviewedArticle == null)
            {
                return BadRequest("Review is not available for this user and this article");
            }
            reviewedArticle.ReviewPoints = param.ReviewPoints;
            reviewedArticle.Comment = param.Comment;
            reviewedArticle.ReviewStatus = (int)ReviewStatus.Reviewed;
            bool updated = ReviewedArticleComponent.Update(reviewedArticle);
            int numberOfReviewed = ReviewedArticleComponent.GetNumberofReviewed(reviewedArticle);
            if (numberOfReviewed == 3)
            {
                bool isApproved = ReviewedArticleComponent.CheckIfApproved(reviewedArticle);
                if (isApproved)
                {
                    param.Article.ApprovalStatus = (int)ApprovalStatus.Approved;
                }
                else
                {
                    param.Article.ApprovalStatus = (int)ApprovalStatus.Rejected;
                }
                bool rez = Component.Update(param.Article);
                if (rez)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Server crashed! Article was not updated");
                }
            }
            if (!updated)
            {
                return BadRequest("Server crashed! Review was not updated");
            }
            return Ok();
        }

        [Route("userReviewAbstain")]
        [HttpPut]
        public IHttpActionResult AbstainReviewArticle([FromBody] ArticleView param)
        {

            string userId = User.Identity.GetUserId();
            ReviewedArticleView reviewedArticle = ReviewedArticleComponent.GetReviewedArticle(userId, param.Id);

            if (reviewedArticle == null)
            {
                return BadRequest("Review is not available for this user and this article");
            }
            reviewedArticle.ReviewStatus = (int)ReviewStatus.Abstain;
            bool updated = ReviewedArticleComponent.Update(reviewedArticle);
            if (!updated)
            {
                return BadRequest("Server crashed! Review was not updated");
            }
            return Ok();
        }
    }
}
