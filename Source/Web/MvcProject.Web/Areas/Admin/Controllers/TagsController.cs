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
    using ViewModels.Products;
    using ViewModels.Tags;

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    public class TagsController : BaseGridController<Tag, TagViewModel, ITagsService>
    {
        private readonly ITagsService tagsService;
        private readonly IProductsService productsService;

        public TagsController(ITagsService tagsService, IProductsService productsService)
            : base(tagsService)
        {
            this.tagsService = tagsService;
            this.productsService = productsService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            return base.Read(request);
        }

        [HttpPost]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, TagViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = new Tag { };
                this.MapEntity(entity, viewModel);
                this.tagsService.Insert(entity);
                viewModel.Id = entity.Id;
            }

            return base.Create(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, TagViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = new Tag { Id = viewModel.Id };
                this.MapEntity(entity, viewModel);
                this.tagsService.Update(entity);
            }

            return base.Update(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, TagViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

        public override void MapEntity(Tag entity, TagViewModel viewModel)
        {
            if (viewModel.Products != null)
            {
                entity.Products = new List<Product>();
                foreach (var product in viewModel.Products)
                {
                    entity.Products.Add(this.productsService.GetById(product.Id));
                }

                //entity.Products = viewModel.Products.AsQueryable().To<Product>().ToList();
            }

            entity.Name = viewModel.Name;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }

        #region TagDetailsHelpers
        [HttpPost]
        public JsonResult GetProductsByTagId([DataSourceRequest]DataSourceRequest request, int tagId)
        {
            var products = this.tagsService.GetById(tagId).Products.AsQueryable().To<ProductDetailsForTagViewModel>();
            return this.GetCollectionAsDataSourceResult(request, products, this.ModelState);
        }
        
        //[HttpPost]
        //public JsonResult ReadByProductId([DataSourceRequest]DataSourceRequest request, int productId)
        //{
        //    var result = this.tagsService
        //        .GetAll()
        //        .Where(t => t.Products.Any(p => p.Id == productId))
        //        .To<TagDetailsForProductViewModel>();
        //    return this.Json(result.ToDataSourceResult(request, this.ModelState));
        //}
        #endregion
    }
}