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
    using Services.Data.ServiceModels;
    using System.Web;
    using System.IO;
    using System.Web.SessionState;
    using Services.Logic;
    using Services.Logic.ServiceModels;
    using Services.Web;
    [Authorize(Roles = IdentityRoles.Admin)]
    public class ProductsController : BaseGridController<Product, ProductViewModel, IProductsService, int>
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;
        private readonly ICommentsService commentsService;
        private readonly IImagesService imagesService;
        private readonly IFileSystemService fileSystemService;
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
            IVotesService votesService,
            IFileSystemService fileSystemService)
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
            this.fileSystemService = fileSystemService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var foreignKeys = new ProductViewModelForeignKeys
            {
                Categories = this.categoriesService.GetAll().To<CategoryDetailsForProductViewModel>(),
                Descriptions = this.descriptionsService.GetAll().To<DescriptionDetailsForProductViewModel>(),
                Sellers = this.usersService.GetAll().Where(u => u.Roles.Any(r => r.RoleName == IdentityRoles.Seller)).To<UserDetailsForProductViewModel>(),
            };

            return this.View(foreignKeys);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            return base.Read(request);
        }

        /// <param name="productImagesIds">Encoded Ids of the product's images.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct([DataSourceRequest]DataSourceRequest request, ProductViewModel viewModel, IEnumerable<string> productImagesIds/*, string productMainImageId*/)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = new Product();
                // Prevents sending null param.
                this.PopulateEntity(entity, viewModel/*, productMainImageId ?? string.Empty*/, productImagesIds ?? new List<string>());
                this.productsService.Insert(entity);
                viewModel.Id = entity.Id;
            }

            return this.GetEntityAsDataSourceResult(request, viewModel, this.ModelState);
        }
        
        /// <param name="productImagesIds">Encoded Ids of the product's images.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProduct([DataSourceRequest]DataSourceRequest request, ProductViewModel viewModel, IEnumerable<string> productImagesIds/*, string productMainImageId*/)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = this.productsService.GetById(viewModel.Id);
                if (entity != null)
                {
                    this.PopulateEntity(entity, viewModel/*, productMainImageId ?? string.Empty*/, productImagesIds ?? new List<string>());
                    this.productsService.Update(entity);
                }
            }

            return this.GetEntityAsDataSourceResult(request, viewModel, this.ModelState);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ProductViewModel viewModel)
        {
            this.HandleDependentEntitiesBeforeDelete(viewModel);
            return base.Destroy(request, viewModel);
        }

        #region DataProviders
        /// <summary>
        /// The 'Save' action of kendo Upload widget.
        /// </summary>
        /// <param name="productImages">The files sent by kendo Upload.</param>
        /// <returns>Returns the Ids of the saved files.</returns>
        [HttpPost]
        public JsonResult SaveImages(IEnumerable<HttpPostedFileBase> productImages)
        {
            var encodedImageIds = new List<string>();
            var rawFiles = new List<RawFile>();

            //The Name of the Upload component is "productImages".
            foreach (var file in productImages)
            {
                var rawFile = this.fileSystemService.ToRawFile(file);
                if (rawFile == null)
                {
                    // skips invalid files
                    continue;
                }

                rawFiles.Add(rawFile);
            }

            var processedImages = this.imagesService.ProcessImages(rawFiles);
            this.imagesService.SaveImages(processedImages);
            encodedImageIds = new List<string>(processedImages.Select(i => IdentifierProvider.EncodeIntIdStatic(i.Id)));

            return Json(new { productImagesIds = encodedImageIds }, "text/plain", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The 'Remove' action of kendo Upload widget.
        /// </summary>
        /// <param name="fileNames">By default, the 'Remove' action of kendo Upload sends the 'name' property
        /// of the files to be removed to a form field named 'fileNames'. Can be changed through 'removeField'
        /// property.</param>
        /// <param name="data">Additional information set through the remove event of kendo Upload.</param>
        /// <param name="imageIds">The encoded Ids of the images to be removed.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveImages(IEnumerable<string> fileNames, object data, IEnumerable<string> imageIds)
        {
            if (imageIds != null && imageIds.Any())
            {
                this.imagesService.RemoveImages(imageIds);
            }

            //return Json(new { productImagesIds = productImagesIds }, "text/plain", JsonRequestBehavior.AllowGet);
            return Json(new { }, "text/plain", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllTags()
        {
            var tags = this.tagsService.GetAll().To<TagDetailsForProductViewModel>();
            return this.Json(tags, JsonRequestBehavior.AllowGet);
        }

        /// <param name="additionalParams">The first element receives product's MainImage Id, if any. The second 
        /// element receives the list of product's images Ids, if any.</param>
        protected override void PopulateEntity(Product entity, ProductViewModel viewModel, params object[] additionalParams)
        {
            var productCommentsIds = viewModel.Comments.Select(c => c.Id);
            entity.Comments = this.commentsService.GetAll().Where(c => productCommentsIds.Contains(c.Id)).ToList();

            var productVotesIds = viewModel.Votes.Select(v => v.Id);
            entity.Votes = this.votesService.GetAll().Where(v => productVotesIds.Contains(v.Id)).ToList();

            var productTagsIds = viewModel.Tags.Select(t => t.Id);
            // Resolves conflict caused by the one-to-one relationship.
            entity.Tags.Clear();
            entity.Tags = this.tagsService.GetAll().Where(t => productTagsIds.Contains(t.Id)).ToList();

            //var encodedProductMainImageId = (string)(additionalParams.FirstOrDefault());
            //var productMainImageId = IdentifierProvider.DecodeToIntStatic(encodedProductMainImageId);
            //entity.MainImageId = productMainImageId;

            entity.MainImageId = viewModel.MainImageId;

            var encodedProductImagesIds = (List<string>)(additionalParams.Skip(0).FirstOrDefault());
            var productImagesIds = encodedProductImagesIds.Select(id => IdentifierProvider.DecodeToIntStatic(id));
            entity.Images = this.imagesService.GetAll().Where(img => productImagesIds.Contains(img.Id)).ToList();

            if (viewModel.MainImageId != null)
            {
                foreach (var image in entity.Images)
                {
                    if (image.Id == viewModel.MainImageId)
                    {
                        image.IsMainImage = true;
                    }
                    else
                    {
                        image.IsMainImage = false;
                    }
                }
                this.imagesService.UpdateMany(entity.Images);
            }

            entity.Title = viewModel.Title;
            entity.ShortDescription = viewModel.ShortDescription;
            entity.CategoryId = viewModel.CategoryId;
            entity.DescriptionId = viewModel.DescriptionId;
            entity.SellerId = viewModel.SellerId;
            entity.QuantityInStock = viewModel.QuantityInStock;
            entity.UnitPrice = viewModel.UnitPrice;
            entity.ShippingPrice = viewModel.ShippingPrice;
            entity.AllTimeItemsSold = viewModel.AllTimeItemsSold;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }

        protected override void HandleDependentEntitiesBeforeDelete(ProductViewModel viewModel)
        {
            var imageEntities = this.imagesService.GetByProductId(viewModel.Id);
            imageEntities.Each(img => img.ProductId = null);
            this.imagesService.UpdateMany(imageEntities);
        }
        #endregion
    }
}