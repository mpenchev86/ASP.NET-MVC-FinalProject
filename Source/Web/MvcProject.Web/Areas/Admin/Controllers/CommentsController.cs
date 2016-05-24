namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using GlobalConstants;
    using Infrastructure.Extensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Comments;

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    public class CommentsController : BaseGridController<CommentViewModel, ICommentsService>
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
            : base(commentsService)
        {
            this.commentsService = commentsService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            return base.Read(request);
        }

        [HttpPost]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, CommentViewModel viewModel)
        {
            //if (viewModel != null && this.ModelState.IsValid)
            //{
            //    // Save record to base
            //}

            //return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));

            return base.Create(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, CommentViewModel viewModel)
        {
            //if (viewModel != null && this.ModelState.IsValid)
            //{
            //    // Edit record
            //}

            //return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));

            return base.Update(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, CommentViewModel viewModel)
        {
            //if (viewModel != null)
            //{
            //    // Destroy record
            //}

            //return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));

            return base.Destroy(request, viewModel);
        }

        #region ClientDetailsHelpers
        //[HttpPost]
        //public JsonResult ReadByProductId([DataSourceRequest]DataSourceRequest request, int productId)
        //{
        //    var result = this.commentsService
        //        .GetAll()
        //        .Where(c => c.ProductId == productId)
        //        .To<CommentDetailsForProductViewModel>();
        //    return this.Json(result.ToDataSourceResult(request, this.ModelState));
        //}

        //[HttpPost]
        //public JsonResult ReadByUserId([DataSourceRequest]DataSourceRequest request, string userId)
        //{
        //    var result = this.commentsService
        //        .GetAll()
        //        .Where(c => c.UserId == userId)
        //        .To<CommentDetailsForProductViewModel>();
        //    return this.Json(result.ToDataSourceResult(request, this.ModelState));
        //}
        #endregion
    }
}