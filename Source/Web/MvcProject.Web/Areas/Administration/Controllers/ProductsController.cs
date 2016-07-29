namespace MvcProject.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Data.Models;
    using Data.Models.Contracts;
    using MvcProject.Common.GlobalConstants;
    using Infrastructure.Extensions;
    using Infrastructure.Filters.ActionMethodSelectors;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Categories;
    using ViewModels.Comments;
    using ViewModels.Descriptions;
    using ViewModels.Images;
    using ViewModels.Products;
    using ViewModels.Properties;
    using ViewModels.Tags;
    using ViewModels.Votes;
    using ViewModels.Users;
    [Authorize(Roles = IdentityRoles.Admin)]
    public class ProductsController : BaseGridController<Product, ProductViewModel, IProductsService, int>
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;
        private readonly ICommentsService commentsService;
        private readonly IImagesService imagesService;
        private readonly IDescriptionsService descriptionsService;
        private readonly ITagsService tagsService;
        private readonly IUsersService usersService;
        private readonly IVotesService votesService;

        public ProductsController(
            IProductsService productsService,
            ICategoriesService categoriesService,
            ICommentsService commentsService,
            IImagesService imagesService,
            IDescriptionsService descriptionsService,
            ITagsService tagsService,
            IUsersService usersService,
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
            this.usersService = usersService;
        }

        public ActionResult Index()
        {
            // Cache maybe
            var foreignKeys = new ProductViewModelForeignKeys
            {
                Categories = this.categoriesService.GetAll().To<CategoryDetailsForProductViewModel>(),
                Images = this.imagesService.GetAll().To<ImageDetailsForProductViewModel>(),
                Descriptions = this.descriptionsService.GetAll().To<DescriptionDetailsForProductViewModel>(),
                Sellers = this.usersService.GetAll().Where(u => u.Roles.Any(r => r.RoleName == IdentityRoles.Seller)).To<UserDetailsForProductViewModel>().ToList(),
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

            //// Votes should be populated first.
            //entity.AllTimeAverageRating = entity.Votes.Any() ? (int)entity.Votes.Average(v => v.VoteValue) : 0;

            var tagIds = viewModel.Tags.Select(tag => tag.Id);
            this.ProcessProductTags(entity, viewModel.Id, tagIds);

            entity.Title = viewModel.Title;
            entity.ShortDescription = viewModel.ShortDescription;
            entity.CategoryId = viewModel.CategoryId;
            entity.DescriptionId = viewModel.DescriptionId;
            entity.SellerId = viewModel.SellerId;
            entity.MainImageId = viewModel.MainImageId;
            entity.QuantityInStock = viewModel.QuantityInStock;
            entity.UnitPrice = viewModel.UnitPrice;
            entity.ShippingPrice = viewModel.ShippingPrice;
            entity.AllTimeItemsSold = viewModel.AllTimeItemsSold;
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