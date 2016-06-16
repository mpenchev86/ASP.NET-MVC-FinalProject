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

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, ProductViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = new Product { };
                this.PopulateEntity(entity, viewModel);
                this.productsService.Insert(entity);
                viewModel.Id = entity.Id;
            }

            return base.Create(request, viewModel);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, ProductViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = new Product { Id = viewModel.Id };
                this.PopulateEntity(entity, viewModel);
                this.productsService.Update(entity);
            }

            return base.Update(request, viewModel);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ProductViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

#region DataProviders
        [HttpPost]
        public JsonResult GetCommentsByProductId([DataSourceRequest]DataSourceRequest request, int productId)
        {
            var comments = this.productsService.GetById(productId).Comments.AsQueryable().To<CommentDetailsForProductViewModel>();
            return this.GetCollectionAsDataSourceResult(request, comments, this.ModelState);
        }

        [HttpPost]
        public JsonResult GetTagsByProductId([DataSourceRequest]DataSourceRequest request, int productId)
        {
            var tags = this.productsService.GetById(productId).Tags.AsQueryable().To<TagDetailsForProductViewModel>();
            return this.GetCollectionAsDataSourceResult(request, tags, this.ModelState);
        }

        [HttpPost]
        public JsonResult GetVotesByProductId([DataSourceRequest]DataSourceRequest request, int productId)
        {
            var votes = this.productsService.GetById(productId).Votes.AsQueryable().To<VoteDetailsForProductViewModel>();
            return this.GetCollectionAsDataSourceResult(request, votes, this.ModelState);
        }

        [HttpPost]
        public JsonResult GetImagesByProductId([DataSourceRequest]DataSourceRequest request, int productId)
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

        protected override void PopulateEntity(Product entity, ProductViewModel viewModel)
        {
            if (viewModel.Tags != null)
            {
                entity.Tags = new List<Tag>();
                foreach (var tag in viewModel.Tags)
                {
                    entity.Tags.Add(this.tagsService.GetById(tag.Id));
                }
            }

            if (viewModel.Comments != null)
            {
                entity.Comments = new List<Comment>();
                foreach (var comment in viewModel.Comments)
                {
                    entity.Comments.Add(this.commentsService.GetById(comment.Id));
                }
            }

            if (viewModel.Images != null)
            {
                entity.Images = new List<Image>();
                foreach (var image in viewModel.Images)
                {
                    entity.Images.Add(this.imagesService.GetById(image.Id));
                }
            }

            if (viewModel.Votes != null)
            {
                entity.Votes = new List<Vote>();
                foreach (var vote in viewModel.Votes)
                {
                    entity.Votes.Add(this.votesService.GetById(vote.Id));
                }
            }

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
#endregion
    }
}