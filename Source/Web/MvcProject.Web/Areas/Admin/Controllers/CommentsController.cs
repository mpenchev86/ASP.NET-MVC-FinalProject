namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models;
    using GlobalConstants;
    using Infrastructure.Extensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Comments;
    using ViewModels.Products;
    using ViewModels.Users;

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    public class CommentsController : BaseGridController<Comment, CommentViewModel, ICommentsService>
    {
        private readonly ICommentsService commentsService;
        private readonly IProductsService productsService;
        private readonly IUsersService usersService;

        public CommentsController(
            ICommentsService commentsService,
            IProductsService productsService,
            IUsersService usersService)
            : base(commentsService)
        {
            this.commentsService = commentsService;
            this.productsService = productsService;
            this.usersService = usersService;
        }

        public ActionResult Index()
        {
            var foreignKeys = new CommentViewModelForeignKeys
            {
                Users = this.usersService.GetAll().To<UserDetailsForCommentViewModel>().ToList(),
                Products = this.productsService.GetAll().To<ProductDetailsForCommentViewModel>().ToList()
            };

            return this.View(foreignKeys);
        }

        [HttpPost]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            return base.Read(request);
        }

        [HttpPost]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, CommentViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = new Comment { };
                this.MapEntity(entity, viewModel);
                this.commentsService.Insert(entity);
                viewModel.Id = entity.Id;
            }

            return base.Create(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, CommentViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = new Comment { Id = viewModel.Id };
                this.MapEntity(entity, viewModel);
                this.commentsService.Update(entity);
            }

            return base.Update(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, CommentViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

        public override void MapEntity(Comment entity, CommentViewModel viewModel)
        {
            entity.Content = viewModel.Content;
            entity.ProductId = viewModel.ProductId;
            entity.UserId = viewModel.UserId;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }

        #region CommentDetailsHelpers
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