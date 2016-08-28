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
        private readonly IIdentifierProvider identifierProvider;

        public ProductsController(
            IProductsService productsService,
            ICategoriesService categoriesService,
            ICommentsService commentsService,
            IImagesService imagesService,
            IDescriptionsService descriptionsService,
            ITagsService tagsService,
            IUsersService usersService,
            IVotesService votesService,
            IFileSystemService fileSystemService,
            IIdentifierProvider identifierProvider)
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
            this.identifierProvider = identifierProvider;
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
        public ActionResult CreateProduct([DataSourceRequest]DataSourceRequest request, ProductViewModel viewModel/*, IEnumerable<string> productImagesIds*/)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = new Product();
                // Prevents sending null param.
                this.PopulateEntity(entity, viewModel/*, productImagesIds ?? new List<string>()*/);
                this.productsService.Insert(entity);
                viewModel.Id = entity.Id;
            }

            return this.GetEntityAsDataSourceResult(request, viewModel, this.ModelState);
        }
        
        /// <param name="productImagesIds">Encoded Ids of the product's images.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProduct([DataSourceRequest]DataSourceRequest request, ProductViewModel viewModel/*, IEnumerable<string> productImagesIds*/)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = this.productsService.GetById(viewModel.Id);
                if (entity != null)
                {
                    this.PopulateEntity(entity, viewModel/*, productImagesIds ?? new List<string>()*/);
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

            //var encodedProductImagesIds = (List<string>)(additionalParams.Skip(0).FirstOrDefault());
            //var productImagesIds = encodedProductImagesIds.Select(id => IdentifierProvider.DecodeToIntStatic(id));
            //entity.Images = this.imagesService.GetAll().Where(img => productImagesIds.Contains(img.Id)).ToList();

            // TODO: Check images validity
            var productImagesIds = viewModel.Images.Select(img => img.Id);
            entity.Images = this.imagesService.GetAll().Where(img => productImagesIds.Contains(img.Id)).ToList();

            entity.MainImageId = HandleMainImage(entity.MainImageId, viewModel.MainImageId, productImagesIds);
            
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

        /// <summary>
        /// The 'Save' action of kendo Upload widget.
        /// </summary>
        /// <param name="productImages">The files sent by kendo Upload.</param>
        /// <returns>Returns the Ids of the saved files.</returns>
        [HttpPost]
        public JsonResult SaveImages(IEnumerable<HttpPostedFileBase> productImages)
        {
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
            //var encodedImageIds = new List<string>(processedImages.Select(i => IdentifierProvider.EncodeIntIdStatic(i.Id)));
            var pocoImages = processedImages.AsQueryable().To<Image>().To<ImageDetailsForProductViewModel>().ToArray();
            return Json(new { productImages = pocoImages }, "text/plain", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The 'Remove' action of kendo Upload widget.
        /// </summary>
        /// <param name="fileNames">By default, the 'Remove' action of kendo Upload sends the 'name' property
        /// of the files to be removed to a form field named 'fileNames'. Can be changed through 'removeField'
        /// property.</param>
        /// <param name="imageIds">The encoded Ids of the images to be removed.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveImages(IEnumerable<string> fileNames, IEnumerable<string> imageIds)
        {
            if (imageIds != null && imageIds.Any())
            {
                var imageIdsDecoded = imageIds.Select(id => (int)this.identifierProvider.DecodeIdToInt(id));
                this.imagesService.RemoveImages(imageIdsDecoded);
                //var removedImages = this.imagesService.GetAll().Where(img => imageIdsDecoded.Any(id => id == img.Id)).To<ImageDetailsForProductViewModel>().ToList();
                return Json(new { /*removedImages = removedImages,*/ removedImagesIds = imageIdsDecoded }, "text/plain", JsonRequestBehavior.AllowGet);
            }

            return Json(new { }, "text/plain", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllTags()
        {
            var tags = this.tagsService.GetAll().To<TagDetailsForProductViewModel>();
            return this.Json(tags, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Ensures proper handling of Product-Image dependency with respect to the main image Id property
        /// </summary>
        /// <param name="entityMainImageId">The main image Id of the newly created or updated product entity.</param>
        /// <param name="viewModelMainImageId">The main image Id set in the viewmodel</param>
        /// <param name="productImagesIds">Collection of the Id-s of the viewmodel's Images navigation property</param>
        /// <returns>The value which is to be assigned to the newly created or updated product entity.</returns>
        private int? HandleMainImage(int? entityMainImageId, int? viewModelMainImageId, IEnumerable<int> productImagesIds)
        {
            if (productImagesIds == null || !productImagesIds.Any(id => id == viewModelMainImageId))
            {
                return null;
            }

            if (entityMainImageId == viewModelMainImageId)
            {
                return viewModelMainImageId;
            }

            if (entityMainImageId != null)
            {
                var oldMainImage = this.imagesService.GetById((int)entityMainImageId);
                if (oldMainImage != null)
                {
                    oldMainImage.IsMainImage = false;
                    this.imagesService.Update(oldMainImage);
                }
            }

            if (viewModelMainImageId != null)
            {
                var newMainImage = this.imagesService.GetById((int)viewModelMainImageId);
                if (newMainImage != null)
                {
                    newMainImage.IsMainImage = true;
                    this.imagesService.Update(newMainImage);
                }
            }

            return viewModelMainImageId;
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