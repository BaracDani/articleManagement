using Business.Interfaces;
using Business.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiService.Controllers
{
    public class ArticleController : CrudApiController<ArticleView, IArticleComponent>
    {
        public ArticleController(IArticleComponent component): base(component)
        {
        }
    }
}
