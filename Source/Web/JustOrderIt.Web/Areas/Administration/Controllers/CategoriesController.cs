namespace JustOrderIt.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using JustOrderIt.Common.GlobalConstants;
    using Infrastructure.Extensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.Categories;
    using ViewModels.Products;
    using ViewModels.Keywords;
    using Data.Models.Catalog;

    [Authorize(Roles = IdentityRoles.Admin)]
    public class CategoriesController : BaseGridController<Category, CategoryViewModel, ICategoriesService, int>
    {
        private readonly ICategoriesService categoriesService;
        private readonly IKeywordsService keywordsService;
        private readonly IProductsService productsService;
        private readonly ISearchFiltersService searchFiltersService;

        public CategoriesController(
            ICategoriesService categoriesService,
            IKeywordsService keywordsService,
            IProductsService productsService,
            ISearchFiltersService searchFiltersService)
            : base(categoriesService)
        {
            this.categoriesService = categoriesService;
            this.productsService = productsService;
            this.searchFiltersService = searchFiltersService;
            this.keywordsService = keywordsService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            return base.Read(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create([DataSourceRequest]DataSourceRequest request, CategoryViewModel viewModel)
        {
            return base.Create(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Update([DataSourceRequest]DataSourceRequest request, CategoryViewModel viewModel)
        {
            return base.Update(request, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Destroy([DataSourceRequest]DataSourceRequest request, CategoryViewModel viewModel)
        {
            return base.Destroy(request, viewModel);
        }

        #region Data Workers
        protected override void PopulateEntity(Category entity, CategoryViewModel viewModel)
        {
            var categoryProductsIds = viewModel.Products.Select(c => c.Id);
            entity.Products = this.productsService.GetAll().Where(p => categoryProductsIds.Contains(p.Id)).ToList();
            
            var categorySearchFiltersIds = viewModel.SearchFilters.Select(c => c.Id);
            entity.SearchFilters = this.searchFiltersService.GetAll().Where(sf => categorySearchFiltersIds.Contains(sf.Id)).ToList();

            var categoryKeywordssIds = viewModel.Keywords.Select(t => t.Id);
            // Resolves conflict caused by the one-to-one relationship.
            entity.Keywords.Clear();
            entity.Keywords = this.keywordsService.GetAll().Where(kw => categoryKeywordssIds.Contains(kw.Id)).ToList();

            entity.Name = viewModel.Name;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.ModifiedOn = viewModel.ModifiedOn;
            entity.IsDeleted = viewModel.IsDeleted;
            entity.DeletedOn = viewModel.DeletedOn;
        }

        [HttpGet]
        public JsonResult GetAllKeywords()
        {
            var keywords = this.keywordsService.GetAll().To<KeywordDetailsForCategoryViewModel>();
            return this.Json(keywords, JsonRequestBehavior.AllowGet);
        }

        protected override IQueryable<CategoryViewModel> GetQueryableData()
        {
            return this.categoriesService.GetAll().To<CategoryViewModel>().OrderBy(x => x.Name);
        }
        #endregion
    }
}