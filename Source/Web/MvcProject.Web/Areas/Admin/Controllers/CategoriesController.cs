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
    using ViewModels.Categories;
    using ViewModels.Products;

    [Authorize(Roles = GlobalConstants.IdentityRoles.Admin)]
    public class CategoriesController : BaseGridController<Category, CategoryViewModel, ICategoriesService, int>
    {
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productsService;

        public CategoriesController(ICategoriesService categoriesService, IProductsService productsService)
            : base(categoriesService)
        {
            this.categoriesService = categoriesService;
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
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, CategoryViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = new Category { };
                this.PopulateEntity(entity, viewModel);
                this.categoriesService.Insert(entity);
                viewModel.Id = entity.Id;
            }

            return base.Create(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, CategoryViewModel viewModel)
        {
            if (viewModel != null && this.ModelState.IsValid)
            {
                var entity = new Category { Id = viewModel.Id };
                this.PopulateEntity(entity, viewModel);
                this.categoriesService.Update(entity);
            }

            return base.Update(request, viewModel);
        }

        [HttpPost]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, CategoryViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

#region DataProviders
        [HttpPost]
        public JsonResult GetProductsByCategoryId([DataSourceRequest]DataSourceRequest request, int categoryId)
        {
            var products = this.categoriesService.GetById(categoryId).Products.AsQueryable().To<ProductDetailsForCategoryViewModel>();
            return this.GetCollectionAsDataSourceResult(request, products, this.ModelState);
        }

        protected override void PopulateEntity(Category entity, CategoryViewModel viewModel)
        {
            if (viewModel.Products != null)
            {
                entity.Products = new List<Product>();
                foreach (var product in viewModel.Products)
                {
                    entity.Products.Add(this.productsService.GetById(product.Id));
                }
            }

            entity.Name = viewModel.Name;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }

        protected override IEnumerable<CategoryViewModel> GetDataAsEnumerable()
        {
            return base.GetDataAsEnumerable().OrderBy(x => x.Name);
        }

        protected override JsonResult GetDataAsJson()
        {
            var categories = this.GetDataAsEnumerable();
            return this.Json(categories, JsonRequestBehavior.AllowGet);
        }
#endregion
    }
}