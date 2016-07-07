namespace MvcProject.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Data.Models;
    using Data.Models.Contracts;
    using GlobalConstants;
    using Infrastructure.Extensions;
    using Infrastructure.Filters.ActionMethodSelectors;
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
    public class ProductsController : BaseGridController<Product, ProductViewModel, IProductsService, int>
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;
        private readonly ICommentsService commentsService;
        private readonly IImagesService imagesService;
        private readonly IDescriptionsService descriptionsService;
        private readonly ITagsService tagsService;
        private readonly IVotesService votesService;

        public ProductsController(
            IProductsService productsService,
            ICategoriesService categoriesService,
            ICommentsService commentsService,
            IImagesService imagesService,
            IDescriptionsService descriptionsService,
            ITagsService tagsService,
            IVotesService votesService)
            : base(productsService)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
            this.commentsService = commentsService;
            this.imagesService = imagesService;
            this.descriptionsService = descriptionsService;
            this.tagsService = tagsService;
            this.votesService = votesService;
        }

        public ActionResult Index()
        {
            // Cache maybe
            var foreignKeys = new ProductViewModelForeignKeys
            {
                Categories = this.categoriesService.GetAll().To<CategoryDetailsForProductViewModel>().ToList(),
                Images = this.imagesService.GetAll().To<ImageDetailsForProductViewModel>().ToList(),
                Descriptions = this.descriptionsService.GetAll().To<DescriptionDetailsForProductViewModel>().ToList()
            };

            return this.View(foreignKeys);
        }

        [HttpPost]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            return base.Read(request);
        }

        // [ValidateAntiForgeryToken]
        [HttpPost]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, ProductViewModel viewModel)
        {
            return base.Create(request, viewModel);
        }

        // [ValidateAntiForgeryToken]
        [HttpPost]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, ProductViewModel viewModel)
        {
            return base.Update(request, viewModel);
        }

        // [ValidateAntiForgeryToken]
        [HttpPost]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ProductViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

        #region DataProviders

        // Cache maybe
        public JsonResult GetAllTags()
        {
            var tags = this.tagsService.GetAll().To<TagDetailsForProductViewModel>();
            return this.Json(tags, JsonRequestBehavior.AllowGet);
        }

        protected override void PopulateEntity(Product entity, ProductViewModel viewModel)
        {
            if (viewModel.Comments != null)
            {
                foreach (var comment in viewModel.Comments)
                {
                    entity.Comments.Add(this.commentsService.GetById(comment.Id));
                }
            }

            if (viewModel.Images != null)
            {
                foreach (var image in viewModel.Images)
                {
                    entity.Images.Add(this.imagesService.GetById(image.Id));
                }
            }

            if (viewModel.Votes != null)
            {
                foreach (var vote in viewModel.Votes)
                {
                    entity.Votes.Add(this.votesService.GetById(vote.Id));
                }
            }

            var tagIds = viewModel.Tags.Select(tag => tag.Id);
            this.ProcessProductTags(entity, viewModel.Id, tagIds);

            entity.Title = viewModel.Title;
            entity.CategoryId = viewModel.CategoryId;
            entity.DescriptionId = viewModel.DescriptionId;
            entity.Height = viewModel.Height;
            entity.Length = viewModel.Length;
            entity.MainImageId = viewModel.MainImageId;
            entity.QuantityInStock = viewModel.QuantityInStock;
            entity.ShippingPrice = viewModel.ShippingPrice;
            entity.ShortDescription = viewModel.ShortDescription;
            entity.UnitPrice = viewModel.UnitPrice;
            entity.Weight = viewModel.Weight;
            entity.Width = viewModel.Width;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }

        private void ProcessProductTags(Product entity, int productId, IEnumerable<int> tagIds)
        {
            entity.Tags.Clear();
            foreach (var tagId in tagIds)
            {
                var tag = this.tagsService.GetById(tagId);
                entity.Tags.Add(tag);
            }
        }
        #endregion
    }
}