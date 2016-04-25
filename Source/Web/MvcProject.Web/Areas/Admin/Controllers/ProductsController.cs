namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models;
    using Data.Models.EntityContracts;
    using GlobalConstants;
    using Infrastructure.Extensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using MvcProject.Web.Areas.Common.Controllers;
    using Services.Data;
    using ViewModels.Comments;
    using ViewModels.Products;
    using ViewModels.Tags;
    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    public class ProductsController : BaseGridController<IProductsService, ProductViewModel>
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
            : base(productsService)
        {
            this.productsService = productsService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            //var viewModel = this.productsService.GetAll().To<ProductViewModel>();

            //return this.Json(viewModel.ToDataSourceResult(request));

            return base.Read(request);
        }

        [HttpPost]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, ProductViewModel viewModel)
        {
            //if (viewModel != null && this.ModelState.IsValid)
            //{
            //    // Save record to base
            //}

            //return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));

            return base.Create(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, ProductViewModel viewModel)
        {
            //if (viewModel != null && this.ModelState.IsValid)
            //{
            //    // Edit record
            //}

            //return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));

            return base.Update(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ProductViewModel viewModel)
        {
            //if (viewModel != null)
            //{
            //    // Destroy record
            //}

            //return this.Json(new[] { viewModel }.ToDataSourceResult(request, this.ModelState));

            return base.Destroy(request, viewModel);
        }

        #region ClientDetailsHelpers
        [HttpPost]
        public ActionResult GetTagsByProductId([DataSourceRequest]DataSourceRequest request, int productId)
        {
            var result = this.productsService
                .GetById(productId)
                .Tags
                .AsQueryable()
                .To<TagDetailsForProductViewModel>();
            return this.Json(result.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult GetCommentsByProductId([DataSourceRequest]DataSourceRequest request, int productId)
        {
            var result = this.productsService
                .GetById(productId)
                .Comments
                .AsQueryable()
                .To<CommentDetailsForProductViewModel>();
            return this.Json(result.ToDataSourceResult(request, this.ModelState));
        }
        #endregion
    }
}