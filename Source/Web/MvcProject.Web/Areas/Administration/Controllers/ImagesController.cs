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
            this.HandleDependentEntitiesBeforeDelete(viewModel);
            return base.Destroy(request, viewModel);
        }

        #region Data Workers
        protected override void PopulateEntity(Image entity, ImageViewModel viewModel)
        {
            entity.OriginalFileName = viewModel.OriginalFileName;
            entity.ProductId = viewModel.ProductId;
            entity.UrlPath = viewModel.UrlPath;

            // If the current image is a product's main image, the rest should have their 'IsMainImage'
            // property set to false because there can be only one main image.
            // Else if it was main image before, the product entity's MainImage property should be cleared.
            if (viewModel.ProductId != null)
            {
                if (viewModel.IsMainImage)
                {
                    var productImages = this.imagesService.GetByProductId((int)entity.ProductId);
                    foreach (var image in productImages)
                    {
                        image.IsMainImage = false;
                    }
                    this.imagesService.UpdateMany(productImages);
                }
                else
                {
                    if (entity.IsMainImage)
                    {
                        var product = this.productsService.GetById((int)viewModel.ProductId);
                        product.MainImageId = null;
                        product.MainImage = null;
                        this.productsService.Update(product);
                    }
                }
            }

            entity.IsMainImage = viewModel.IsMainImage;
            entity.FileExtension = viewModel.FileExtension;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }

        protected override void HandleDependentEntitiesBeforeDelete(ImageViewModel viewModel)
        {
            if (viewModel.IsMainImage)
            {
                var product = this.productsService.GetById((int)viewModel.ProductId);
                product.MainImageId = null;
                product.MainImage = null;
                this.productsService.Update(product);
            }
        }
        #endregion
    }
}