namespace MvcProject.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models;
    using MvcProject.Common.GlobalConstants;
    using Infrastructure.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Images;
    using ViewModels.Products;

    [Authorize(Roles = IdentityRoles.Admin)]
    public class ImagesController : BaseGridController<Image, ImageViewModel, IImagesService, int>
    {
        private readonly IImagesService imagesService;
        private readonly IProductsService productsService;

        public ImagesController(
            IImagesService imagesService,
            IProductsService productsService)
            : base(imagesService)
        {
            this.imagesService = imagesService;
            this.productsService = productsService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var foreignKeys = new ImageViewModelForeignKeys
            {
                Products = this.productsService.GetAll().To<ProductDetailsForImageViewModel>()
            };

            return this.View(foreignKeys);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            return base.Read(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, ImageViewModel viewModel)
        {
            return base.Create(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, ImageViewModel viewModel)
        {
            return base.Update(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ImageViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

        #region DataProviders
        protected override void PopulateEntity(Image entity, ImageViewModel viewModel, params object[] additionalParams)
        {
            entity.OriginalFileName = viewModel.OriginalFileName;
            entity.ProductId = viewModel.ProductId;
            entity.UrlPath = viewModel.UrlPath;
            entity.FileExtension = viewModel.FileExtension;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }
        #endregion
    }
}