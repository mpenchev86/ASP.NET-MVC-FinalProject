namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Data.Models;
    using Data.Models.EntityContracts;
    using GlobalConstants;
    using Infrastructure.Extensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using MvcProject.Web.Areas.Common.Controllers;
    using Services.Data;
    using ViewModels.Categories;
    using ViewModels.Comments;
    using ViewModels.Descriptions;
    using ViewModels.Images;
    using ViewModels.Products;
    using ViewModels.Properties;
    using ViewModels.Tags;
    using ViewModels.Votes;

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    public class ProductsController : BaseGridController<Product, ProductViewModel, IProductsService>
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;
        private readonly IImagesService imagesService;
        private readonly IDescriptionsService descriptionsService;

        public ProductsController(
            IProductsService productsService,
            ICategoriesService categoriesService,
            IImagesService imagesService,
            IDescriptionsService descriptionsService)
            : base(productsService)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
            this.imagesService = imagesService;
            this.descriptionsService = descriptionsService;
        }

        public ActionResult Index()
        {
            var foreignKeys = new ProductViewModelForeignKeys
            {
                Categories = this.categoriesService.GetAll().To<CategoryDetailsForProductViewModel>().ToList(),
                Images = this.imagesService.GetAll().To<ImageDetailsForProductViewModel>().ToList(),
                //Descriptions = this.descriptionsService.GetAll().To<DescriptionDetailsForProductViewModel>().ToList()
            };

            return this.View(foreignKeys);
        }

        [HttpPost]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
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

#region ProductDetailsHelpers
        [HttpPost]
        public ActionResult GetTagsByProductId([DataSourceRequest]DataSourceRequest request, int productId)
        {
            var tags = this.productsService.GetById(productId).Tags.AsQueryable().To<TagDetailsForProductViewModel>();
            return this.GetCollectionAsDataSourceResult(request, tags, this.ModelState);
        }

        [HttpPost]
        public ActionResult GetCommentsByProductId([DataSourceRequest]DataSourceRequest request, int productId)
        {
            var comments = this.productsService.GetById(productId).Comments.AsQueryable().To<CommentDetailsForProductViewModel>();
            return this.GetCollectionAsDataSourceResult(request, comments, this.ModelState);
        }

        [HttpPost]
        public ActionResult GetVotesByProductId([DataSourceRequest]DataSourceRequest request, int productId)
        {
            var votes = this.productsService.GetById(productId).Votes.AsQueryable().To<VoteDetailsForProductViewModel>();
            return this.GetCollectionAsDataSourceResult(request, votes, this.ModelState);
        }

        [HttpPost]
        public ActionResult GetImagesByProductId([DataSourceRequest]DataSourceRequest request, int productId)
        {
            var images = this.productsService.GetById(productId).Images.AsQueryable().To<ImageDetailsForProductViewModel>();
            return this.GetCollectionAsDataSourceResult(request, images, this.ModelState);
        }

        [HttpGet]
        public ActionResult GetDescriptionByProductId(int? descriptionId)
        {
            var description = new DescriptionDetailsForProductViewModel();
            if (descriptionId != null)
            {
                description = this.descriptionsService
                    .GetAll()
                    .Where(x => x.Id == (int)descriptionId)
                    .To<DescriptionDetailsForProductViewModel>()
                    .FirstOrDefault();
            }

            return this.PartialView("_DescriptionTab", description);
        }
#endregion

        //public override IEnumerable<ProductViewModel> GetDataAsEnumerable()
        //{
        //    return base.GetDataAsEnumerable().OrderBy(x => x.Id);
        //}
    }
}