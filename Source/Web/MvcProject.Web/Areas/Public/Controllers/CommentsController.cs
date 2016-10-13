namespace MvcProject.Web.Areas.Public.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Infrastructure.Validators;
    using Services.Data;
    using ViewModels.Comments;
    public class CommentsController: BasePublicController
    {
        ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        [HttpGet]
        public ActionResult Create(int productId)
        {
            var comment = new CommentViewModel();
            comment.ProductId = productId;
            return this.PartialView("_", comment);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult Create(CommentViewModel comment)
        {
            return this.Json(new { });
        }
    }
}